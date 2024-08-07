using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Anubis.Infrastructure.Services
{
    static class ExtensionService
    {
        public static string FormatPhone(this object input, string format = "($1) $2-$3", string emptyac = "() ")
        {
            var numbers = new string(NumbersOnly(input, false, false, false).Take(10).ToArray());
            return Regex.Replace(numbers, @"(\d{3})?(\d{3})(\d{4})", format).Replace(emptyac, "");
        }

        public static string NumbersOnly(this object input, bool allowNegative = true, bool allowDecimal = true, bool replaceZero = true)
        {
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using var sr = new StringReader(input.ToString().Trim());
                if (allowNegative && sr.Peek() != -1 && (char)sr.Peek() == '-')
                    sw.Write((char)sr.Read());

                var readDecimal = false || !allowDecimal;
                while (sr.Peek() != -1)
                {
                    var c = (char)sr.Read();
                    if (char.IsDigit(c))
                    {
                        sw.Write(c);
                    }
                    else if (c == '.')
                    {
                        if (!allowDecimal) break;
                        if (!readDecimal)
                        {
                            sw.Write(c);
                            readDecimal = true;
                        }
                    }
                }
            }
            if (sb.ToString() == ".") sb.Clear();
            if (sb.ToString() == "" && replaceZero) return "0";

            return sb.ToString();
        }
    }
}
