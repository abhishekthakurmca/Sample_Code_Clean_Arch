using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Behaviours
{
    public static class EncodeDecode
    {
        /// <summary>
        /// encode the value
        /// </summary>
        /// <param name="value">normal string</param>
        /// <returns>encoded string</returns>
        public static string EncodeString(string value)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            string encodedString = System.Convert.ToBase64String(toEncodeAsBytes);
            return encodedString;
        }

        /// <summary>
        /// decode the value
        /// </summary>
        /// <param name="value">encoded string</param>
        /// <returns>normal string</returns>
        public static string DecodeString(string value)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(value);
            string decodedString = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return decodedString;
        }
    }
}
