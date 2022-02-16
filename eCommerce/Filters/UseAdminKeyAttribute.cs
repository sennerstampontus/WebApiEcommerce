using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eCommerce.Filters
{
    public class UseAdminKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var adminKey = configuration.GetValue<string>("ApiAdminKey");


            if(!context.HttpContext.Request.Headers.TryGetValue("valid", out var valid))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!adminKey.Equals(valid))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
