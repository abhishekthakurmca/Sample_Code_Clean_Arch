using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Commands.LTL;
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

namespace Anubis.Application.FreightCompany.Commands.WGS
{
    public class CreateWGSWeightCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public string LabelValue { get; set; }
    }
    public class CreateWGSWeightCommandHandler : IRequestHandler<CreateWGSWeightCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWGSWeightCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateWGSWeightCommand request, CancellationToken cancellationToken)
        {

            var weight = await _context.Set<WGSCompanyWeights>().FindAsync(request.Id);
            if (request.Id > 0 && weight == null)
                return Result.Failure(new string[] { "Weight not found" });

            //Add weight Process
            if (weight == null)
            {
                weight = new WGSCompanyWeights();
                weight.Company_Id = request.Company_Id;
                weight.To = request.To;
                weight.From = request.From;
                weight.Created = DateTime.UtcNow;
                weight.IsDeleted = false;
                weight.LabelValue = $"{Convert.ToString(weight.From)} - {Convert.ToString(weight.To)} lbs";
                var miles = await _context.Set<WGSCompanyMiles>().Where(x => x.Company_Id == request.Company_Id).Select(wgs => wgs.Id).ToListAsync();
                foreach (var item in miles)
                {
                    weight.WGSCompanyPrice.Add(new WGSCompanyPrice
                    {
                        CompanyWGSMiles_Id = item,
                        Created = DateTime.UtcNow,
                        Company_Id = request.Company_Id,
                        IsDeleted = false,
                        Price = 0
                    });
                }
                _context.Set<WGSCompanyWeights>().Add(weight);
            }
            //Edit weight Process
            else
            {
                weight.Company_Id = request.Company_Id;
                weight.To = request.To;
                weight.From = request.From;
                weight.LastModified = DateTime.UtcNow;
                weight.LabelValue = $"{Convert.ToString(weight.From)} - {Convert.ToString(weight.To)} lbs";
                weight.IsDeleted = false;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
