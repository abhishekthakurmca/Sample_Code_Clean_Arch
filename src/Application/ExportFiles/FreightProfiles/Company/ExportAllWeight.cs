using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Mappings;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.Dashboard;
using Anubis.Application.FreightCompany.Queries.LinkedClients;
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
    public class WGSWeight 
    {      
        public string LBL { get; set; }
        public long From { get; set; }      
        public long To { get; set; }
        
    }

    public class ExportAllWeight : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllWeightHandler : IRequestHandler<ExportAllWeight, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;
        public ExportAllWeightHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }
        public async Task<ExportFeature> Handle(ExportAllWeight request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await (from rs in _context.Set<WGSCompanyWeights>()
                              where rs.Company_Id == request.Id && rs.IsDeleted != true
                              select new WGSWeight
                              {
                                  LBL = rs.LabelValue,
                                  From = rs.From,
                                  To = rs.To,
                              }
            ).DynamicPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"WGS-Weights-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
