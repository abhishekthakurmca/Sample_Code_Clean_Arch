using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.TeamMember.Command
{
    public class AssignShipmentToTeamMemberCommand : IRequest<Result>
    {
        public long ShipmentId { get; set; }
        public int TeamMemberId { get; set; }
    }
    public class AssignShipmentToTeamMemberCommandHandler : IRequestHandler<AssignShipmentToTeamMemberCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public AssignShipmentToTeamMemberCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AssignShipmentToTeamMemberCommand request, CancellationToken cancellationToken)
        {
            var assignedShipment = await _context.Set<TeamMemberShipments>().FirstOrDefaultAsync(x => x.ShipmentId == request.ShipmentId);
            assignedShipment ??= new TeamMemberShipments();
            assignedShipment.ShipmentId = request.ShipmentId;
            assignedShipment.TeamMember_Id = request.TeamMemberId;
            if (assignedShipment.Id == 0)
               await _context.Set<TeamMemberShipments>().AddAsync(assignedShipment);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
