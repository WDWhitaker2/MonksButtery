using Microsoft.EntityFrameworkCore;
using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.SqlDataService
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Beer> Beers { get; internal set; }
        public DbSet<StockOrder> StockOrders { get; internal set; }
        public DbSet<CellarStockItem> CellarStockItems { get; internal set; }
        public DbSet<TappedStockItem> TappedStockItems { get; internal set; }
        public DbSet<PubLocation> PubLocations { get; internal set; }
        public DbSet<StoreDefaultPricing> StoreDefaultPricings { get; internal set; }
        public DbSet<SystemUser> SystemUsers { get; internal set; }
        public DbSet<StoreUser> StoreUsers { get; internal set; }
        public DbSet<File> Files { get; internal set; }
        public DbSet<SystemUserPasswordResetToken> SystemUserPasswordResetTokens { get; internal set; }
        public DbSet<CartItem> CartItems { get; internal set; }
        public DbSet<CartSession> CartSessions { get; internal set; }
        public DbSet<StoreUserRegistrationToken> StoreUserRegistrationTokens { get; internal set; }
        public DbSet<StoreUserPasswordResetToken> StoreUserPasswordResetTokens { get; internal set; }
        public DbSet<StoreUserLikedBeer> StoreUserLikedBeers { get; internal set; }
        public DbSet<StoreUserAddress> StoreUserAddresses { get; internal set; }

        public DbSet<PromoCode> PromoCodes { get; internal set; }
        public DbSet<Order> Orders { get; internal set; }
        public DbSet<OrderItem> OrderItems { get; internal set; }
        public DbSet<DeliverySlot> DeliverySlots { get; internal set; }
        public DbSet<DeliveryDateAllocation> DeliveryDateAllocations { get; internal set; }
        public DbSet<SystemSettings> SystemSettings { get; internal set; }
        public DbSet<WholesaleApplication> WholesaleApplications { get; internal set; }
        public DbSet<Booking> Bookings { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockOrder>().HasOne(a => a.Beer).WithMany().HasForeignKey(a => a.BeerId);
            modelBuilder.Entity<StockOrder>().HasOne(a => a.ReceivedBeer).WithMany().HasForeignKey(a => a.ReceivedBeerId);
            modelBuilder.Entity<CellarStockItem>().HasOne(a => a.Beer).WithMany().HasForeignKey(a => a.BeerId);
            modelBuilder.Entity<TappedStockItem>().HasOne(a => a.Beer).WithMany().HasForeignKey(a => a.BeerId);
            modelBuilder.Entity<TappedStockItem>().HasOne(a => a.PubLocation).WithMany(a=>a.TappedStockItems).HasForeignKey(a => a.PubLocationId);


            modelBuilder.Entity<SystemUserPasswordResetToken>().HasOne(a => a.SystemUser).WithMany().HasForeignKey(a => a.SystemUserId);

            modelBuilder.Entity<CartSession>().HasMany(a => a.Items).WithOne(a=>a.CartSession).HasForeignKey(a => a.CartSessionId);
            modelBuilder.Entity<StoreUser>().HasOne(a => a.CartSession).WithMany().HasForeignKey(a => a.CartSessionID);

            modelBuilder.Entity<StoreUserRegistrationToken>().HasOne(a => a.StoreUser).WithMany().HasForeignKey(a => a.StoreUserId);
            modelBuilder.Entity<StoreUserPasswordResetToken>().HasOne(a => a.StoreUser).WithMany().HasForeignKey(a => a.StoreUserId);


            modelBuilder.Entity<StoreUserLikedBeer>().HasOne(a => a.StoreUser).WithMany().HasForeignKey(a => a.StoreUserId);
            modelBuilder.Entity<StoreUserLikedBeer>().HasOne(a => a.Beer).WithMany().HasForeignKey(a => a.BeerId);

            modelBuilder.Entity<OrderItem>().HasOne(a => a.TappedStockItem).WithMany().HasForeignKey(a => a.TappedStockItemId);
            modelBuilder.Entity<OrderItem>().HasOne(a => a.Order).WithMany(a=>a.Items).HasForeignKey(a => a.OrderId);

            modelBuilder.Entity<Order>().HasOne(a => a.PromoCode).WithMany().HasForeignKey(a => a.PromoCodeId);
            modelBuilder.Entity<Order>().HasOne(a => a.DeliveryAddress).WithMany().HasForeignKey(a => a.DeliveryAddressId);
            modelBuilder.Entity<Order>().Property(a => a.OrderNumber).ValueGeneratedOnAdd();


            modelBuilder.Entity<WholesaleApplication>().HasOne(a => a.StoreUser).WithMany().HasForeignKey(a => a.StoreUserId);


        }
    }
}
