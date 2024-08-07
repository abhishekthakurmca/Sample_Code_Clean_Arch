using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands
{
    public class CreateCompanyContactCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public int ContactType_Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Ext { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class CreateCompanyContactCommandHandler : IRequestHandler<CreateCompanyContactCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateCompanyContactCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateCompanyContactCommand request, CancellationToken cancellationToken)
        {
            var companyContact = await _context.Set<Domain.Entities.CompanyContact>().FindAsync(request.Id);

            if (request.Id > 0 && companyContact == null)
                return Result.Failure(new string[] { "Contact not found" });

            companyContact = companyContact == null ? new Domain.Entities.CompanyContact() : companyContact;
            companyContact.FName = request.FName;
            companyContact.LName = request.LName;
            companyContact.Company_Id = request.Company_Id;
            companyContact.Email = request.Email;
            companyContact.Ext = request.Ext;
            companyContact.Fax = request.Fax;
            companyContact.Mobile = request.Mobile;
            companyContact.IsDeleted = false;
            companyContact.ContactType_Id = request.ContactType_Id;
            companyContact.Notes = request.Notes;
            companyContact.Title = request.Title;
            companyContact.CreatedDate = DateTime.UtcNow;

            if (request.Id == 0)
                _context.Set<Domain.Entities.CompanyContact>().Add(companyContact);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
