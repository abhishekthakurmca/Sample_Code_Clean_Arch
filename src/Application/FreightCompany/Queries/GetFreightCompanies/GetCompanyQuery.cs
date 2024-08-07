using Anubis.Application.Common.Exceptions;
using Anubis.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.GetFreightCompanies
{

    public class GetCompanyQuery : IRequest<FreightCompanyEditDto>
    {
        public int Id { get; set; }
    }

    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, FreightCompanyEditDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FreightCompanyEditDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Domain.Entities.FreightCompany>()
                //.Include(fc => fc.ShipmentFreightTypes)
                //.Include(fc => fc.WGSCompanyMiles)
                //.Include(fc => fc.WGSCompanyWeights)
                //.ThenInclude(weight => weight.WGSCompanyPrice)
                //.Where(fc => fc.Id == request.Id)
                .FirstOrDefaultAsync(fc => fc.Id == request.Id, cancellationToken);
            var compantData = result == null ? throw new BusinessException("Company not found") :
                 new FreightCompanyEditDto
                 {
                     Id = result.Id,
                     Fax = result.Fax,
                     Name = result.Name,
                     Phone = result.Phone,
                     ShortName = result.ShortName,
                     //Billing address
                     TenderEmail = result.TenderEmail,
                     Address = result.Address,
                     Address1 = result.Address1,
                     City = result.City,
                     Country = result.Country,
                     PaymentMethod = result.PaymentMethod,
                     PaymentTerm = result.PaymentTerm,
                     Region = result.Region,
                     State = result.State,
                     ZipCode = result.ZipCode,
                     IsActive = result.IsActive,
                     FTL = result.FTL,
                     LTL = result.LTL,
                     WGS = result.WGS,
                     //MultiCheckboxShipments = result.ShipmentFreightTypes.Select(x => new MultiCheckboxShipment
                     //{
                     //    Id = x.ShipmentFreight_Id,
                     //    IsSelected = true,

                     //}).ToList()
                 };
            //var ShipmentFreightTypes = await _context.Set<Domain.Entities.LU_ShipmentFreightTypes>().ToListAsync();

            //foreach (var item in ShipmentFreightTypes)
            //{
            //    if (compantData.MultiCheckboxShipments.FirstOrDefault(x => x.Id == item.Id) == null)
            //        compantData.MultiCheckboxShipments.Add(new MultiCheckboxShipment { Id = item.Id, IsSelected = false, Name = item.Name });
            //    else
            //    {
            //        compantData.MultiCheckboxShipments.FirstOrDefault(x => x.Id == item.Id).IsSelected = true;
            //        compantData.MultiCheckboxShipments.FirstOrDefault(x => x.Id == item.Id).Name = item.Name;
            //    }
            //}
            return compantData;
        }
    }
}
