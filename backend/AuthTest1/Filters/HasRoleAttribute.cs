using AuthTest.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthTest.API.Filters
{
    public class HasRoleAttribute : TypeFilterAttribute
    {
        public HasRoleAttribute(params string[] roles) : base(typeof(HasRoleImpl))
        {
            Arguments = new object[] { roles };
        }

        private class HasRoleImpl : ActionFilterAttribute
        {
            private readonly string[] _roles;
            private readonly UserManager<IdentityUser> _userManager;

            public HasRoleImpl(UserManager<IdentityUser> userManager, string[] roles)
            {
                _roles = roles;
                _userManager = userManager;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {

                var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var user = _userManager.FindByIdAsync(userId).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                if (!roles.Any(x => _roles.Contains(x)))
                {
                    var res = new ObjectResult(null);
                    res.StatusCode = 403;
                    context.Result = res;
                }
                else
                {
                    base.OnActionExecuting(context);
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
