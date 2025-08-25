using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.DateConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Api.Middlewares
{
    public class RestrictUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWork _unitOfWork;

        public RestrictUrlMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
        {
            _next = next;
            _unitOfWork = unitOfWork;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool isRestricted = false;
            DateTime? toDate = null;
            int typeID = 0;

            foreach (var item in Constants.RESTRICTED_URLS)
            {
                if (context.Request.Path.Value!.Contains(item.Url))
                {
                    typeID = item.TypeID;
                    var dateConfiguration = new DateConfigurationModel();
                    if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Options || context.Request.Method == HttpMethods.Put)
                    {
                        if (context.Request.HasFormContentType)
                        {
                            var form = context.Request.Form;
                            foreach (var key in form.Keys)
                            {
                                if (key.Equals("CourseTypeID") || key.Equals("coursetype"))
                                {
                                    int.TryParse(form[key], out int val);
                                    if (key.Equals("coursetype"))
                                        dateConfiguration.CourseTypeID = (val == 1 || val == 3) ? 1 : val;
                                    else
                                        dateConfiguration.CourseTypeID = val;
                                }
                                if (key.Equals("CourseSubTypeID") || key.Equals("coursetype"))
                                {
                                    int.TryParse(form[key], out int val);
                                    dateConfiguration.CourseSubTypeID = val;
                                }
                                if (key.Equals("DepartmentID"))
                                {
                                    int.TryParse(form[key], out int val);
                                    dateConfiguration.DepartmentID = val;
                                }
                                if (key.Equals("EndTermID"))
                                {
                                    int.TryParse(form[key], out int val);
                                    dateConfiguration.EndTermID = val;
                                }
                                if (key.Equals("TypeID"))
                                {
                                    int.TryParse(form[key], out int val);
                                    typeID = val;
                                }
                            }
                        }
                        else if (context.Request.ContentLength > 0)
                        {
                            context.Request.EnableBuffering();
                            using (var reader = new StreamReader(
                            context.Request.Body,
                            encoding: Encoding.UTF8,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                            {
                                var body = await reader.ReadToEndAsync();
                                context.Request.Body.Position = 0;
                                dateConfiguration = JsonSerializer.Deserialize<DateConfigurationModel>(body);
                                typeID= dateConfiguration?.TypeID??0;
                            }
                        }

                        var dateData = await _unitOfWork.DateConfigurationRepository.GetDateDataForMiddleware(dateConfiguration!);
                        if (dateData != null)
                        {
                            dateData!.ForEach(x =>
                            {
                                if (x.TypeID == typeID && x.DepartmentID == dateConfiguration!.DepartmentID && x.CourseTypeID == dateConfiguration.CourseTypeID && x.CourseSubTypeID == dateConfiguration.CourseSubTypeID
                                    && Convert.ToDateTime(x.To_Date) < DateTime.Now)
                                    isRestricted = true;
                                toDate = Convert.ToDateTime(x.To_Date);
                            });
                        }
                    }

                    if (isRestricted)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        var response = new ApiResult<object>
                        {
                            State = EnumStatus.Error,
                            Message = string.Format(item.Message, toDate?.ToString("dd-MM-yyyy hh:mm tt")),
                            ErrorMessage = Constants.MSG_UNAUTHORIZED_ACCESS,
                        };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                }
            }
            await _next(context);
        }
    }

    public static class RestrictUrlMiddlewareExtensions
    {
        public static IApplicationBuilder UseRestrictUrl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RestrictUrlMiddleware>();
        }
    }
    public class RestrictUrlFactory : IMiddleware
    {
        private readonly IServiceProvider _serviceProvider;

        
        public RestrictUrlFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
            var middleware = new RestrictUrlMiddleware(next, _unitOfWork);
            await middleware.InvokeAsync(context);
        }
    }
}
