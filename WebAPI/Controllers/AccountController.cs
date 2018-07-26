using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = register.Email, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    var currentUser = await _userManager.FindByNameAsync(user.UserName);
                    var role = await _userManager.AddToRoleAsync(currentUser, "User");

                    return Ok(currentUser.UserName);
                }
                //error register failed
                //AddErrors(result);
                return Json(result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        [HttpGet("reset/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestReset([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)//|| !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Json(new { resetCode = "lala" });
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSender.SendEmailAsync(email, "Reset Password",
               $"Please reset your password by using this reset code{code}");

            return Json(new { resetCode = code });
        }

        [HttpPost("reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist

            }
            var result = await _userManager.ResetPasswordAsync(user, model.ResetCode, model.Password);
            if (result.Succeeded)
            {

            }
            return Json(result.Errors.Select(r => $"{r.Code} : {r.Description}"));
        }
    }
}