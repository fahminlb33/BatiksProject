using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatiksProject.Models
{
    public class StatusCodeViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorStatusCode { get; set; }

        public string OriginalURL { get; set; }
        public bool ShowOriginalURL => !string.IsNullOrEmpty(OriginalURL);
    }
}
