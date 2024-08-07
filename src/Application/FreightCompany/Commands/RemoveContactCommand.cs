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
    public class RemoveContactCommand : IRequest<Result>
    {
        public long ContactId { get; set; }
    }
    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemoveContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Set<CompanyContact>()
                .FirstOrDefaultAsync(sl => sl.Id == request.ContactId, cancellationToken);
            if (contact == null)
                return Result.Failure(new string[] { "contact was not available" });
            contact.IsDeleted = true;
            //_context.Set<CompanyContact>().Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
