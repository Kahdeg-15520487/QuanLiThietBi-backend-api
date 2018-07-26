using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/DeviceDetail")]
    public class DeviceDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<DeviceDetail> GetDeviceDetails()
        {
            return _context.DeviceDetails.Include(dvdt => dvdt.DeviceCategory);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDetail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.DeviceDetails.Include(dvdt => dvdt.DeviceCategory).SingleOrDefaultAsync(m => m.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceDetail([FromRoute] Guid id, [FromBody] DeviceDetail deviceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostDeviceDetails([FromBody] DeviceDetail deviceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DeviceDetails.Add(deviceDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceDetail", new { id = deviceDetail.Id }, deviceDetail);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceDetail = await _context.DeviceDetails.SingleOrDefaultAsync(m => m.Id == id);
            if (deviceDetail == null)
            {
                return NotFound();
            }

            _context.DeviceDetails.Remove(deviceDetail);
            await _context.SaveChangesAsync();

            return Ok(deviceDetail);
        }

        private bool DeviceDetailExists(Guid id)
        {
            return _context.DeviceDetails.Any(e => e.Id == id);
        }
    }
}