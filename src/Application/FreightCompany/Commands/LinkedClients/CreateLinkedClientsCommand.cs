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

namespace Anubis.Application.FreightCompany.Commands.LinkedClients
{
    public class CreateLinkedClientsCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public long CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string ScheduleFrequency { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public bool ApprovalRequired { get; set; }
        public int TransportDays { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string FreightType { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class CreateWGSAccesrailsCommandHandler : IRequestHandler<CreateLinkedClientsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWGSAccesrailsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateLinkedClientsCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Set<CompanyLinkedClients>().FindAsync(request.Id);
            var existsRecord = new CompanyLinkedClients();
            if (request.Id > 0 && client == null)
                return Result.Failure(new string[] { "Linked client not found" });
            else if (request.Id > 0 && client != null)
                existsRecord = await _context.Set<CompanyLinkedClients>().
                    FirstOrDefaultAsync(x => x.Id != request.Id && 
                        x.ClientName.ToLower() == request.ClientName.ToLower() &&
                        x.CollectionId == request.CollectionId &&
                        x.IsDeleted != true);
            else if (request.Id == 0 && client == null)
                existsRecord = await _context.Set<CompanyLinkedClients>().
                    FirstOrDefaultAsync(x => x.ClientName.ToLower() == request.ClientName.ToLower() && 
                        x.CollectionId == request.CollectionId &&
                        x.IsDeleted != true);

            if (existsRecord != null && request.Id == 0)
            {
                if (request.IsPrimary && existsRecord.IsPrimary )
                    return Result.Failure(new string[] { $"Primary account already linked with {existsRecord.ClientName}" });
                else if (request.IsSecondary && existsRecord.IsSecondary)
                    return Result.Failure(new string[] { $"Secondary account already linked with {existsRecord.ClientName}" });
            }

            if (request.Id == 0 && client == null)
                client = new CompanyLinkedClients();
            client.Company_Id = request.Company_Id;
            client.IsDeleted = request.IsDeleted;
            client.ClientId = request.ClientId;
            client.ClientName = request.ClientName;
            client.CollectionId = request.CollectionId;
            client.CollectionName = request.CollectionName;
            client.ApprovalRequired= request.ApprovalRequired;
            client.IsPrimary = request.IsPrimary;
            client.IsSecondary= request.IsSecondary;
            client.ScheduleFrequency = request.ScheduleFrequency;
            client.TransportDays = request.TransportDays;
            client.FromTime = request.FromTime;
            client.ToTime= request.ToTime;
            client.FreightType = request.FreightType;
            if (request.Id == 0)
                _context.Set<CompanyLinkedClients>().Add(client);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
