using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.WGS
{
    public class InsertLabourQuoteCommand : IRequest<Result>
    {
        public long CompanyId { get; set; }
        public decimal LabourCost { get; set; }
    }
    public class InsertLabourQuoteCommandHandler : IRequestHandler<InsertLabourQuoteCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public InsertLabourQuoteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(InsertLabourQuoteCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Set<Domain.Entities.FreightCompany>().FindAsync(request.CompanyId);
            if (company == null)
                return Result.Failure(new string[] { "Please refresh your data and try again." });
            company.LabourCost = request.LabourCost;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
