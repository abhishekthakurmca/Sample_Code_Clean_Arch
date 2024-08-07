using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anubis.Application.ExportFiles.FreightProfiles.Company;
using Anubis.Application.ExportFiles.FreightProfiles.TeamMember;
using Anubis.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anubis.WebUI.Controllers
{
    [Authorize]
    public class ExportController : ApiController
    {
        [HttpPost("[action]")]
        public async Task<FileResult> ExportCompany(ExportAllCompanyQuery query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }
        [HttpPost("[action]")]
        public async Task<FileResult> ExportContact(ExportAllContactQuery query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportFTL(ExportAllFTLQuery query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }
        [HttpPost("[action]")]
        public async Task<FileResult> ExportLTL(ExportAllLTLQuery query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportFreightCode(ExportAllFreightCode query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportLinkedClient(ExportAllCompanyLinkedClient query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportHistoricFreightRates(ExportAllHistoricFreightRates query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }



        [HttpPost("[action]")]
        public async Task<FileResult> ExportApprovalPending(ExportAllApprovalPending query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

    

        [HttpPost("[action]")]
        public async Task<FileResult> ExportRejectedShipments(ExportAllRejectedShipments query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportTenderedShipments(ExportAllTenderedShipments query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }


 

        [HttpPost("[action]")]
        public async Task<FileResult> ExportApprovedInvoice(ExportAllApprovedInvoice query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportApprovedRequiredInvoices(ExportAllApprovedRequiredInvoices query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

      
        [HttpPost("[action]")]
        public async Task<FileResult> ExportWeight(ExportAllWeight query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportMiles(ExportAllMiles query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportAccessorial(ExportAllAccessorial query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }

   

        
        [HttpPost("[action]")]
        public async Task<FileResult> ExportTeamMembers(ExportAllTeamMember query)
        {
            var data = await Mediator.Send(query);
            return File(data.Content, data.ContentType, data.FileName);
        }
        [HttpPost("[action]")]
        public async Task<FileResult> ExportFTLLanes(ExportFTLLanes query)
        {
            var data = await Mediator.Send(query);
            return File(data.Content, data.ContentType, data.FileName);
        }
        [HttpPost("[action]")]
        public async Task<FileResult> ExportLTLLanes(ExportLTLLanes query)
        {
            var data = await Mediator.Send(query);
            return File(data.Content, data.ContentType, data.FileName);
        }
     

   
    }
}
