using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.UserActivityLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kaushal_Darpan.Api.Code.Attribute
{
    public class LogUserActivityFilter : IActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogUserActivityFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext?.User?.Identity?.Name ?? "Anonymous";
            var action = context.ActionDescriptor.RouteValues["action"] ?? "";
            var controller = context.ActionDescriptor.RouteValues["controller"] ?? "";
            var pageUrl = context.HttpContext?.Request?.Path ?? "";
            var ipAddress = context.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";

            var logModel = new UserActivityLoggerModel
            {
                UserName = username,
                ActionName = action,
                Controller = controller,
                PageUrl = pageUrl,
                IpAddress = ipAddress
            };

            _unitOfWork.UserActivityLoggerRepository.SaveUserLogActivity(logModel).Wait();//save
            _unitOfWork.SaveChanges();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}
