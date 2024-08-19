using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;
        public string? IDNumber { get; set; } = null;
        public DateOnly? Birthday { get; set; } = null;
        public string? MobilNumber { get; set; } = null;
        public string? Adress { get; set; } = null;
        public string? City { get; set; } = null;
        public string? District { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
}
