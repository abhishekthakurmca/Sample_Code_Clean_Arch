using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCategoryDB.Command
{

    public class CreateCategoryCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Set<Domain.Entities.FreightCategory>().FindAsync(request.Id);
            var existsRecord = new Domain.Entities.FreightCategory();
            if (request.Id > 0 && category == null)
                return Result.Failure(new string[] { "Category not found" });

            else if (request.Id > 0 && category != null)
                existsRecord = await _context.Set<Domain.Entities.FreightCategory>().FirstOrDefaultAsync(x => x.Id != request.Id
                 && x.Name == request.Name && x.IsDeleted != true);
            else if (request.Id == 0 && category == null)
                existsRecord = await _context.Set<Domain.Entities.FreightCategory>().FirstOrDefaultAsync(x => x.Name == request.Name && x.IsDeleted != true);
            if (existsRecord != null)
                return Result.Failure(new string[] { "Category already exists." });
            category = category == null ? new Domain.Entities.FreightCategory() : category;
            category.Name = request.Name;
            category.IsDeleted = request.IsDeleted;
            if (request.Id == 0)
                _context.Set<Domain.Entities.FreightCategory>().Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
