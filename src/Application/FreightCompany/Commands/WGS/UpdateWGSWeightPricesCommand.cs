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
    public class UpdateWGSWeightPricesCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public decimal Price { get; set; }

    }

    public class UpdateWGSWeightPricesCommandHandler : IRequestHandler<UpdateWGSWeightPricesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateWGSWeightPricesCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateWGSWeightPricesCommand request, CancellationToken cancellationToken)
        {
            var wgsPrices = await _context.Set<WGSCompanyPrice>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (wgsPrices == null)
                return Result.Failure(new string[] { "Weight was not available" });
            wgsPrices.Price = request.Price;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }


}
