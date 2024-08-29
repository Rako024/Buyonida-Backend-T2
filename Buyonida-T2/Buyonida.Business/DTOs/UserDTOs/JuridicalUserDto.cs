using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record JuridicalUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IDNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string MobilNumber { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Voen { get; set; }
        public string BankName { get; set; }
        public string BankVoen { get; set; }
        public string Code { get; set; }
        public string Mh { get; set; }
        public string SwiftBik { get; set; }
        public string Hh { get; set; }
        public string CompanyName { get; set; }
        public string Director { get; set; }
    }
}
