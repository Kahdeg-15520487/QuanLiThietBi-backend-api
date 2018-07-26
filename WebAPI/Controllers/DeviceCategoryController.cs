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
    [Route("api/DeviceCategory")]
    public class DeviceCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<DeviceCategory> GetDeviceCategorys()
        {
            return _context.DeviceCategorys;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.DeviceCategorys.SingleOrDefaultAsync(m => m.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceCategory([FromRoute] Guid id, [FromBody] DeviceCategory deviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceCategoryExists(id))
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
        public async Task<IActionResult> PostDeviceCategory([FromBody] DeviceCategory DeviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DeviceCategorys.Add(DeviceCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceCategory", new { id = DeviceCategory.Id }, DeviceCategory);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceCategory = await _context.DeviceCategorys.SingleOrDefaultAsync(m => m.Id == id);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            _context.DeviceCategorys.Remove(deviceCategory);
            await _context.SaveChangesAsync();

            return Ok(deviceCategory);
        }

        private bool DeviceCategoryExists(Guid id)
        {
            return _context.DeviceCategorys.Any(e => e.Id == id);
        }
    }
}