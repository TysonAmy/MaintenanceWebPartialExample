using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Services
{
    /// <summary>
    /// This is used to allow emails to be sent out in MS Identity
    /// Referance for IEmailSender
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.ui.services.iemailsender?view=aspnetcore-6.0
    /// Reference to create EmailSender
    /// https://medium.com/@MisterKevin_js/enabling-email-verification-in-asp-net-core-identity-ui-2-1-b87f028a97e0
    /// </summary>
    public class EmailSender : IEmailSender
    {
        // Our private configuration variables
        public string host;
        public int port;
        public bool enableSSL;
        public string userName;
        public string password;

        //public EmailSender()
        //{

        //}

        /// <summary>
        /// Get the parameterized configuration
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="port">Port</param>
        /// <param name="enableSSL">Is SSL Enabled</param>
        /// <param name="userName">User name</param>
        /// <param name="password">password</param>
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        /// <summary>
        /// Using configuration to send the email by using SmtpClient
        /// </summary>
        /// <param name="email">Receivers Email Address</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="htmlMessage">Email Body</param>
        /// <returns>A <see cref="Task"/> that sends the email</returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}
