﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
