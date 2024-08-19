using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Exceptions
{
    public class GlobalAppException : Exception
    {
        public GlobalAppException() : base() { }
        public GlobalAppException(string message) : base(message) { }
        public GlobalAppException(string message, Exception innerException) : base(message, innerException) { }
    }
}
