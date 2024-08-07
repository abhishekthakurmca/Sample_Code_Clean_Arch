using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.FTL
{
    public class RemoveAndDeactiveFTLCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public bool IsDeactive { get; set; }
        public string isDeleted { get; set; }
    }
    public class RemoveAndDeactiveFTLCommandHandler : IRequestHandler<RemoveAndDeactiveFTLCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RemoveAndDeactiveFTLCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RemoveAndDeactiveFTLCommand request, CancellationToken cancellationToken)
        {

            var ftlCompany = await _context.Set<FTL_Company>().FindAsync(request.Id);
            var existsRecord = new FTL_Company();
            if (request.Id > 0 && ftlCompany == null)
                return Result.Failure(new string[] { "FTL not found" });
            if (request.Action.ToLower() == "delete")
                ftlCompany.IsDeleted = true;
            else if (request.Action.ToLower() == "deactivate")
                ftlCompany.IsActive = request.IsDeactive;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
