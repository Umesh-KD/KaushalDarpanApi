using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.UserActivityLogger;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Principal;


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
            try
            {
                var claimPrincipal = GetCustomPrincipal(context);
                var userid = claimPrincipal.UserID;
                //var username = context.HttpContext?.User?.Identity?.Name ?? "Anonymous";
                var action = context.ActionDescriptor.RouteValues["action"] ?? "";
                var controller = context.ActionDescriptor.RouteValues["controller"] ?? "";
                var pageUrl = context.HttpContext?.Request?.Path ?? "";
                var ipAddress = context.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";// not used
                var refererUrl = context.HttpContext?.Request?.Headers?.Referer.ToString() ?? "";
                var frontendUrl = context.HttpContext?.Request?.Headers["X-Frontend-Url"].FirstOrDefault() ?? "";

                var logModel = new UserActivityLoggerModel
                {
                    UserID = userid,
                    ActionName = action,
                    Controller = controller,
                    PageUrl = pageUrl,
                    IpAddress = ipAddress,
                    RefererUrl = refererUrl,
                    FrontendPageUrl = frontendUrl
                };

                _unitOfWork.UserActivityLoggerRepository.SaveUserLogActivity(logModel).Wait();//save
                _unitOfWork.SaveChangesAsync().Wait();
            }
            catch (Exception ex)
            {

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        private KDClaimPrincipal GetCustomPrincipal(ActionExecutingContext context)
        {
            KDClaimPrincipal _KDClaimPrincipal = new KDClaimPrincipal();
            var _claimsPrincipal = context.HttpContext?.User;
            if (_claimsPrincipal.Identity != null)
            {
                _KDClaimPrincipal.IsAuthenticated = _claimsPrincipal.Identity.IsAuthenticated;

                if (_KDClaimPrincipal.IsAuthenticated)
                {
                    _KDClaimPrincipal.UserID = Convert.ToInt32(_claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    _KDClaimPrincipal.Identity = new GenericIdentity(_claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
                    _KDClaimPrincipal.UserName = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                    _KDClaimPrincipal.Email = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                    _KDClaimPrincipal.RoleIDs = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == nameof(_KDClaimPrincipal.RoleIDs))?.Value;
                    _KDClaimPrincipal.RoleNames = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == nameof(_KDClaimPrincipal.RoleNames))?.Value;
                }
            }

            return _KDClaimPrincipal;
        }

        class KDClaimPrincipal
        {
            public IIdentity? Identity { get; set; }

            public bool IsAuthenticated { get; set; }
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string RoleIDs { get; set; }
            public string RoleNames { get; set; }
        }

    }

}
