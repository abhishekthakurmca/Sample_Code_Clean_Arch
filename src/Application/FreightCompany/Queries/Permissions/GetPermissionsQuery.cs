using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.Permissions
{
    public class GetPermissionsQuery:  IRequest<List<LU_Permissions>>
    {
        public long Company_Id { get; set; }
    }
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<LU_Permissions>>
    {
        private readonly IApplicationDbContext _context;

        public GetPermissionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LU_Permissions>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var lu_Data = await _context.Set<LU_Permissions>().Where(x => x.IsActive == true && x.FreightPermision == true 
            ).ToListAsync();
            var freightPermision = new List<FreightPermissions>();
             freightPermision = await _context.Set<FreightPermissions>().Where(x => x.IsActive == true && x.Company_Id == request.Company_Id).ToListAsync();
            foreach(var d in lu_Data)
            {
                d.FreightPermissions = freightPermision;
            }
            return lu_Data;
        }
    }

}
