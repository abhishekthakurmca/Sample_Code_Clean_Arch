using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
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

namespace Anubis.Application.FreightCompany.Queries.LinkedClients
{

    public class GetLinkedClientDetailsQuery : IRequest<LinkedClientDetailsDto>
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int CollectionPointId { get; set; }
        public string CollectionPointName { get; set; }
    }
    public class GetLinkedClientDetailsQueryHandler : IRequestHandler<GetLinkedClientDetailsQuery, LinkedClientDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLinkedClientDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LinkedClientDetailsDto> Handle(GetLinkedClientDetailsQuery request, CancellationToken cancellationToken)
        {
            LinkedClientDetailsDto clientLinks = new LinkedClientDetailsDto();
            var clients = await _context.Set<CompanyLinkedClients>()
                .Where(x => (x.ClientName == request.ClientName || 
                    x.ClientId == request.ClientId) &&
                    x.CollectionId == request.CollectionPointId &&
                    x.IsDeleted != true)
                .ToListAsync(cancellationToken);

            if (clients.Any())
            {
                if (clients.FirstOrDefault(x => x.IsPrimary == true) != null && clients.FirstOrDefault(x => x.IsSecondary == true) != null)
                    clientLinks.Message = "Primary and Secondary accounts already linked for this client";
                else if (clients.FirstOrDefault(x => x.IsPrimary == true) != null && clients.FirstOrDefault(x => x.IsSecondary == true) == null)
                    clientLinks.ClientPreferenceDto = new List<ClientPreferenceDto>{
                    new ClientPreferenceDto{Id=2,Name="Secondary" }};
                else if (clients.FirstOrDefault(x => x.IsPrimary == true) == null && clients.FirstOrDefault(x => x.IsSecondary == true) != null)
                    clientLinks.ClientPreferenceDto = new List<ClientPreferenceDto>{
                    new ClientPreferenceDto{Id=1,Name="Primary" }};
            }
            else
            {
                clientLinks.ClientPreferenceDto = new List<ClientPreferenceDto>
                {
                    new ClientPreferenceDto{Id=1,Name="Primary"},
                    new ClientPreferenceDto{Id=2,Name="Secondary" }
                };
            }
            return clientLinks;
        }
    }
}
