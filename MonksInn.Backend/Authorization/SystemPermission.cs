using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Authorization
{
    public enum SystemPermission
    {
        // Beer Library
        CanAccessBeerIndexPage,
        CanAddUpdateBeer,
        CanArchiveBeer,

        // Stock Orders
        CanAccessStockOrdersPage,
        CanOrderStock,
        CanRecieveStock,
        CanArchiveStockOrder,

        // Cellar Stock
        CanAccessCellarStockPage,
        CanTapCellarStock,
        CanUpdateCellarStock,
        CanArchiveCellarStock,
        CanManualAddCellarStock,

        // Tapped Stock
        CanAccessTappedStockPage,
        CanUpdateTappedStock,
        CanArchiveTappedStock,

        // Store Orders
        CanAccessAllOrdersPage,
        CanAccessDispatchOrdersPage,
        CanAccessDeliveryOrdersPage,
        CanDispatchOrder,

        // Layout
        CanSeeAdminLinks,

        // System users
        CanAccessSystemUserPage,
        CanAddUpdateSystemUser,
        CanArchiveSystemUser,

        // Store User
        CanAccessStoreUserPage,
        CanAddUpdateStoreUser,
        CanArchiveStoreUser,

        //System Settings
        CanAccessSystemSettingsPage,

        // Pub Locations
        CanAccessPubLocationPage,
        CanAddUpdatePubLocation,
        CanArchivePubLocation,

        // Pricing Structure
        CanAccessPricingStructurePage,
        CanAddUpdatePricingStructure,
        CanArchivePricingStructure,

        // Delivery Slots
        CanAccessDeliverySlotsPage,
        CanAddUpdateDeliverySlots,
        CanArchiveDeliverySlots,

        // Promo Codes
        CanAccessPromoCodesPage,
        CanAddUpdatePromoCodes,
        CanArchivePromoCodes,
        CanAccessTakeawayOrdersPage,

        // Wholesale Applications
        CanAccessWholesaleApplicationPage,
        CanActionWholesaleApplication,

        // bookings 
        CanAccessBookingsPage,
        CanAddUpdateBooking,
        CanArchiveBooking,
    }
}
