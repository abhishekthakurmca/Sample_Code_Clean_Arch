using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.Dtos.TeamMember;
using Anubis.Application.TeamMember.Command;
using Anubis.Application.TeamMember.Query;
using Anubis.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anubis.WebUI.Controllers
{
    [Authorize]
    public class TeamMemberController : ApiController
    {
      
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<TeamMemberDto>>> GetAllTeamMembers(GetAllTeamMemberGridQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AddUpdateTeamMember(AddUpdateTeamMemberCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> RemoveTeamMember(RemoveTeamMemberCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<OptionVm>>> GetAllTeamMembersDDL()
        {
            return Ok(await Mediator.Send(new GetAllTeamMembersDDLQuery()));
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> AssignShipmentToTeamMember(AssignShipmentToTeamMemberCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
