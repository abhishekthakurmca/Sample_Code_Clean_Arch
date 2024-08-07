using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anubis.Application.Common.Models;
using Anubis.Application.ExportFiles.FreightProfiles.Company;
using Anubis.Application.FreightCompany.Commands;
using Anubis.Application.FreightCompany.Commands.Documents;
using Anubis.Application.FreightCompany.Commands.FTL;
using Anubis.Application.FreightCompany.Commands.LinkedClients;
using Anubis.Application.FreightCompany.Commands.LTL;
using Anubis.Application.FreightCompany.Commands.WGS;
using Anubis.Application.FreightCompany.Queries.Documents;
using Anubis.Application.FreightCompany.Queries.Dropdowns;
using Anubis.Application.FreightCompany.Queries.FTL;
using Anubis.Application.FreightCompany.Queries.GetCompanyContacts;
using Anubis.Application.FreightCompany.Queries.GetFreightCompanies;
using Anubis.Application.FreightCompany.Queries.LinkedClients;
using Anubis.Application.FreightCompany.Queries.LTL;
using Anubis.Application.FreightCompany.Queries.WGS;
using Anubis.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Anubis.Application.FreightCompany.Queries.HistoricFreight;
using Anubis.Application.FreightCompany.Queries.Permissions;

namespace Anubis.WebUI.Controllers
{
    [Authorize]
    public class FreightCompanyController : ApiController
    {
        #region Company Details
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<FreightCompany>>> GetCompanys(GridCompanyQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<long>> EditCompany(UpdateCompanyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<long>> AddCompany(CreateCompanyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<FileResult> ExportWG(ExportAllCompanyQuery query)
        {
            var vm = await Mediator.Send(query);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }


        #endregion
        #region Company Contact Details
        [HttpGet("{id}")]
        public async Task<ActionResult<FreightCompanyEditDto>> GetCompanyDetails(int id)
        {
            return await Mediator.Send(new GetCompanyQuery() { Id = id });
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<CompanyContactDto>>> GetCompanyContacts(GridCompanyContactQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddCompanyContact(CreateCompanyContactCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveContact(RemoveContactCommand command)
        {
            return await Mediator.Send(command);
        }
        #endregion
        [HttpPost("[action]")]
        public async Task<ActionResult<BasicInformationCompanyDto>> GetCompanyBasicInformation(string ddlFor)
        {
            return await Mediator.Send(new BasicInformationDDLQuery() { ddlFor = ddlFor });
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<BasicInformationCompanyDto>> GetFreightCodeList(long company_Id)
        {
            return await Mediator.Send(new FreightCodeListQuery() { Company_Id = company_Id });
        }
        #region FTL details
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<FtlDTO>>> GetFTL(GridFTLQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditFTL(CreateFTLCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> ActiveDeleteFTL(RemoveAndDeactiveFTLCommand command)
        {
            return await Mediator.Send(command);
        }
        #endregion
        #region LTL details
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<LtlDto>>> GetLTL(GridLTLQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditLTL(CreateLTLCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> ActiveDeleteLTL(RemoveAndDeactiveLTLCommand command)
        {
            return await Mediator.Send(command);
        }

        #endregion

        #region WGS Details
        #region WGS Miles Crud
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<WGSMilesDto>>> GetMiles(GridWGSMilesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditWGSMiles(CreateWGSMilesCommand command)
        {
            return await Mediator.Send(command);
        }

        #endregion
        #region WGS Miles Crud
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<WGSCompanyWeights>>> GetWeights(GridWGSWeightQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditWGSWeight(CreateWGSWeightCommand command)
        {
            return await Mediator.Send(command);
        }
        #endregion
        #region WGC Details
        [HttpPost("[action]")]
        public async Task<ActionResult<GridWGSResult<WGSCompanyMiles, WGSCompanyWeights, WGSCompanyPrice>>> GetWGCGrid(GridWGSQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> UpdateWGCPrice(UpdateWGSWeightPricesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        #endregion

        #endregion

        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<FreightCompanyCodes>>> GetFreightCompanyCodes(GridFreightCodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditFreightCode(FreightCodeCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveCompanyCode(RemoveFreightCodeCommand command)
        {
            return await Mediator.Send(command);
        }
        #region WGSAccesrails

        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<Application.FreightCompany.Queries.WGS.WGSAccesrailsDto>>> GetWGSAccesrails(GridWGSAccesrailsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditWGSAccesrail(CreateWGSAccesrailsCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveWGSAccesrail(RemoveWGSAccesrailsCommand command)
        {
            return await Mediator.Send(command);
        }
        #endregion
        #region Linked CLients
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<CompanyLinkedClients>>> GetLinkedClients(GridLinkedClientsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<LinkedClientDetailsDto>> GetLinkedClientPreferences(GetLinkedClientDetailsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditLinkedClinets(CreateLinkedClientsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveLinkedClient(RemoveLinkedClientCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> InsertLabourCost(InsertLabourQuoteCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<Decimal>> GetLabourCost(long companyId)
        {
            return await Mediator.Send(new GetLabourCostQuery() { CompanyId=companyId });
        }
        #endregion
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<CompanyDocuments>>> GetCompanyDocuments(GridCompanyDocumentsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> SaveCompanyDocuments(CompanyDocumentsCommand command)
        {
            return await Mediator.Send(command);
        }

        #region Historic Freight Rates
        [HttpPost]
        public async Task<ActionResult<GridResult<FreightRates>>> GetHistoricFreightRates(GetHistoricFreightRatesQuery query)
        {
            return await Mediator.Send(query);
        }
        #endregion


        [HttpPost("[action]")]
        public async Task<ActionResult<List<LU_Permissions>>> GetPermissions(long Company_Id)
        {
            return await Mediator.Send(new GetPermissionsQuery(){ Company_Id = Company_Id });
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddUpdatePermissions(AddUpdatePermissionsCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
