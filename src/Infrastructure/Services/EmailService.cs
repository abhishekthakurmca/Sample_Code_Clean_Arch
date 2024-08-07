using Anubis.Application.Common.Interfaces;
using Anubis.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Anubis.Domain.Enums;
using ClosedXML.Excel;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using SendGrid;

namespace Anubis.Infrastructure.Services
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class EmailParam
    {
        public bool IsEmail { get; set; }
        public RecipientEmails RecipientEmails { get; set; }
        public bool isAdhoc { get; set; }
        public bool IsHTML { get; set; }
        public string RequestJSON { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
    public class RecipientEmails
    {
        public RecipientEmails()
        {
            To = new List<string>();
            CC = new List<string>();
            BCC = new List<string>();
        }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        public EmailService(IHostingEnvironment environment, IConfiguration config, IApplicationDbContext context, IIdentityService identityService)
        {
            _environment = environment;
            _config = config;
            _context = context;
            _identityService = identityService;
        }
       
  

     
      



    

        

        private async Task<string> SendEmailByPheme(EmailParam email)
        {
            ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var Baseurl = _config["Pheme:WebApi"];
            var httpClient = new HttpClient();
            string token = await _identityService.GetAccessToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var url = $"{Baseurl}api/NotificationAPI/SendNotification";
            var bodycontent = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, bodycontent);
            if (response.StatusCode.ToString() == "OK")
            {
                return "Accepted";
            }
            else
            {
                return "";
            }
        }

        // Method for send reminder email for Pending Tender list in excel file. 
        public async Task<string> SendReminderConfirmationEmail()
        {
            try
            {
                var pendingShipments = _context.Set<Route>().Include(x => x.RouteShipments).Include(x => x._Company).Where(x => x.RouteShipments.Any(s => s.Tendered == (int)Tender.SendEmail && x.Id == s.Route_Id)).ToList().GroupBy(e => e._Company.Name).ToDictionary(e => e.Key, g => g.ToList());
                var sendTo = string.Empty;
                foreach (var pShipments in pendingShipments)
                {
                    var carriers = pShipments.Key;
                    var items = pShipments.Value;

                    DataTable dt = new DataTable("Grid");
                    dt.Columns.AddRange(new DataColumn[11] { new DataColumn("SHIPMENTID"),
                                    new DataColumn("ROUTEID"),
                                    new DataColumn("CLIENTNAME"),
                                    new DataColumn("CITY"),
                                    new DataColumn("STATE"),
                                    new DataColumn("SITE"),
                                    new DataColumn("PICKUPDATE"),
                                    new DataColumn("DOCKDATE"),
                                    new DataColumn("CARRIER"),
                                    new DataColumn("RECEIVINGTYPENAME"),
                                    new DataColumn("CRM")

                            });

                    foreach (var item in items)
                    {
                        var SiteName = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().SiteName : "";
                        var ReceivingTypeName = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().ReceivingTypeName : "";
                        var ShipmentId = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().ShipmentId.ToString() : "";
                        var ClientName = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().Client.ToString() : "";
                        var City = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().CollectionPointCity.ToString() : "";
                        var State = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().CollectionPointState.ToString() : "";
                        var CRMName = item.RouteShipments.Count > 0 ? item.RouteShipments.FirstOrDefault().CRMName : "";
                        sendTo = item._Company.TenderEmail;
                        dt.Rows.Add(ShipmentId, item.Id, ClientName, City, State, SiteName, item.PickUpDate, item.DockDate, item._Company.Name, ReceivingTypeName, CRMName);

                    }
                    byte[] bytes = null;
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            bytes = stream.ToArray();
                        }
                    }
                    var apiKey = _config.GetValue<string>("Anubis:WebApiKey");
                    var client = new SendGridClient(apiKey);
                    string emailBody = string.Empty;
                    emailBody = "This is the reminder email with an excel attachment of pending Tenders.";
                    var attachments = new List<Attachment>();
                    var attachment = new Attachment()
                    {
                        Content = Convert.ToBase64String(bytes),
                        Filename = $"TenderPendingDetails-{DateTime.Now.Ticks}.xlsx",
                        Type = "application/vnd.ms-excel"
                    };
                    attachments.Add(attachment);
                    var Subject = "ERI Pending Tenders";
                    var content = new EmailParam()
                    {
                        isAdhoc = true,
                        IsEmail = true,
                        IsHTML = true,
                        RequestJSON = "{'notificationBody':'" + emailBody + "','emailSubject':'" + Subject + "','Source':'Anubis'}",
                        RecipientEmails = new RecipientEmails()
                        {
                            To = new List<string> { sendTo },
                            BCC = new List<string> { },
                            CC = new List<string> { },
                        },
                        Attachments = attachments
                    };
                    await SendEmailByPheme(content);
                }
                return " Email send successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SendInterCompany(InterCompanyEmail email)
        {
            string body = string.Empty;
            var path = $"{_environment.ContentRootPath}\\wwwroot\\Templates\\InterCompanyEmailTemplate.html";
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            string[] interCompanyEmail = _config["InterCompanyFailureEmail"].Split(",");
            body = body.Replace("#Message", email.Message);
            var Subject = $"No package on Intercompany Shipment";
            var ToEmail = interCompanyEmail.ToList();
            var content = new EmailParam()
            {
                isAdhoc = true,
                IsEmail = true,
                IsHTML = true,
                RequestJSON = "{'notificationBody':'" + body + "','emailSubject':'" + Subject + "','Source':'Anubis'}",
                RecipientEmails = new RecipientEmails()
                {
                    To = ToEmail,
                    BCC = new List<string> { },
                    CC = new List<string> { },
                },
            };
            var response = await SendEmailByPheme(content);
            return response;
        }

        #region  4686 H.03 Freight Type Alerts      
        public async Task<string> SendChangedFreightTypeEmail(ShipmentConfirmation shipmentConfirmation)
        {
            string body = string.Empty;
            var path = $"{_environment.ContentRootPath}\\wwwroot\\Templates\\FreightTypeAlert.html";
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            string[] crmEmail = _config["CRMEmail"].Split(",");
            string[] logisticEmail = _config["LogisticEmail"].Split(",");
            string shipmentId = !string.IsNullOrEmpty(shipmentConfirmation.ShipmentIds) ? shipmentConfirmation.ShipmentIds : shipmentConfirmation.ShipmentId.ToString();
            body = body.Replace("#CLIENTID", shipmentConfirmation.ClientId.ToString());
            body = body.Replace("#CLIENT", shipmentConfirmation.Client);
            body = body.Replace("#SHIPMENTID", shipmentId);
            body = body.Replace("#ORIGINALFREIGHTTYPE", shipmentConfirmation.ExistingFreightType);
            body = body.Replace("#NEWFREIGHTTYPE", shipmentConfirmation.FreightType);
            var Subject = $"ERI Freight Type Changed from {shipmentConfirmation.ExistingFreightType} to {shipmentConfirmation.FreightType} for SHIPMENT ID " + shipmentConfirmation.ShipmentId.ToString() + ")";
            var ToEmail = !string.IsNullOrEmpty(shipmentConfirmation.CRMEmail) ? new List<string> { shipmentConfirmation.CRMEmail } : crmEmail.ToList();
            //Check Duplicate Email 
            var CCEmail = logisticEmail.Where(x => !ToEmail.Contains(x)).ToList();
            var content = new EmailParam()
            {
                isAdhoc = true,
                IsEmail = true,
                IsHTML = true,
                RequestJSON = "{'notificationBody':'" + body + "','emailSubject':'" + Subject + "','Source':'Anubis'}",
                RecipientEmails = new RecipientEmails()
                {
                    To = ToEmail,
                    BCC = new List<string> { },
                    CC = CCEmail
                },
            };
            var response = await SendEmailByPheme(content);
            return response;
        }

        public async Task<string> SendConfirmationForNeedHelp(ShipmentConfirmation shipmentConfirmation)
        {
            string body = string.Empty;
            var path = $"{_environment.ContentRootPath}\\wwwroot\\Templates\\NeedHelpNoteTemplate.html";
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            var NeedHelpEmail = _config.GetValue<string>("NeedHelpEmail");
            string shipmentId = !string.IsNullOrEmpty(shipmentConfirmation.ShipmentIds) ? shipmentConfirmation.ShipmentIds : shipmentConfirmation.ShipmentId.ToString();
            body = body.Replace("#CLIENT", shipmentConfirmation.Client);
            body = body.Replace("#ROUTEID", $"R{shipmentConfirmation.RouteId.ToString()}");
            body = body.Replace("#NOTES", shipmentConfirmation.Notes);
            body = body.Replace("#SHIPMENTID", shipmentId);
            var Subject = "Need Help";
            var content = new EmailParam()
            {
                isAdhoc = true,
                IsEmail = true,
                IsHTML = true,
                RequestJSON = "{'notificationBody':'" + body + "','emailSubject':'" + Subject + "','Source':'Anubis'}",
                RecipientEmails = new RecipientEmails()
                {
                    To = new List<string> { NeedHelpEmail },
                    BCC = new List<string> { },
                    CC = new List<string> { },
                }
            };
            var response = await SendEmailByPheme(content);
            return response;
        }
        #endregion
    }
}
