using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using CityAppREST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityAppREST.Filters
{
    public class NameIdentifierFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userClaims = context.HttpContext.User.Claims;
            var isAdmin = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value == UserType.Admin.ToString();

            if (!isAdmin && context.ActionArguments.ContainsKey("id"))
            {
                var idParam = context.ActionArguments["id"] as Int32?;
                var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value ?? "0");
                if (idParam != userId) // user may not read or write data of another user
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new EmptyResult();
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
