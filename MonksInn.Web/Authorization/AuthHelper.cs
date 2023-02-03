using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Web.Authorization
{
    public static class AuthHelper
    {
        private static List<PermissionRole> PermissionRoles => new List<PermissionRole>
        {
            //new PermissionRole(StorePermission.CanOrderCellarStock, StoreUserRole.WholeSaleUser),
        };

        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {

            if (user.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

        public static bool HasAccess(this ClaimsPrincipal user, StorePermission permission)
        {
            if (IsAuthenticated(user))
            {
                foreach (var item in PermissionRoles.Where(a => a.Permission == permission))
                {
                    foreach (var role in item.Roles)
                    {
                        bool isAuthorized = user.IsInRole(role.ToString());
                        if (isAuthorized)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            if (IsAuthenticated(user))
            {
                var id = user?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    return new Guid(id);
                }
            }
            return null;
        }

        private class PermissionRole
        {
            internal PermissionRole(StorePermission permission, params StoreUserRole[] roles)
            {
                Permission = permission;
                Roles = new List<StoreUserRole>();
                foreach (var item in roles)
                {
                    Roles.Add(item);
                }

            }
            internal StorePermission Permission { get; set; }
            internal List<StoreUserRole> Roles { get; set; }
        }
    }
}
