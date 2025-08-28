using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majlis.Common.Models.Api.Request
{
    public class SendEmailRequest
    {
        public string Email { get; set; }
        public string? Name { get; set; }
        public string Subject { get; set; }
        public string EmailHtml { get; set; }
        public string RegistrationLink { get; set; }
        public string EmailType { get; set; }
        public string? Message { get; set; } // Only for feedback type
    }

}
