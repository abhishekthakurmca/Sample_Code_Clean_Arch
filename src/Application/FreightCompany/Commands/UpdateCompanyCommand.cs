using Anubis.Application.Common.Interfaces;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands
{
    public class UpdateCompanyCommand : IRequest<long>
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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Set<Domain.Entities.FreightCompany>().FindAsync(request.Id);
            //var shipmentTypes = await _context.Set<Domain.Entities.CompanyShipmentTypes>().Where(x => x.Company_Id == request.Id).ToListAsync();
            if (company != null)
            {
                //var shipmentFreightTypes = await _context.Set<Domain.Entities.CompanyShipmentFreightTypes>().Where(x => x.Company_Id == company.Id).ToListAsync();
                company.Name = request.Name;
                company.ShortName = request.ShortName;
                company.IsActive = request.IsActive;
                company.Fax = request.Fax;
                company.Phone = request.Phone;
                company.LastModified = DateTime.UtcNow;
                company.Address = request.Address;
                company.Address1 = request.Address1;
                company.City = request.City;
                company.Country = request.Country;
                company.PaymentMethod = request.PaymentMethod;
                company.PaymentTerm = request.PaymentTerm;
                company.Region = request.Region;
                company.State = request.State;
                company.ZipCode = request.ZipCode;
                company.TenderEmail = request.TenderEmail;
                company.FTL = request.FTL;
                company.LTL = request.LTL;
                company.WGS = request.WGS;
                //if (company.ShipmentFreightTypes.Any())
                //{
                //    _context.Set<Domain.Entities.CompanyShipmentFreightTypes>().RemoveRange(shipmentFreightTypes.Where(x => !request.ShipmentFreightTypes.Contains(x.ShipmentFreight_Id)).ToList());
                //    company.ShipmentFreightTypes = company.ShipmentFreightTypes.Where(x => request.ShipmentFreightTypes.Contains(x.ShipmentFreight_Id)).ToList();
                //}
                //if (request.ShipmentFreightTypes != null && request.ShipmentFreightTypes.Any())
                //{
                //    var shipmentFreight = new List<CompanyShipmentFreightTypes>();
                //    foreach (var item in request.ShipmentFreightTypes)
                //    {
                //        if (shipmentFreightTypes.FirstOrDefault(x => x.ShipmentFreight_Id == item) == null)
                //            shipmentFreight.Add(new CompanyShipmentFreightTypes { ShipmentFreight_Id = item, Company_Id = request.Id });
                //    }
                //    _context.Set<Domain.Entities.CompanyShipmentFreightTypes>().AddRange(shipmentFreight);
                //}
                await _context.SaveChangesAsync(cancellationToken);
            }
            return company.Id;
        }
    }
}
