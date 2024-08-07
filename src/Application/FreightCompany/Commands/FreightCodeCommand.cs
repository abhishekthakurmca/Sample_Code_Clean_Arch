using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands
{
    public class FreightCodeCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long FreightCategory_Id { get; set; }
        public long Company_Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class FreightCodeCommandHandler : IRequestHandler<FreightCodeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FreightCodeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(FreightCodeCommand request, CancellationToken cancellationToken)
        {
            var freightCode = await _context.Set<FreightCompanyCodes>().FindAsync(request.Id);
            var existsRecord = new FreightCompanyCodes();
            if (request.Id > 0 && freightCode == null)
                return Result.Failure(new string[] { "Freight code not found" });
            else if (request.Id > 0 && freightCode != null)
            {
                existsRecord = await _context.Set<FreightCompanyCodes>().FirstOrDefaultAsync(x => x.Id != request.Id && x.Company_Id == request.Company_Id
                 && x.Name == request.Name && x.IsDeleted != true);
            }
            else if (request.Id == 0 && freightCode == null)
            {
                existsRecord = await _context.Set<FreightCompanyCodes>().FirstOrDefaultAsync(x => x.Company_Id == request.Company_Id
                 && x.Name == request.Name && x.IsDeleted  != true);
                freightCode = new FreightCompanyCodes();
            }
            if (existsRecord != null)
                return Result.Failure(new string[] { "Same Freight code already exists." });

            freightCode.Name = request.Name;
            freightCode.Company_Id = request.Company_Id;
            freightCode.DefaultPrice = request.DefaultPrice;
            freightCode.ShortDescription = request.ShortDescription;
            freightCode.Description = request.Description;

            freightCode.FreightCategory_Id = request.FreightCategory_Id;
            freightCode.IsDeleted = false;
            if (request.Id == 0)
                _context.Set<FreightCompanyCodes>().Add(freightCode);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
