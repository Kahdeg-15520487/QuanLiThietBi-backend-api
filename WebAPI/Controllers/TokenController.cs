using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        private readonly double AccessExpire;
        private readonly double RefreshExpire;

        public TokenController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;

            double.TryParse(_configuration["Jwt:AccessExpire"], out AccessExpire);
            double.TryParse(_configuration["Jwt:RefreshExpire"], out RefreshExpire);

            //System.IO.File.WriteAllText(@"D:\log.txt", $"{AccessExpire} {RefreshExpire}");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<string> CheckClaim()
        {
            List<string> result = new List<string>();

            result.AddRange(User.Claims.Select(c => c.ToString()));

            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            result.AddRange(roles);

            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

                    if (!result.Succeeded)
                    {
                        return Unauthorized();
                    }

                    var token = GenerateAccessToken(user, Guid.NewGuid().ToString(), GetAccessTokenExpire());

                    var jwtWriter = new JwtSecurityTokenHandler();

                    var refreshToken = await GetRefreshTokenAsync(user);

                    if (refreshToken != null)
                    {
                        await RemoveRefreshToken(refreshToken);
                    }

                    var refToken = GenerateRefreshToken(Guid.NewGuid().ToString(), GetRefreshTokenExpire());

                    refreshToken = new RefreshToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Token = jwtWriter.WriteToken(refToken),
                        Expire = GetRefreshTokenExpire()
                    };

                    await SaveRefreshToken(refreshToken);

                    return Ok(new { token = jwtWriter.WriteToken(token), refreshToken = refreshToken.Token });
                }
            }

            return BadRequest();
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                //get refreshtoken in database
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var refToken = await GetRefreshTokenAsync(user);

                    //validate with request
                    if (refToken.Token.Equals(request.RefreshToken))
                    {
                        //if OK then remove refreshtoken and
                        //    return new token and refreshToken
                        await RemoveRefreshToken(refToken);

                        {
                            var token = GenerateAccessToken(user, Guid.NewGuid().ToString(), GetAccessTokenExpire());
                            var refreshToken = GenerateRefreshToken(Guid.NewGuid().ToString(), GetRefreshTokenExpire());

                            var jwtWriter = new JwtSecurityTokenHandler();

                            RefreshToken newRefToken = new RefreshToken
                            {
                                Id = Guid.NewGuid(),
                                UserId = user.Id,
                                Token = jwtWriter.WriteToken(refreshToken),
                                Expire = GetRefreshTokenExpire()
                            };

                            await SaveRefreshToken(newRefToken);

                            return Ok(new { token = jwtWriter.WriteToken(token), refreshToken = jwtWriter.WriteToken(refreshToken) });
                        }
                    }
                    //else error
                    return Forbid();
                }
                return NotFound();
            }
            return BadRequest();
        }

        private JwtSecurityToken GenerateAccessToken(User user, string guid, DateTime expire)
        {
            var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, guid)
                    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            // Adding roles code
            // Roles property is string collection but you can modify Select code if it it's not
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claimsIdentity.Claims,
                expires: expire,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        private JwtSecurityToken GenerateRefreshToken(string guid, DateTime expire)
        {
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Jti, guid) };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: expire,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        private DateTime GetAccessTokenExpire()
        {
            return DateTime.UtcNow.AddSeconds(AccessExpire);
        }

        private DateTime GetRefreshTokenExpire()
        {
            return DateTime.UtcNow.AddSeconds(RefreshExpire);
        }

        private async Task<object> SaveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return null;
        }

        private async Task<RefreshToken> GetRefreshTokenAsync(User user)
        {
            return await _context.RefreshTokens.Include(rt => rt.User).SingleOrDefaultAsync(m => m.UserId == user.Id);
        }

        private async Task<object> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}