using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Helpers
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string ExceptionMessage { get; set; }
        public dynamic Result { get; set; }
    }
}
