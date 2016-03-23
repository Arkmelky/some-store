using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Abstract;

namespace Domain.Models.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username,emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .Append("New order")
                    .Append("---------")
                    .Append("Goods:");

                foreach (var item in cart.Items)
                {
                    var subtotal = item.StoreProduct.Price*item.Quantity;
                    body.AppendFormat("{0} x {1} (Calc: {2:c})", item.Quantity, item.StoreProduct.Price, subtotal);
                }

                body.AppendFormat("Total price: {0:c}", cart.CalcTotalPrice())
                    .Append("----")
                    .Append("Delivery:")
                    .Append(shippingDetails.Name)
                    .Append(shippingDetails.Address1)
                    .Append(shippingDetails.Address2 ?? "")
                    .Append(shippingDetails.Address3 ?? "")
                    .Append(shippingDetails.City)
                    .Append(shippingDetails.Country)
                    .Append("------")
                    .AppendFormat("Use a gift box: {0}",shippingDetails.GiftWrap ? "Yes" : "No");
                MailMessage mail = new MailMessage(
                    emailSettings.MailFrom,
                    emailSettings.MailTo,
                    "New order sent",
                    body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mail.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mail);
            }
        }
    }

    public class EmailSettings
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool WriteAsFile { get; set; }
        public string FileLocation { get; set; }

        public EmailSettings()
        {
            MailTo = "arkady.soloveyko@gmail.com";
            MailFrom = "somestore@mail.com";
            UseSsl = true;
            Username = "Username";
            Password = "Password";
            ServerName = "someserv.com";
            ServerPort = 123;
            WriteAsFile = true;
            FileLocation = @"G:\some_store_mails";
        }
    }
}
