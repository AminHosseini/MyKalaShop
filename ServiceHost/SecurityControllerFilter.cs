using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;

namespace ServiceHost
{
    public class SecurityControllerFilter : IActionFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityControllerFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionPermissions = (NeedsPermissionAttribute)(context.ActionDescriptor as ControllerActionDescriptor)
                .MethodInfo.GetCustomAttributes(typeof(NeedsPermissionAttribute)).FirstOrDefault();

            if (actionPermissions == null)
                return;

            var accountPermissions = _authHelper.GetPermissions();

            if (accountPermissions.All(x => x != actionPermissions.Permission))
                context.HttpContext.Response.Redirect("/Account/Index");
        }
    }
}
