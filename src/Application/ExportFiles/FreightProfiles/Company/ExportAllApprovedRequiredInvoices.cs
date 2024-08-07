using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries;
using MediatR;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
    public class ExportAllApprovedRequiredInvoices : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllApprovedRequiredInvoicesHandler : IRequestHandler<ExportAllApprovedRequiredInvoices, ExportFeature>
    {

        private readonly IExcelConverter _excelConverter;
        private readonly IDapper _dapper;

        public ExportAllApprovedRequiredInvoicesHandler(IExcelConverter excelConverter, IDapper dapper)
        {
            _excelConverter = excelConverter;
            _dapper = dapper;

        }

        public async Task<ExportFeature> Handle(ExportAllApprovedRequiredInvoices request, CancellationToken cancellationToken)
        {try
            {
               
                string procName = "spGetInvoiceList";
                request.Page = 1;
                var param = GridQueryParameters.Parameters(request);
                bool isApproved = false;
                param.Add("@Approved", isApproved, DbType.Boolean);
                var approvalRequiredInvoiceList = await _dapper.GetAll<FreightBillingDto>(procName, param);
                var result = approvalRequiredInvoiceList
                  .Select(x => new
                  {
                      RouteId = x.Route_Id,
                      ShipmentId = x.ShipmentIdToolTip,
                      CompanyName = x.CompanyName,
                      InvoiceNumber = x.InvoiceNumberToolTip,
                      InvoiceDate = x.InvoiceDateToolTip,
                      ClientName = x.ClientNameToolTip,
                      State = x.StateToolTip,
                      City = x.CityToolTip,
                      CollectionPoint = x.CollectionPoint,
                      SiteName = x.SiteName,
                      Quote = x.Quote,
                      GrandTotal = x.GrandTotal,
                      Percentage = x.Percentage


                  }).ToList();

                return new ExportFeature
                {
                    Content = _excelConverter.ConvertCsv(result),
                    ContentType = "text/csv",
                    FileName = $"Approved-Required-Invoices-{DateTime.Now.Ticks}.csv"
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}
