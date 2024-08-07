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
    public class AddUpdatePermissionsCommand : IRequest<Result>
    {
        public List<long> FreightPermissionIds { get; set; }
        public List<long> PermissionIds { get; set; }
        public long Company_Id { get; set; }

    }
    public class AddUpdatePermissionsCommandHandler : IRequestHandler<AddUpdatePermissionsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public AddUpdatePermissionsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddUpdatePermissionsCommand request, CancellationToken cancellationToken)
        {
            var freightPermisions = new List<FreightPermissions>();
            freightPermisions = await _context.Set<FreightPermissions>().Where(x => x.Company_Id == request.Company_Id).ToListAsync();
            var permisionIds = freightPermisions.Select(x => x.Permission_Id).ToList();
            var newPermissions = request.FreightPermissionIds.Where(p => !permisionIds.Any(p2 => p2 == p));
            if (newPermissions != null && newPermissions.Any())
                foreach (var item in newPermissions)
                {
                    _context.Set<FreightPermissions>().Add(new FreightPermissions
                    {
                        Company_Id = request.Company_Id,
                        Permission_Id = (int)item,
                        IsActive = request.PermissionIds.Contains(item) ? true : false
                    });
                }
            if (freightPermisions != null && freightPermisions.Any())
                foreach (var d in freightPermisions)
                {
                    d.IsActive = request.PermissionIds.Contains(d.Permission_Id) ? true : false;
                }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
