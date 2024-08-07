using Anubis.Application.Common.Exceptions;
using Anubis.Application.Common.Interfaces;
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

namespace Anubis.Application.FreightCompany.Commands
{
    public class CreateCompanyCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }
        //public List<int> ShipmentFreightTypes { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTerm { get; set; }
        public bool FTL { get; set; }
        public bool LTL { get; set; }
        public bool WGS { get; set; }
        public string TenderEmail { get; set; }
    }
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.FreightCompany
            {
                Name = request.Name,
                ShortName = request.ShortName,
                IsActive = request.IsActive,
                Fax = request.Fax,
                Phone = request.Phone,
                Created = DateTime.UtcNow,
                //Billing address
                Address = request.Address,
                Address1 = request.Address1,
                City = request.City,
                Country = request.Country,
                PaymentMethod = request.PaymentMethod,
                PaymentTerm = request.PaymentTerm,
                Region = request.Region,
                State = request.State,
                ZipCode = request.ZipCode,
                FTL = request.FTL,
                LTL = request.LTL,
                WGS = request.WGS,
                TenderEmail = request.TenderEmail
            };
            //foreach (var item in request.ShipmentFreightTypes)
            //    entity.ShipmentFreightTypes.Add(new CompanyShipmentFreightTypes { ShipmentFreight_Id = item });
            _context.Set<Domain.Entities.FreightCompany>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
