using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string ExceptionMessage { get; set; }
        public dynamic Result { get; set; }
    }
}
