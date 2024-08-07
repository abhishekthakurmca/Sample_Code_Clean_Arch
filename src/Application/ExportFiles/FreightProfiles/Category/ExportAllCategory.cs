using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.ExportFiles.FreightProfiles.Category;
using Anubis.Application.FreightCompany.Queries.GetFreightCompanies;
using Anubis.Domain.Entities;
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
    public class ExportAllCategory : GridQuery, IRequest<ExportFeature>
    {
    }
    public class ExportAllCategoryHandler : IRequestHandler<ExportAllCategory, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllCategoryHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllCategory request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<FreightCategory>().Where(x => x.IsDeleted != true).Select(x => new FreightCategory
            {
                Id = x.Id,
                Name = x.Name,
            }).ProjectTo<FreightCategoryDto>(_mapper.ConfigurationProvider).DynamicPageAsync(request, cancellationToken);
            return new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(data.Data),
                ContentType = "text/csv",
                FileName = $"Freight-Category-{DateTime.Now.Ticks}.csv"
            };
        }
    }

}
