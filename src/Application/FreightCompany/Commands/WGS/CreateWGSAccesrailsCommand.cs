using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.WGS
{
    public class CreateWGSAccesrailsCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long FreightCode_Id { get; set; }
        public bool IsFixedPrice { get; set; }
        public bool IsDeleted { get; set; }
        public decimal DefaultPrice { get; set; }
    }
    public class CreateWGSAccesrailsCommandHandler : IRequestHandler<CreateWGSAccesrailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWGSAccesrailsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateWGSAccesrailsCommand request, CancellationToken cancellationToken)
        {

            var accesrails = await _context.Set<WGSAccesrails>().FindAsync(request.Id);
            if (request.Id > 0 && accesrails == null)
                return Result.Failure(new string[] { "Accesrails not found" });
            
            if (request.Id == 0 && accesrails == null)
                accesrails = new WGSAccesrails();
            accesrails.Company_Id = request.Company_Id;
            accesrails.IsDeleted = request.IsDeleted;
            accesrails.IsFixedPrice = true;
            accesrails.DefaultPrice = request.DefaultPrice;
            accesrails.FreightCode_Id = request.FreightCode_Id;
            if (request.Id == 0)
                _context.Set<WGSAccesrails>().Add(accesrails);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }

}
