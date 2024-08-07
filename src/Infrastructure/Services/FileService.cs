using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly IApplicationDbContext _context;

        public FileService(IConfiguration _configuration, IApplicationDbContext context)
        {
            configuration = _configuration;
            _context = context;
        }
        public async Task SaveFileAsync(string path, Stream file)
        {
            var uri = new Uri(path);
            var accountString = configuration.GetValue<string>("AzureCloudStorage");//Environment.GetEnvironmentVariable("AzureCloudStorage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(uri.Scheme);
            await container.CreateIfNotExistsAsync();
            string guid = Guid.NewGuid().ToString();
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference($"{uri.Host}/{uri.LocalPath}");
            await cloudBlockBlob.UploadFromStreamAsync(file);
        }

        public async Task<Stream> OpenFileAsync(string path)
        {
            var ms = new MemoryStream();
            var uri = new Uri(path);
            var accountString = configuration.GetValue<string>("AzureCloudStorage");//Environment.GetEnvironmentVariable("AzureCloudStorage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            string containerName = uri.Scheme == "https" ? "tartarusattachments" : uri.Scheme;

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            string blobPath = uri.Scheme == "https" ? uri.LocalPath.Replace("/tartarusattachments/", "") : $"{uri.Host}/{uri.LocalPath}";

            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(blobPath);
            await cloudBlockBlob.DownloadToStreamAsync(ms);
            ms.Position = 0;
            return ms;
        }


        public async Task<Result> ReadFTLExcel(Stream file, long id)
        {
            var obj = await GetExcelColumn(file);
            if (!obj.Succeeded)
                return obj;
            var ftldata = await _context.Set<FTL_Company>().Where(x => x.Company_Id == id && x.IsDeleted != true).ToListAsync();            
            List<FTL_Company> fTL_Companies = new List<FTL_Company>();
            DataTable dataTable = (DataTable)obj.Data;
            var errorList = new List<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(dataTable.Rows[i]["OriginCity"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[i]["OriginState"].ToString()) ||
                        string.IsNullOrEmpty(dataTable.Rows[i]["DestinationCity"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[i]["DestinationState"].ToString())
                         //|| string.IsNullOrEmpty(dataTable.Rows[i]["Price"].ToString()) 
                         || (Convert.ToDecimal(dataTable.Rows[i]["Price"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["Price"].ToString())))
                    {
                        if (!string.IsNullOrEmpty(dataTable.Rows[i]["Price"].ToString()))
                        {
                            var price = Convert.ToDecimal(dataTable.Rows[i]["Price"]);
                            errorList.Add((i + 1).ToString());
                        }
                    }
                    else
                    {
                        var truckId = _context.Set<LU_Truck>()
                        .Where(lu => lu.Name == dataTable.Rows[i]["TruckSize"].ToString())
                        .FirstOrDefault()?.Id;
                        var data = fTL_Companies.FirstOrDefault(x => x.OriginCity == dataTable.Rows[i]["OriginCity"].ToString() && x.OriginState == dataTable.Rows[i]["OriginState"].ToString()
                        && x.DestinationCity == dataTable.Rows[i]["DestinationCity"].ToString() && x.DestinationState == dataTable.Rows[i]["DestinationState"].ToString() && x.Truck_Id == truckId);
 
                        if (data == null)
                        {
                            var fTL_Companie = new FTL_Company()
                            {
                                Company_Id = id,
                                OriginCity = dataTable.Rows[i]["OriginCity"].ToString(),
                                OriginState = dataTable.Rows[i]["OriginState"].ToString(),
                                DestinationCity = dataTable.Rows[i]["DestinationCity"].ToString(),
                                DestinationState = dataTable.Rows[i]["DestinationState"].ToString(),
                                Price = Convert.ToDecimal(dataTable.Rows[i]["Price"]),
                                Truck_Id = truckId,
                                IsActive = true,
                                IsDeleted = false
                            };
                            fTL_Companies.Add(fTL_Companie);
                        }
                        else
                        {
                            errorList.Add($"{i + 1} Error: Duplicate entry.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(dataTable.Rows[i]["Price"].ToString()))
                        errorList.Add($"{i + 1}  Error:{ex.Message}");
                }
            }

            if (fTL_Companies.Count > 0)
            {
                ftldata.ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                });
            }

            _context.Set<FTL_Company>().AddRange(fTL_Companies);
            CancellationToken cancellationToken;
            await _context.SaveChangesAsync(cancellationToken);

            if (errorList.Count > 0)
                return Result.Failure(errorList);

            return Result.Success();
        }
        public async Task<Result> ReadLTLExcel(Stream file, long id)
        {
            var obj = await GetExcelColumn(file);
            if (!obj.Succeeded)
                return obj;
            var ltldata = await _context.Set<LTL_Company>().Where(x => x.Company_Id == id && x.IsDeleted != true).ToListAsync();
            List<LTL_Company> lTL_Companies = new List<LTL_Company>();
            DataTable dataTable = (DataTable)obj.Data;
            var errorList = new List<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                        if (string.IsNullOrEmpty(dataTable.Rows[i]["OriginCity"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[i]["OriginState"].ToString()) ||
                       string.IsNullOrEmpty(dataTable.Rows[i]["DestinationCity"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[i]["DestinationState"].ToString()) ||
                       ((Convert.ToDecimal(dataTable.Rows[i]["PPrice1"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice1"].ToString())) || (Convert.ToDecimal(dataTable.Rows[i]["PPrice2"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice2"].ToString())) ||
                       (Convert.ToDecimal(dataTable.Rows[i]["PPrice3"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice3"].ToString())) || (Convert.ToDecimal(dataTable.Rows[i]["PPrice4"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice4"].ToString())) ||
                       (Convert.ToDecimal(dataTable.Rows[i]["PPrice5"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice5"].ToString())) || (Convert.ToDecimal(dataTable.Rows[i]["PPrice6"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice6"].ToString())) ||
                       (Convert.ToDecimal(dataTable.Rows[i]["PPrice7"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice7"].ToString())) || (Convert.ToDecimal(dataTable.Rows[i]["PPrice8"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice8"].ToString())) ||
                       (Convert.ToDecimal(dataTable.Rows[i]["PPrice9"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice9"].ToString())) || (Convert.ToDecimal(dataTable.Rows[i]["PPrice10"]) < 0 || string.IsNullOrEmpty(dataTable.Rows[i]["PPrice10"].ToString()))))
                        {
                        if (!string.IsNullOrEmpty(dataTable.Rows[i]["PPrice1"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice2"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice3"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice4"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice5"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice6"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice7"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice8"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice9"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice10"].ToString()))
                        {
                            errorList.Add((i + 1).ToString());
                        }
                        }
                        else
                        {
                            var truckId = _context.Set<LU_Truck>()
                            .Where(lu => lu.Name == dataTable.Rows[i]["TruckSize"].ToString())
                            .FirstOrDefault()?.Id;
                            var data = lTL_Companies.FirstOrDefault(x => x.OriginCity == dataTable.Rows[i]["OriginCity"].ToString() && x.OriginState == dataTable.Rows[i]["OriginState"].ToString()
                              && x.DestinationCity == dataTable.Rows[i]["DestinationCity"].ToString() && x.DestinationState == dataTable.Rows[i]["DestinationState"].ToString() && x.Truck_Id == truckId);
                            
                            if (data == null)
                            {
                                var lTL_Companie = new LTL_Company()
                                {
                                    Company_Id = id,
                                    OriginCity = dataTable.Rows[i]["OriginCity"].ToString(),
                                    OriginState = dataTable.Rows[i]["OriginState"].ToString(),
                                    DestinationCity = dataTable.Rows[i]["DestinationCity"].ToString(),
                                    DestinationState = dataTable.Rows[i]["DestinationState"].ToString(),
                                    PPrice1 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice1"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice1"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice1"]) : 0,
                                    PPrice2 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice2"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice2"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice2"]) : 0,
                                    PPrice3 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice3"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice3"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice3"]) : 0,
                                    PPrice4 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice4"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice4"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice4"]) : 0,
                                    PPrice5 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice5"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice5"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice5"]) : 0,
                                    PPrice6 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice6"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice6"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice6"]) : 0,
                                    PPrice7 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice7"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice7"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice7"]) : 0,
                                    PPrice8 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice8"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice8"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice8"]) : 0,
                                    PPrice9 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice9"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice9"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice9"]) : 0,
                                    PPrice10 = string.IsNullOrEmpty(dataTable.Rows[i]["PPrice10"].ToString()) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["PPrice10"]) > 0 ? Convert.ToDecimal(dataTable.Rows[i]["PPrice10"]) : 0,
                                    Truck_Id = truckId,
                                    IsActive = true,
                                    IsDeleted = false
                                };
                                lTL_Companies.Add(lTL_Companie);
                            }
                            else
                            {
                                errorList.Add($"{i + 1} Error: Duplicate entry.");
                            }
                        }
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(dataTable.Rows[i]["PPrice1"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice2"].ToString()) ||
                            !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice3"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice4"].ToString()) ||
                            !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice5"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice6"].ToString()) ||
                            !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice7"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice8"].ToString()) ||
                            !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice9"].ToString()) || !string.IsNullOrEmpty(dataTable.Rows[i]["PPrice10"].ToString()))
                        errorList.Add($"{i + 1}  Error:{ex.Message}");
                }
            }
            if (lTL_Companies.Count > 0)
            {
                ltldata.ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                });
            }

            _context.Set<LTL_Company>().AddRange(lTL_Companies);
            CancellationToken cancellationToken;
            await _context.SaveChangesAsync(cancellationToken);

            if (errorList.Count > 0)
                return Result.Failure(errorList);

            return Result.Success();
        }


        public async Task<Result> GetExcelColumn(Stream stream)
        {
            DataTable dt = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                var relationship = await Task.Run(() => sheets.FirstOrDefault());
                if (relationship == null)
                    return Result.Failure(new string[] { "Sheet Name not found" });

                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationship.Id.Value);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<DocumentFormat.OpenXml.Spreadsheet.Row> rows = sheetData.Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>();
                foreach (Cell cell in rows.ElementAt(0))
                {
                    if (dt.Columns.Contains(GetCellValue(spreadSheetDocument, cell)))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell) + 1);
                    }
                    else
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }
                }
                foreach (DocumentFormat.OpenXml.Spreadsheet.Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();
                    int columnIndex = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        // Gets the column index of the cell with data
                        int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                        cellColumnIndex--; //zero based index
                        if (columnIndex < cellColumnIndex)
                        {
                            do
                            {
                                tempRow[columnIndex] = ""; //Insert blank data here;
                                columnIndex++;
                            }
                            while (columnIndex < cellColumnIndex);
                        }
                        tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                        columnIndex++;
                    }
                    dt.Rows.Add(tempRow);
                }
            }
            dt.Rows.RemoveAt(0);
            return Result.Success(dt);
        }

        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }
        public static int? GetColumnIndexFromName(string columnName)
        {

            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }


    }
}
