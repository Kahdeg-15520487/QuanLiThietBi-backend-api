using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class DeviceDetail
    {
        public Guid Id { get; set; }
        public Guid DeviceCategoryId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DeviceCategory DeviceCategory { get; set; }
    }

    public class DeviceCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}
