using Hangfire.Dashboard;
using System.Text;


namespace Kaushal_Darpan.Api.Code.Hangfire
{
    public class HangfireCustomBasicAuthenticationFilter : IDashboardAuthorizationFilter
    {

        private readonly string _username;
        private readonly string _password;

        public HangfireCustomBasicAuthenticationFilter(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow only HTTPS
            //if (!httpContext.Request.IsHttps)
            //{
            //    return false;
            //}

            var authHeader = httpContext.Request.Headers["Authorization"].ToString();

            // No Authorization header
            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.Headers["WWW-Authenticate"] =
                    "Basic realm=\"Hangfire Dashboard\"";

                return false;
            }

            try
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();

                var credentials = Encoding.UTF8.GetString(
                    Convert.FromBase64String(encodedCredentials)
                );

                var separatorIndex = credentials.IndexOf(':');

                if (separatorIndex <= 0)
                {
                    return false;
                }

                var username = credentials.Substring(0, separatorIndex);
                var password = credentials.Substring(separatorIndex + 1);

                //if (username == _username && password == _password)
                if (username == "RK" && password == "DEV")
                {
                    return true;
                }
            }
            catch
            {
                // Invalid Base64 / credentials
            }

            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            httpContext.Response.Headers["WWW-Authenticate"] =
                "Basic realm=\"Hangfire Dashboard\"";

            return false;
        }
    }
}
