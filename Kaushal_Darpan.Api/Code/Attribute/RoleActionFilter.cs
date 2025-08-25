using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Core.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Kaushal_Darpan.Api.Code.Attribute
{
    public class RoleActionFilter : System.Attribute, IActionFilter
    {
        private readonly EnumRole[] _userRole = { };
        public RoleActionFilter(params EnumRole[] userRole)
        {
            _userRole = userRole;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            int roleId = 0;
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg == null) continue;

                // Check if arg itself is a list (IEnumerable)
                if (arg is IEnumerable<object> outerList)
                {
                    foreach (var item in outerList)
                    {
                        var type = item.GetType();

                        // Try exact match first
                        var property = type.GetProperty("RoleID", BindingFlags.Public | BindingFlags.Instance);

                        // If not found, do case-insensitive search manually to avoid ambiguity
                        if (property == null)
                        {
                            property = type
                                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .FirstOrDefault(p => string.Equals(p.Name, "RoleID", StringComparison.OrdinalIgnoreCase));
                        }
                        //value
                        if (property != null)
                        {
                            roleId = (int)(property.GetValue(item) ?? 0);
                            break;
                        }
                    }
                }
                else
                {
                    var type = arg.GetType();

                    // Try exact match first
                    var property = type.GetProperty("RoleID", BindingFlags.Public | BindingFlags.Instance);

                    // If not found, do case-insensitive search manually to avoid ambiguity
                    if (property == null)
                    {
                        property = type
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(p => string.Equals(p.Name, "RoleID", StringComparison.OrdinalIgnoreCase));
                    }
                    // value
                    if (property != null)
                    {
                        roleId = (int)(property.GetValue(arg) ?? 0);
                        break;
                    }
                }
            }


            //check
            if (!_userRole.Any(x => ((int)x) == roleId))
            {
                context.Result = new JsonResult(new ApiResult<string>
                {
                    State = EnumStatus.Error,
                    ErrorMessage = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE,
                    Data = Constants.UN_AUTH_ROLE
                });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}
