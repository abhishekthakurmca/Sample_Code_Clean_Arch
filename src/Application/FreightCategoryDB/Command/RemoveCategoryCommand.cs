using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCategoryDB.Command
{

    public class RemoveCategoryCommand : IRequest<Result>
    {
        public long Id { get; set; }
    }

    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RemoveCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = await _context.Set<Domain.Entities.FreightCategory>().FindAsync(request.Id);
            var existsRecord = new Domain.Entities.FreightCompanyCodes();
            if (request.Id > 0 && category == null)
                return Result.Failure(new string[] { "Category not found" });
            existsRecord = await _context.Set<Domain.Entities.FreightCompanyCodes>().FirstOrDefaultAsync(x => x.FreightCategory_Id == request.Id);

            if (existsRecord != null)
                return Result.Failure(new string[] { $"Category already assigned  with company Code {existsRecord.Name}" });
            //_context.Set<Domain.Entities.FreightCategory>().Remove(category);
            category.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
