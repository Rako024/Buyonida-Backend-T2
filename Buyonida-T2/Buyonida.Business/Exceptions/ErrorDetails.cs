﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }


        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
