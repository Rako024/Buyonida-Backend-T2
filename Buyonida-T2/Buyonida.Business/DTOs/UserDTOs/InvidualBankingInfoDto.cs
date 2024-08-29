using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record InvidualBankingInfoDto
    {
        public string Voen { get; set; }
        public string BankName { get; set; }
        public string BankVoen { get; set; }
        public string Code { get; set; }
        public string Mh { get; set; }
        public string SwiftBik { get; set; }
        public string Hh { get; set; }
        public string UserId { get; set; }
    }
}
