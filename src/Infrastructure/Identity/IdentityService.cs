using Anubis.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public IdentityService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            return await Task.FromResult(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name));
        }

        public async Task<string> GetAccessToken()
        {
            var Baseurl = _config["Cerberus:WebApi"];
            var secret = _config["Cerberus:ClientSecret"];
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage();

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Cache-Control", "no-cache");
            request.RequestUri = new Uri($"{Baseurl}connect/token");
            request.Method = HttpMethod.Post;

            var bodycontent = new StringContent($"grant_type=client_credentials&scope=cerberus&client_id=admin&client_secret={secret}", Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Content = bodycontent;

            var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var jobj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            return jobj["access_token"].ToString();
        }
    }
}

