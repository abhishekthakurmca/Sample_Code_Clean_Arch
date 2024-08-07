using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Interfaces
{
    public interface IGoogleService
    {
        GoogleAPIResult GetMiles(string origins, string destinations);
    }
}
