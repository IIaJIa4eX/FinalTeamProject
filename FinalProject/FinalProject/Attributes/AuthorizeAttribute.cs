using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MarketPracticingPlatform.Attributes
{

    public class UnAuthorizedRedirectAttribute : TypeFilterAttribute
    {
        public UnAuthorizedRedirectAttribute() : base(typeof(UnAuthorizedRedirectFilter))
        {

        }
    }
    public class UnAuthorizedRedirectFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                context.Result = new RedirectResult("~/Login");
            }
        }
    }

}