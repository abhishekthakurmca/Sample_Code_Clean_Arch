using Anubis.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task SaveFileAsync(string path, Stream file);
        Task<Stream> OpenFileAsync(string path);
        Task<Result> ReadFTLExcel(Stream file, long id);
        Task<Result> ReadLTLExcel(Stream file, long id);
        Task<Result> GetExcelColumn(Stream stream);
    }
}
