using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Core.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kaushal_Darpan.Api.Code.Attribute
{
    public class ValidationActionFilter : System.Attribute, IActionFilter
    {
        public ValidationActionFilter()
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var lstErrors = context.ModelState.GetModelErrors();
                //var strErrors = JsonConvert.SerializeObject(lstErrors);
                context.Result = new JsonResult(new ApiResult<object>
                {
                    State = EnumStatus.Error,
                    ErrorMessage = Constants.MSG_VALIDATION_FAILED,
                    Data = lstErrors
                });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}
