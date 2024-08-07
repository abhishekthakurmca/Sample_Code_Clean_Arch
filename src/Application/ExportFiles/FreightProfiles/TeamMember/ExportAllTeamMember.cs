using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.Dtos.TeamMember;
using Anubis.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.ExportFiles.FreightProfiles.TeamMember
{
    public class ExportAllTeamMember : GridQuery, IRequest<ExportFeature>
    {
    }
    public class ExportManageInvoiceHandler : IRequestHandler<ExportAllTeamMember, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportManageInvoiceHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllTeamMember request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<LU_TeamMember>().Where(x => x.IsDeleted != true)
              .ProjectTo<TeamMemberDto>(_mapper.ConfigurationProvider)
              .DynamicExportPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(data.Data),
                ContentType = "text/csv",
                FileName = $"TeamMember-Details-{DateTime.Now.Ticks}.csv"
            };
        }
    }
}
