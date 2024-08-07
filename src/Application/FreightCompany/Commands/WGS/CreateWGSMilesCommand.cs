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

namespace Anubis.Application.FreightCompany.Commands.WGS
{

    public class CreateWGSMilesCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public decimal Price { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public string LabelValue { get; set; }
        public int Truck_Id { get; set; }
    }
    public class CreateWGSMilesCommandHandler : IRequestHandler<CreateWGSMilesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWGSMilesCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateWGSMilesCommand request, CancellationToken cancellationToken)
        {

            var miles = await _context.Set<WGSCompanyMiles>().FindAsync(request.Id);
            if (request.Id > 0 && miles == null)
                return Result.Failure(new string[] { "Miles not found" });
            //Add weight Process
            if (miles == null)
            {
                miles = new WGSCompanyMiles();
                miles.Company_Id = request.Company_Id;
                miles.To = request.To;
                miles.From = request.From;
                miles.Created = DateTime.UtcNow;
                miles.IsDeleted = false;
                miles.Price = request.Price;
                miles.LabelValue = $"{Convert.ToString(miles.From)} to {Convert.ToString(miles.To)}";
                miles.Truck_Id = request.Truck_Id > 0 ? request.Truck_Id : miles.Truck_Id;

                var weights = await _context.Set<WGSCompanyWeights>().Where(x => x.Company_Id == request.Company_Id).Select(wgs => wgs.Id).ToListAsync();
                if (weights.Any())
                {
                    _context.Set<WGSCompanyMiles>().Add(miles);

                    await _context.SaveChangesAsync(cancellationToken);
                    var wGSCompanyPriceList = new List<WGSCompanyPrice>();
                    foreach (var item in weights)
                    {
                        wGSCompanyPriceList.Add(new WGSCompanyPrice
                        {
                            CompanyWGSWeight_Id = item,
                            Created = DateTime.UtcNow,
                            CompanyWGSMiles_Id = miles.Id,
                            Company_Id = request.Company_Id,
                            IsDeleted = false,
                            Price = 0
                    });
                    }
                    _context.Set<WGSCompanyPrice>().AddRange(wGSCompanyPriceList);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                    _context.Set<WGSCompanyMiles>().Add(miles);
            }
            //Edit weight Process
            else
            {
                miles.Company_Id = request.Company_Id;
                miles.To = request.To;
                miles.From = request.From;
                miles.LastModified = DateTime.UtcNow;
                miles.Price = request.Price;
                miles.LabelValue = $"{Convert.ToString(miles.From)} to {Convert.ToString(miles.To)}";
                miles.IsDeleted = false;
                miles.Truck_Id = request.Truck_Id > 0 ? request.Truck_Id : miles.Truck_Id;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
