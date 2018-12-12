using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using CityAppREST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityAppREST.Filters
{
    public class ReadWriteAccessFilter : ActionFilterAttribute
    {
        public string RequestObjectType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userClaims = context.HttpContext.User.Claims;
            var isAdmin = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value == UserType.Admin.ToString(); // admin has r/w access to all

            if (!isAdmin && context.ActionArguments.ContainsKey("id"))
            {
                var idParam = (context.ActionArguments["id"] as Int32?).ToString();
                bool hasAccess = false;

                switch (RequestObjectType)
                {
                    case nameof(Company):
                        var companyIds = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value?.Split(" ").ToList() ?? new List<String>();
                        hasAccess = companyIds.Contains(idParam); // owners only have read or write access to their own companies
                        break;
                    case nameof(User):
                        var userId = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";
                        hasAccess = idParam == userId; // users may not read or write data of another user
                        break;
                    default: throw new ArgumentException("Invalid RequestObjectType");

                }

                if (!hasAccess)
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new EmptyResult();
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
