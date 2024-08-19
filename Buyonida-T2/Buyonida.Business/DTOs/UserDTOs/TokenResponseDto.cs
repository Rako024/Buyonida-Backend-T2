using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string UserName { get; set; }

    }
}
