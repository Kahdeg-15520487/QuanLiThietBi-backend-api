using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<DeviceCategory> DeviceCategorys { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceDetail> DeviceDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            var deviceCategorys = new[]
            {
                new DeviceCategory(){ Id = Guid.NewGuid(), CategoryName = "Monitor"},
                new DeviceCategory(){ Id = Guid.NewGuid(), CategoryName = "Keyboard"},
                new DeviceCategory(){ Id = Guid.NewGuid(), CategoryName = "Computer"},
                new DeviceCategory(){ Id = Guid.NewGuid(), CategoryName = "VGA cable"},
                new DeviceCategory(){ Id = Guid.NewGuid(), CategoryName = "Power cable"},
            };

            builder.Entity<DeviceCategory>().HasData(deviceCategorys);

            var deviceDetails = new[]
            {
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[0].Id, Name = "Dell Monitor"},
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[0].Id, Name = "Dell Wide Monitor"},
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[1].Id, Name = "Logitech K120 Keyboard"},
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[2].Id, Name = "Dell OEM Computer"},
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[3].Id, Name = "VGA cable"},
                new DeviceDetail(){ Id = Guid.NewGuid(), DeviceCategoryId = deviceCategorys[4].Id, Name = "Power cable"},
            };

            builder.Entity<DeviceDetail>().HasData(deviceDetails);

            var devices = new[]
            {
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[0].Id, Quantity = 10},
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[1].Id, Quantity = 10},
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[2].Id, Quantity = 10},
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[3].Id, Quantity = 10},
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[4].Id, Quantity = 10},
                new Device(){ Id = Guid.NewGuid(), DeviceDetailId = deviceDetails[5].Id, Quantity = 10},
            };

            builder.Entity<Device>().HasData(devices);
        }
    }
}
