using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Core.Entities
{
    public class Personal : AppUser
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
    }
}
