using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Configuration;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Helpers;
using JS.Suite.BusinessLogic.Templating;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Messaging.Email
{
    /// <summary>
    /// Email Helper
    /// </summary>
    public class EmailHelper : SingletonBase<EmailHelper>
    {
        /// <summary>
        /// Sends the email of the specified type.
        /// </summary>
        /// <param name="emailType">Type of the email.</param>
        /// <param name="model">The model.</param>
        /// <param name="toAddresses">To addresses.</param>
        /// <returns></returns>
        public IProcessResult Send(EmailTypes emailType, object model, IEnumerable<string> toAddresses)
        {
            string emailTypeName = EnumHelper.Current.GetName(emailType);

            string emailAccount = GetEmailAccount(emailType);

            string body = EmailTemplateEngine.Current.Generate(emailTypeName, model);
            string subject = EmailTemplateEngine.Current.Generate(String.Format("{0}Subject", emailTypeName), model);

            return Send(emailAccount, body, subject, toAddresses);
        }

        /// <summary>
        /// Sends the email using the specified Account.
        /// </summary>
        /// <param name="emailAccount">The email account.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="toAddresses">To addresses.</param>
        /// <returns></returns>
        private IProcessResult Send(string emailAccount, string body, string subject, IEnumerable<string> toAddresses)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (string toAddress in toAddresses)
            {
                mailMessage.To.Add(toAddress);
            }

            return Send(emailAccount, mailMessage);
        }

        /// <summary>
        /// Sends the email using the specified Account.
        /// </summary>
        /// <param name="emailAccount">The email account.</param>
        /// <param name="mailMessage">The mail message.</param>
        /// <returns></returns>
        private IProcessResult Send(string emailAccount, MailMessage mailMessage)
        {
            BackgroundProcessHelper.Current.Trigger((cancelationToken) =>
            {
                EmailAccountSection emailAccountConfigurationSection = (EmailAccountSection)ConfigurationManager.GetSection("emailAccounts");

                EmailAccountElement emailAccountConfiguration = emailAccountConfigurationSection.EmailAccounts[emailAccount];

                NetworkCredential credentials = new NetworkCredential(emailAccountConfiguration.UserName, emailAccountConfiguration.Password, "");

                string fromDisplayName = emailAccountConfiguration.FromDisplayName;

                if (ConfigurationHelper.Current.GetEnvironment() == Environments.Sandbox)
                {
                    fromDisplayName = String.Join(" - ", "Sandbox", fromDisplayName);
                }

                mailMessage.From = new MailAddress(emailAccountConfiguration.FromAddress, fromDisplayName);

                SmtpClient SmtpClient = new SmtpClient(emailAccountConfiguration.SmtpAddress);

                SmtpClient.Port = emailAccountConfiguration.SmtpPort;
                SmtpClient.EnableSsl = emailAccountConfiguration.UseSSL;
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = credentials;

                SmtpClient.Send(mailMessage);
            });

            return new ProcessResult(ResultCodes.Success);
        }

        private string GetEmailAccount(EmailTypes emailType)
        {
            switch (emailType)
            {
                case EmailTypes.ConfirmEmail:
                case EmailTypes.ResetPassword:
                    return EmailAccounts.Support;
                case EmailTypes.AppMessage:
                default:
                    return EmailAccounts.AppMessenger;
            }
        }
    }
}
