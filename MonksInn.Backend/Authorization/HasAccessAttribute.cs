
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HasAccessAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly SystemPermission[] Permissions;
        private readonly Role[] SystemRoles;

        public HasAccessAttribute(params SystemPermission[] permissions)
        {
            Permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            bool isAuthorized = false;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }
            else
            {
                isAuthorized = true;
            }

            // you can also use registered services
            //var someService = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            if (Permissions?.Any() == true)
            {
                isAuthorized = false;

                foreach (var permission in Permissions)
                {
                    bool permissionIsAuthorized = context.HttpContext.User.HasAccess(permission);
                    if (permissionIsAuthorized)
                    {
                        isAuthorized = true;
                        break;
                    }
                }

            }

            if (SystemRoles?.Any() == true)
            {
                isAuthorized = false;

                foreach (var role in SystemRoles)
                {
                    bool roleIsAuthorized = context.HttpContext.User.IsInRole(role.ToString());
                    if (roleIsAuthorized)
                    {
                        isAuthorized = true;
                        break;
                    }
                }
            }

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }

}