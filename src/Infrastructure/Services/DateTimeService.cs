using Anubis.Application.Common.Interfaces;
using System;

namespace Anubis.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
