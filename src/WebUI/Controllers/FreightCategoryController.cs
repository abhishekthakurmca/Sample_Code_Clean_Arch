using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anubis.Application.Common.Models;
using Anubis.Application.ExportFiles.FreightProfiles.Company;
using Anubis.Application.FreightCategoryDB.Command;
using Anubis.Application.FreightCategoryDB.Query;
using Anubis.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anubis.WebUI.Controllers
{
    [Authorize]
    public class FreightCategoryController : ApiController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<FreightCategory>>> GetCategories(GridCategoryQuery query)
        {
            return Ok(await Mediator.Send(query));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddEditCategory(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveCategory(RemoveCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
