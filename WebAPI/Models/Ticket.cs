using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
    }
}
