using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain.Interfaces
{
    public interface IDatabaseService
    {
        public IDatabaseRepository<Beer> Beers { get; }
        public IDatabaseRepository<StockOrder> StockOrders { get; }
        public IDatabaseRepository<CellarStockItem> CellarStockItems { get; }
        public IDatabaseRepository<TappedStockItem> TappedStockItems { get; }

        public IDatabaseRepository<PubLocation> PubLocations { get; }
        public IDatabaseRepository<StoreDefaultPricing> StoreDefaultPricings { get; }
        public IDatabaseRepository<SystemUser> SystemUsers { get; }
        public IDatabaseRepository<StoreUser> StoreUsers { get; }
        public IDatabaseRepository<File> Files { get; }

        public IDatabaseRepository<SystemUserPasswordResetToken> SystemUserPasswordResetTokens { get; }
        public IDatabaseRepository<StoreUserRegistrationToken> StoreUserRegistrationTokens { get; }
        
        public IDatabaseRepository<CartSession> CartSessions { get; }
        public IDatabaseRepository<CartItem> CartItems { get; }
        public IDatabaseRepository<StoreUserPasswordResetToken> StoreUserPasswordResetTokens { get; }
        public IDatabaseRepository<StoreUserLikedBeer> StoreUserLikedBeers { get; }
        public IDatabaseRepository<StoreUserAddress> StoreUserAddresses { get; }

        public IDatabaseRepository<PromoCode> PromoCodes { get; }
        public IDatabaseRepository<Order> Orders { get; }
        public IDatabaseRepository<OrderItem> OrderItems { get; }
        public IDatabaseRepository<DeliverySlot> DeliverySlots { get; }
        public IDatabaseRepository<DeliveryDateAllocation> DeliveryDateAllocations { get; }
        public IDatabaseRepository<SystemSettings> SystemSettings { get; }
        public IDatabaseRepository<WholesaleApplication> WholesaleApplications { get; }
        public IDatabaseRepository<Booking> Bookings { get; }

        public int SaveChanges();
    }
}
