using Anubis.Application.Common.Exceptions;
using Anubis.Application.Common.Interfaces;
using Anubis.Domain.Entities;
using Anubis.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
namespace Anubis.Application.FreightCompany.Queries.Dropdowns
{


    public class BasicInformationDDLQuery : IRequest<BasicInformationCompanyDto>
    {
        public string ddlFor { get; set; }
        public long Company_Id { get; set; }
    }

    public class BasicInformationDDLQueryHandler : IRequestHandler<BasicInformationDDLQuery, BasicInformationCompanyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BasicInformationDDLQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BasicInformationCompanyDto> Handle(BasicInformationDDLQuery request, CancellationToken cancellationToken)
        {
            var result = new BasicInformationCompanyDto();
            if (request.ddlFor.ToLower() == "basic")
            {
                result.Countries = await _context.Set<Domain.Entities.LU_Country>().ToListAsync(cancellationToken);
                result.States = await _context.Set<Domain.Entities.LU_State>().ToListAsync(cancellationToken);
                result.PaymentTerms = (from PaymentTerms e in Enum.GetValues(typeof(PaymentTerms)) select new Payment { Id = (int)e, Name = e.ToString() }).ToList();
                result.PaymentMethods = (from PaymentMethods e in Enum.GetValues(typeof(PaymentMethods)) select new Payment { Id = (int)e, Name = e.ToString() }).ToList();
                //result.ShipmentFreightTypes = await _context.Set<Domain.Entities.LU_ShipmentFreightTypes>().ToListAsync();
                //result.MultiCheckboxShipmens= await _context.Set<Domain.Entities.LU_ShipmentFreightTypes>()
                //    .Select(x=>new MultiCheckboxShipment { 
                //    Id=x.Id,
                //    IsSelected=false,
                //    Name=x.Name
                //    })
                //    .ToListAsync(cancellationToken);
            }
            else if (request.ddlFor.ToLower() == "contact")
                result.ClientContactTypes = await _context.Set<Domain.Entities.LU_ClientContactTypes>().ToListAsync(cancellationToken);
            else if (request.ddlFor.ToLower() == "freightcategory")
                result.FreightCategories = await _context.Set<Domain.Entities.FreightCategory>().ToListAsync(cancellationToken);
            return result;

        }
    }
}
