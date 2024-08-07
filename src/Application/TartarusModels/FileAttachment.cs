using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.TartarusModels
{
    public class FileAttachment
    {
        public FileDetail FileDetail { get; set; }
    }
    public class FileDetail
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public string VirtualPath { get; set; }
        public int AttachmentTypeId { get; set; }
        public string AttachmentTypeName { get; set; }
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }
        public string Key { get; set; }
        public string EntityName { get; set; }
    }
}
