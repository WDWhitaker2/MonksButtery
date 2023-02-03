using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.SqlDataService
{
    public class SqlDataService : IDatabaseService
    {
        private SqlDataContext db { get; set; }
        public SqlDataService(SqlDataContext dbContext)
        {
            db = dbContext;
        }
        
        public IDatabaseRepository<Beer> Beers => new SqlDataRepository<Beer>(db.Beers);
        public IDatabaseRepository<StockOrder> StockOrders => new SqlDataRepository<StockOrder>(db.StockOrders);
        public IDatabaseRepository<CellarStockItem> CellarStockItems => new SqlDataRepository<CellarStockItem>(db.CellarStockItems);
        public IDatabaseRepository<TappedStockItem> TappedStockItems => new SqlDataRepository<TappedStockItem>(db.TappedStockItems);

        public IDatabaseRepository<PubLocation> PubLocations => new SqlDataRepository<PubLocation>(db.PubLocations);

        public IDatabaseRepository<StoreDefaultPricing> StoreDefaultPricings => new SqlDataRepository<StoreDefaultPricing>(db.StoreDefaultPricings);

        public IDatabaseRepository<SystemUser> SystemUsers => new SqlDataRepository<SystemUser>(db.SystemUsers);

        public IDatabaseRepository<StoreUser> StoreUsers => new SqlDataRepository<StoreUser>(db.StoreUsers);

        public IDatabaseRepository<File> Files => new SqlDataRepository<File>(db.Files);

        public IDatabaseRepository<SystemUserPasswordResetToken> SystemUserPasswordResetTokens => new SqlDataRepository<SystemUserPasswordResetToken>(db.SystemUserPasswordResetTokens);

        public IDatabaseRepository<CartSession> CartSessions => new SqlDataRepository<CartSession>(db.CartSessions);

        public IDatabaseRepository<CartItem> CartItems => new SqlDataRepository<CartItem>(db.CartItems);

        public IDatabaseRepository<StoreUserRegistrationToken> StoreUserRegistrationTokens => new SqlDataRepository<StoreUserRegistrationToken>(db.StoreUserRegistrationTokens);

        public IDatabaseRepository<StoreUserPasswordResetToken> StoreUserPasswordResetTokens => new SqlDataRepository<StoreUserPasswordResetToken>(db.StoreUserPasswordResetTokens);

        public IDatabaseRepository<StoreUserLikedBeer> StoreUserLikedBeers => new SqlDataRepository<StoreUserLikedBeer>(db.StoreUserLikedBeers);

        public IDatabaseRepository<StoreUserAddress> StoreUserAddresses => new SqlDataRepository<StoreUserAddress>(db.StoreUserAddresses);

        public IDatabaseRepository<PromoCode> PromoCodes => new SqlDataRepository<PromoCode>(db.PromoCodes);

        public IDatabaseRepository<Order> Orders => new SqlDataRepository<Order>(db.Orders);

        public IDatabaseRepository<OrderItem> OrderItems => new SqlDataRepository<OrderItem>(db.OrderItems);
        public IDatabaseRepository<DeliverySlot> DeliverySlots => new SqlDataRepository<DeliverySlot>(db.DeliverySlots);

        public IDatabaseRepository<DeliveryDateAllocation> DeliveryDateAllocations => new SqlDataRepository<DeliveryDateAllocation>(db.DeliveryDateAllocations);

        public IDatabaseRepository<SystemSettings> SystemSettings => new SqlDataRepository<SystemSettings>(db.SystemSettings);

        public IDatabaseRepository<WholesaleApplication> WholesaleApplications => new SqlDataRepository<WholesaleApplication>(db.WholesaleApplications);
        public IDatabaseRepository<Booking> Bookings => new SqlDataRepository<Booking>(db.Bookings);


        
        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
