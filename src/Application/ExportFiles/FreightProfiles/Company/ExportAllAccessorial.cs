using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
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

    public class WGSAccesrailsDto
    {
        public long Id { get; set; }       
        public string FreightCode { get; set; }

    }

    public class ExportAllAccessorial : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllAccessorialHandler : IRequestHandler<ExportAllAccessorial, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;
        public ExportAllAccessorialHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }
        public async Task<ExportFeature> Handle(ExportAllAccessorial request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<WGSAccesrails>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                                                                 .Include(x => x.FreightCompanyCodes).Select(x => new WGSAccesrailsDto
                                                                 {
                                                                     FreightCode = x.FreightCompanyCodes.Name,
                                                                     Id = x.Id
                                                                 })
                                                                .DynamicPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Accessorials-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
