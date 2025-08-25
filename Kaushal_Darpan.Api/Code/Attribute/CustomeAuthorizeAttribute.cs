using Kaushal_Darpan.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kaushal_Darpan.Api.Code.Attribute
{
    public class CustomeAuthorizeAttribute : System.Attribute, IAuthorizationFilter
    {
        public CustomeAuthorizeAttribute()
        {

        }

        private readonly EnumRole[] _userRole = { };
        public CustomeAuthorizeAttribute(params EnumRole[] userRole)
        {
            _userRole = userRole;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User == null || context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new ApiResult<string>
                {
                    State = EnumStatus.Error,
                    Message = Constants.MSG_UNAUTHORIZED_ACCESS,
                    Data = Constants.UN_AUTH_ACCESS
                });

            }
            else if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //var roleIds = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RoleIDs")?.Value ?? ",";
                //if (!string.IsNullOrWhiteSpace(roleIds) && !roleIds.Split(",").Any(x => _userRole.Any(x1 => ((int)x1).ToString() == x)))
                //{
                //    context.Result = new JsonResult(new ApiResult<object>
                //    {
                //        Status = EnumStatus.Failed,
                //        Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE
                //    });
                //}
            }
        }
    }
}
