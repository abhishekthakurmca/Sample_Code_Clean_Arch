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

namespace Anubis.Application.FreightCompany.Commands.Documents
{
    public class CompanyDocumentsCommand : IRequest<Result>
    {
        public long Company_Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }

    }
    public class CompanyDocumentsCommandHandler : IRequestHandler<CompanyDocumentsCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CompanyDocumentsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CompanyDocumentsCommand request, CancellationToken cancellationToken)
        {
            var doc = new CompanyDocuments();
            doc.Company_Id = request.Company_Id;
            doc.DocumentName = request.Name;
            doc.DocumentPath = request.Path;
            doc.DocumentType = request.Type;
            doc.IsDeleted = false;
            _context.Set<CompanyDocuments>().Add(doc);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
