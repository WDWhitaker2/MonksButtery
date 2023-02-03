using Microsoft.AspNetCore.Authorization;
using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Backend.Authorization
{
    public static class AuthHelper
    {
        private static List<PermissionRole> PermissionRoles => new List<PermissionRole>
        {
            // Beer Library
            new PermissionRole(SystemPermission.CanAccessBeerIndexPage, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanAddUpdateBeer, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanArchiveBeer, Role.Admin),

            // Stock Orders
            new PermissionRole(SystemPermission.CanAccessStockOrdersPage, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanOrderStock, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanRecieveStock, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanArchiveStockOrder, Role.Admin),

            // Cellar Stock
            new PermissionRole(SystemPermission.CanAccessCellarStockPage, Role.Admin,Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanTapCellarStock, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanUpdateCellarStock, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanArchiveCellarStock, Role.Admin),
            new PermissionRole(SystemPermission.CanManualAddCellarStock, Role.Admin),

            // Tapped Stock
            new PermissionRole(SystemPermission.CanAccessTappedStockPage, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanUpdateTappedStock, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanArchiveTappedStock, Role.Admin, Role.Manager),

            // store orders
            new PermissionRole(SystemPermission.CanAccessAllOrdersPage, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanAccessDispatchOrdersPage, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanAccessDeliveryOrdersPage, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanAccessTakeawayOrdersPage, Role.Admin, Role.Manager, Role.StaffMember),
            new PermissionRole(SystemPermission.CanDispatchOrder, Role.Admin, Role.Manager, Role.StaffMember),

            //Layout
            new PermissionRole(SystemPermission.CanSeeAdminLinks, Role.Admin),

            // System users 
            new PermissionRole(SystemPermission.CanAccessSystemUserPage, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdateSystemUser, Role.Admin),
            new PermissionRole(SystemPermission.CanArchiveSystemUser, Role.Admin),

            // Store users 
            new PermissionRole(SystemPermission.CanAccessStoreUserPage, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdateStoreUser, Role.Admin),
            new PermissionRole(SystemPermission.CanArchiveStoreUser, Role.Admin),
            
            // System Settings
            new PermissionRole(SystemPermission.CanAccessSystemSettingsPage, Role.Admin),

            // Pub Locations
            new PermissionRole(SystemPermission.CanAccessPubLocationPage, Role.Admin),
            new PermissionRole(SystemPermission.CanArchivePubLocation, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdatePubLocation, Role.Admin),

            // Pricing Structure
            new PermissionRole(SystemPermission.CanAccessPricingStructurePage, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdatePricingStructure, Role.Admin),
            new PermissionRole(SystemPermission.CanArchivePricingStructure, Role.Admin),

            // Delivery Slots
            new PermissionRole(SystemPermission.CanAccessDeliverySlotsPage, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdateDeliverySlots, Role.Admin),
            new PermissionRole(SystemPermission.CanArchiveDeliverySlots, Role.Admin),

            // Promo Codes
            new PermissionRole(SystemPermission.CanAccessPromoCodesPage, Role.Admin),
            new PermissionRole(SystemPermission.CanAddUpdatePromoCodes, Role.Admin),
            new PermissionRole(SystemPermission.CanArchivePromoCodes, Role.Admin),

            // Wholesale Applications
            new PermissionRole(SystemPermission.CanAccessWholesaleApplicationPage, Role.Admin),
            new PermissionRole(SystemPermission.CanActionWholesaleApplication, Role.Admin),
            
            // Bookings
            new PermissionRole(SystemPermission.CanAccessBookingsPage, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanAddUpdateBooking, Role.Admin, Role.Manager),
            new PermissionRole(SystemPermission.CanArchiveBooking, Role.Admin, Role.Manager),

        };

        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {

            if (user.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

        public static bool HasAccess(this ClaimsPrincipal user, SystemPermission permission)
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
            internal PermissionRole(SystemPermission permission, params Role[] roles)
            {
                Permission = permission;
                Roles = new List<Role>();
                foreach (var item in roles)
                {
                    Roles.Add(item);
                }

            }
            internal SystemPermission Permission { get; set; }
            internal List<Role> Roles { get; set; }
        }
    }









}
