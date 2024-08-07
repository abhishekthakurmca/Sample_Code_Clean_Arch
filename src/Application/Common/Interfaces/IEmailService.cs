using Anubis.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Interfaces
{
    public interface IEmailService
    {
       
        Task<string> SendReminderConfirmationEmail();
        Task<string> SendChangedFreightTypeEmail(ShipmentConfirmation shipmentConfirmation);
        Task<string> SendInterCompany(InterCompanyEmail email);
        Task<string> SendConfirmationForNeedHelp(ShipmentConfirmation shipmentConfirmation);
    }
    public class OptionVm
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public int ReceivingTypeID { get; set; }
    }
}
