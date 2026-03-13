using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Api.Middlewares
{
    public class PGMKMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWork _unitOfWork;

        public PGMKMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //public PGMKMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
        //{
        //    _next = next;
        //    _unitOfWork = unitOfWork;
        //}

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            // Get requested path
            var requestedUrl = context.Request.Path.Value?.ToLower();

            if (!string.IsNullOrEmpty(requestedUrl))
            {
                if (requestedUrl.Contains("/api/fileuploadmaster"))
                {
                    // param key value
                    string key = await GetRequestedParamKey(context, "kudfa");

                    // Check if the file exists
                    string KeyVal = await unitOfWork.FileUploadMasterRepository.GetKeyOfOpenPage("kudfa");

                    if (string.IsNullOrEmpty(key) || key != KeyVal)
                    {
                        // Prepare JSON result
                        var result = new ApiResult<string>
                        {
                            State = EnumStatus.Error,
                            ErrorMessage = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE,
                            Data = Constants.UN_AUTH_ROLE
                        };

                        // Set response
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        var json = JsonConvert.SerializeObject(result);
                        await context.Response.WriteAsync(json);

                        return; // stop further processing
                    }
                }
                else
                {
                    bool isKeyExists = false;
                    if (requestedUrl.Contains("/api/rolemaster"))
                    {
                        // check key
                        isKeyExists = GetKeyOfOpenPage("krma");
                    }
                    else if (requestedUrl.Contains("/api/menumaster"))
                    {
                        // check key
                        isKeyExists = GetKeyOfOpenPage("kmma");
                    }
                    // check exists
                    if (isKeyExists)
                    {
                        // Prepare JSON result
                        var result = new ApiResult<string>
                        {
                            State = EnumStatus.Error,
                            ErrorMessage = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE,
                            Data = Constants.UN_AUTH_ROLE
                        };

                        // Set response
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        var json = JsonConvert.SerializeObject(result);
                        await context.Response.WriteAsync(json);

                        return; // stop further processing
                    }
                }
            }

            // Call the next middleware
            await _next(context);
        }

        private bool GetKeyOfOpenPage(string key)
        {
            try
            {
                // make path
                var filePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, "PGMK", $"{key}.txt");

                if (System.IO.File.Exists(filePath))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return false;
        }

        private async Task<string> GetRequestedParamKey(HttpContext context, string key)
        {
            string keyVal = "";
            try
            {
                // key request headers first
                if (context.Request.Headers.ContainsKey(key))
                {
                    keyVal = context.Request.Headers[key].ToString();
                }
                // key from FormData
                if (string.IsNullOrEmpty(keyVal) && context.Request.HasFormContentType)
                {
                    var form = await context.Request.ReadFormAsync();
                    if (form.ContainsKey(key))
                    {
                        keyVal = form[key];
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return keyVal;
        }
    }
}
