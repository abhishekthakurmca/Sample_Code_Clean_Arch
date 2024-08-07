using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.LinkedClients
{
    public class RemoveLinkedClientCommand : IRequest<Result>
    {
        public long LinkedId { get; set; }
    }
    public class RemoveLinkedClientCommandHandler : IRequestHandler<RemoveLinkedClientCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemoveLinkedClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemoveLinkedClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Set<CompanyLinkedClients>()
                .FirstOrDefaultAsync(sl => sl.Id == request.LinkedId, cancellationToken);
            if (client == null)
                return Result.Failure(new string[] { "Linked client was not available" });
            client.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
