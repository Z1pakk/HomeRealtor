﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public int ApartId { get; set; }

        public bool Status { get; set; }

        public string UserId { get; set; }

        public string RealtorId { get; set; }

        public string Message { get; set; }
    }
}
