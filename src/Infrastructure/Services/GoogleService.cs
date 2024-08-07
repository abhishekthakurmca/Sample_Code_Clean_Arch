using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Infrastructure.Services
{
    public class GoogleService : IGoogleService
    {
        private static IConfiguration _Configuration;
        public GoogleService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        private static string GoogleApiKey
        {
            get { return _Configuration.GetValue<string>("GoogleApiKey"); }
        }
        private static string GoogleApiUrl
        {
            get { return _Configuration.GetValue<string>("GoogleDistanceMatrixApiUrl"); }
        }

        private static Uri CreateWebRequestUri(string origins, string destinations)
        {
            return new Uri(string.Format(
                "{0}?origins={1}&destinations={2}&units={3}&key={4}",
                GoogleApiUrl,
                origins,
                destinations,
                "imperial",
                GoogleApiKey));
        }

        private static string ConvertToString(string[] data)
        {
            StringBuilder _result = new StringBuilder();
            foreach (var item in data)
            {
                _result.Append(item + ",");
            }
            return _result.ToString().Remove(_result.ToString().Length - 1);
        }

        private static string SendWebRequest(string origins, string destinations)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(CreateWebRequestUri(origins, destinations));
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                            return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        public GoogleAPIResult GetMiles(string origins, string destinations)
        {
            return JsonConvert.DeserializeObject<GoogleAPIResult>(SendWebRequest(origins, destinations));
            //return vv;
            //dynamic _results = JsonConvert.DeserializeObject(SendWebRequest(origins, destinations));

            //if (_results == null)
            //    return null;

            //if (_results["status"] == "OK")
            //{
            //    foreach (var result in _results["rows"])
            //    {
            //        var data = JsonConvert
            //                .DeserializeObject<GoogleAPIResult>(result.ToString());
            //    }
            //}
            //return null;
        }
    }
}
