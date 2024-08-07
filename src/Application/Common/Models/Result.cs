using Anubis.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anubis.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors, bool info)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Info = info;
        }
        internal Result(bool succeeded, IEnumerable<string> errors, bool info, long invoiceDetailId)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Info = info;
            InvoiceDetailId = invoiceDetailId;
        }
        internal Result(bool succeeded, IEnumerable<string> errors, object data)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = data;
        }
        public bool Info { get; set; }
        public bool Succeeded { get; set; }
        public object Data { get; set; }

        public string[] Errors { get; set; }
        public long InvoiceDetailId { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { },false);
        }

        public static Result Success(IEnumerable<string> messages)
        {
            return new Result(true, messages,false);
        }
        public static Result Success(long invoiceDetailId)
        {
            return new Result(true, new string[] { }, false, invoiceDetailId);
        }
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors,false );
        }

        public static Result Infomation(IEnumerable<string> errors)
        {
            return new Result(false, errors,true);
        }
       
        public static Result Success(object data)
        {
            return new Result(true, new string[] { }, data);
        }

    }
    public class ShipmentCapacity
    {
        public String DeliveryDate { get; set; }
        public string PickupDate { get; set; }
    }
}
