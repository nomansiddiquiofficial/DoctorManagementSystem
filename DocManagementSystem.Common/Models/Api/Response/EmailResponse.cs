using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocManagementSystem.Common.Models.Api.Response
{
    public class EmailTemplateResponse
    {
        public string EmailHtml { get; set; }
        public string ConfirmationMessage { get; set; }
        public string EmailSubject { get; set; }
        public string EmailRegistraionLink { get; set; }
    }

    public class SmtpSettingsResponse
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FromName { get; set; }
        public bool EnableSSL { get; set; }
    }

}
