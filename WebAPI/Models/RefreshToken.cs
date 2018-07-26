using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
