using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MiddlewareSample
{
    public class JWTInHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           var authenticationCookieName = "token";
           var cookie = context.Request.Cookies[authenticationCookieName];
           if (cookie != null)
           {
               var token = JsonConvert.DeserializeObject<AccessToken>(cookie);
               context.Request.Headers.Append("Authorization", "Bearer " + token.token);
               
           }

           await _next.Invoke(context);
        }
    }
}