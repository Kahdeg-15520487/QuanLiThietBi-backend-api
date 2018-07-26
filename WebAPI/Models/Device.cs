using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public Guid DeviceDetailId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public DeviceDetail DeviceDetail { get; set; }
    }
}
