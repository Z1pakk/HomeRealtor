using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class AddOrderModel
    {
        public int ApartId { get; set; }

        public bool Status { get; set; }

        public string UserId { get; set; }

        public string RealtorId { get; set; }

        public string Message { get; set; }
    }
}
