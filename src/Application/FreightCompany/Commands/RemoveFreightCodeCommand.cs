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

namespace Anubis.Application.FreightCompany.Commands
{
    public class RemoveFreightCodeCommand : IRequest<Result>
    {
        public long FreightCodeId { get; set; }
    }
    public class RemoveFreightCodeCommandHandler : IRequestHandler<RemoveFreightCodeCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemoveFreightCodeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemoveFreightCodeCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Set<FreightCompanyCodes>()
                .FirstOrDefaultAsync(sl => sl.Id == request.FreightCodeId, cancellationToken);
            if (contact == null)
                return Result.Failure(new string[] { "Code was not available" });

            contact.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
