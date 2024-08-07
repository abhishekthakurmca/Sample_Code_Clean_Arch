using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.Dashboard;
using Anubis.Application.FreightCompany.Queries.GetFreightCompanies;
using Anubis.Domain.Entities;
using Anubis.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
    public  class ExportAllApprovalPending : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllApprovalPendingHandler : IRequestHandler<ExportAllApprovalPending, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllApprovalPendingHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllApprovalPending request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var result= await _context.Set<RouteShipments>().Where(x => x.Tendered == (int)Tender.ApprovalPending)
                         .ProjectTo<ExportShipmentDTO>(_mapper.ConfigurationProvider)
                            .DynamicPageAsync(request, cancellationToken);
            return new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(result.Data),
                ContentType = "text/csv",
                FileName = $"Approval-Pending-Shipments-{DateTime.Now.Ticks}.csv"
            };
        }
    }

}
