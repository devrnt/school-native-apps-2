using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityAppREST.Filters
{
    public class UserDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);
        }
    }
}
