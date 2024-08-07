using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.FTL
{

    public class CreateFTLCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string OriginCity { get; set; }
        public string OriginState { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public int Truck_Id { get; set; }
    }

    public class CreateFTLCommandHandler : IRequestHandler<CreateFTLCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFTLCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateFTLCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ftlCompany = await _context.Set<FTL_Company>().FindAsync(request.Id);
                var existsRecord = new FTL_Company();
                if (request.Id > 0 && ftlCompany == null)
                    return Result.Failure(new string[] { "FTL not found" });
                else if (request.Id > 0 && ftlCompany != null)
                {
                    existsRecord = await _context.Set<FTL_Company>().FirstOrDefaultAsync(x => x.Id != request.Id && x.Company_Id == request.Company_Id
                     && x.OriginCity == request.OriginCity && x.OriginState == request.OriginState && x.DestinationCity == request.DestinationCity && x.DestinationState == request.DestinationState && x.IsDeleted != true && (x.Truck_Id == request.Truck_Id || (x.Truck_Id == null && request.Truck_Id == 0)));
                }
                else if (request.Id == 0 && ftlCompany == null)
                {
                    existsRecord = await _context.Set<FTL_Company>().FirstOrDefaultAsync(x => x.Company_Id == request.Company_Id
                     && x.OriginCity == request.OriginCity && x.OriginState == request.OriginState && x.DestinationCity == request.DestinationCity && x.DestinationState == request.DestinationState && x.IsDeleted != true && (x.Truck_Id == request.Truck_Id || (x.Truck_Id == null && request.Truck_Id == 0)));
                }
                if (existsRecord != null)
                    return Result.Failure(new string[] { "FTL already exists." });
                ftlCompany = ftlCompany == null ? new FTL_Company() : ftlCompany;
                ftlCompany.OriginCity = request.OriginCity;
                ftlCompany.OriginState = request.OriginState;
                ftlCompany.IsActive = true;
                ftlCompany.IsDeleted = request.IsDeleted;
                ftlCompany.Company_Id = request.Company_Id;
                ftlCompany.DestinationCity = request.DestinationCity;
                ftlCompany.DestinationState = request.DestinationState;
                ftlCompany.Price = request.Price;
                ftlCompany.Truck_Id = request.Truck_Id > 0 ? request.Truck_Id : ftlCompany.Truck_Id;
                if (request.Id == 0)
                    _context.Set<FTL_Company>().Add(ftlCompany);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
