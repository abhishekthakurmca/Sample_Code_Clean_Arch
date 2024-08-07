using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Commands.LTL
{
    public class CreateLTLCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string OriginCity { get; set; }
        public string OriginState { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState{ get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public decimal PPrice1 { get; set; }
        public decimal PPrice2 { get; set; }
        public decimal PPrice3 { get; set; }
        public decimal PPrice4 { get; set; }
        public decimal PPrice5 { get; set; }
        public decimal PPrice6 { get; set; }
        public decimal PPrice7 { get; set; }
        public decimal PPrice8 { get; set; }
        public decimal PPrice9 { get; set; }
        public decimal PPrice10 { get; set; }
        public int Truck_Id { get; set; }
    }

    public class CreateLTLCommandHandler : IRequestHandler<CreateLTLCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateLTLCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateLTLCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ltlCompany = await _context.Set<LTL_Company>().FindAsync(request.Id);
                var existsRecord = new LTL_Company();
                if (request.Id > 0 && ltlCompany == null)
                    return Result.Failure(new string[] { "LTL not found" });
                else if (request.Id > 0 && ltlCompany != null)
                {
                    existsRecord = await _context.Set<LTL_Company>().FirstOrDefaultAsync(x => x.Id != request.Id && x.Company_Id == request.Company_Id
                     && x.OriginCity == request.OriginCity && x.OriginState == request.OriginState && x.DestinationCity == request.DestinationCity && x.DestinationState == request.DestinationState && x.IsDeleted != true && (x.Truck_Id == request.Truck_Id || (x.Truck_Id == null && request.Truck_Id == 0)));
                }
                else if (request.Id == 0 && ltlCompany == null)
                {
                    existsRecord = await _context.Set<LTL_Company>().FirstOrDefaultAsync(x => x.Company_Id == request.Company_Id
                     && x.OriginCity == request.OriginCity && x.OriginState == request.OriginState && x.DestinationCity == request.DestinationCity && x.DestinationState == request.DestinationState && x.IsDeleted != true && (x.Truck_Id == request.Truck_Id || (x.Truck_Id == null && request.Truck_Id == 0)));
                }
                if (existsRecord != null)
                    return Result.Failure(new string[] { "LTL already exists." });
                ltlCompany = ltlCompany == null ? new LTL_Company() : ltlCompany;
                ltlCompany.OriginCity = request.OriginCity;
                ltlCompany.OriginState = request.OriginState;
                ltlCompany.IsActive = true;
                ltlCompany.IsDeleted = request.IsDeleted;
                ltlCompany.Company_Id = request.Company_Id;
                ltlCompany.DestinationCity = request.DestinationCity;
                ltlCompany.DestinationState = request.DestinationState;
                ltlCompany.PPrice1 = request.PPrice1;
                ltlCompany.PPrice10 = request.PPrice10;
                ltlCompany.PPrice2 = request.PPrice2;
                ltlCompany.PPrice3 = request.PPrice3;
                ltlCompany.PPrice4 = request.PPrice4;
                ltlCompany.PPrice5 = request.PPrice5;
                ltlCompany.PPrice6 = request.PPrice6;
                ltlCompany.PPrice7 = request.PPrice7;
                ltlCompany.PPrice8 = request.PPrice8;
                ltlCompany.PPrice9 = request.PPrice9;
                ltlCompany.Truck_Id = request.Truck_Id > 0 ? request.Truck_Id : ltlCompany.Truck_Id;

                if (request.Id == 0)
                    _context.Set<LTL_Company>().Add(ltlCompany);
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
