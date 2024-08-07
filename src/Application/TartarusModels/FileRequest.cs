using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.TartarusModels
{
    public class FileRequest
    {
        public string EntityID { get; set; }
        public int EntityTypeID { get; set; }
    }

    public class DeleteAttachmentRequest
    {
        public int AttachmentId { get; set; }
    }
}
