using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.Common.Models
{
    public class ExportFeature
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
