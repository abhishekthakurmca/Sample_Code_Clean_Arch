using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Exceptions
{
    public class DimensionsInvalidException : Exception
    {
        public DimensionsInvalidException(string dimensionsString, Exception ex)
            : base($"Dimension \"{dimensionsString}\" is invalid.", ex) { }

    }
}
