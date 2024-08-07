using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Anubis.Infrastructure.Common.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ServiceException(string url, HttpResponseMessage response) : base($"Failed fetch of {url}")
        {
            try
            {
                ResponseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch
            {
                
            }
        }

        public string ResponseContent { get; private set; }
    }
}
