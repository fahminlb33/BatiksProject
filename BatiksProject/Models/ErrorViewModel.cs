﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatiksProject.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Referrer { get; set; }
        public bool ShowReferrer => !string.IsNullOrEmpty(Referrer);

        public string ExceptionMessage { get; set; }
        public bool ShowExceptionMessage => !string.IsNullOrEmpty(ExceptionMessage);
    }
}
