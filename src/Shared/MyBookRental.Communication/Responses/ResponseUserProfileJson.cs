﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Communication.Responses
{
    public class ResponseUserProfileJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
