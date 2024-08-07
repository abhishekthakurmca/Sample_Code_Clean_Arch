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

namespace Anubis.Application.FreightCompany.Commands.WGS
{
    
    public class RemoveWGSAccesrailsCommand : IRequest<Result>
    {
        public long Id { get; set; }
    }
    public class RemoveWGSAccesrailsCommandHandler : IRequestHandler<RemoveWGSAccesrailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemoveWGSAccesrailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemoveWGSAccesrailsCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Set<WGSAccesrails>()
                .FirstOrDefaultAsync(sl => sl.Id == request.Id , cancellationToken);
            if (contact == null)
                return Result.Failure(new string[] { "Code was not available" });

            contact.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
