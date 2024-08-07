using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetAccessToken();
    }
}
