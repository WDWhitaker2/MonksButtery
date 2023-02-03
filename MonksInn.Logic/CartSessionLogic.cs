using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class CartSessionLogic : BaseLogic
    {
        public CartSessionLogic(IUnitOfWork uow) : base(uow)
        {
        }


        /// <summary>
        /// Adds a cart item to a cart. if no cart exists then a new one is created. if user exists then the new cart is assigned to the user. returns the Cart ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="userId"></param>
        /// <param name="cartitem"></param>
        /// <returns></returns>
        public Guid AddStockToCart(Guid? cartId, Guid? userId, Guid stockItemId, int units, bool isCellarStock = false)
        {
            CartSession cart = null;

            if (cartId != null)
            {
                cart = Uow.DbContext.CartSessions.AsQueryable(false).FirstOrDefault(a => a.Id == cartId);
            }

            // if there is no cart then create one.
            if (cart == null)
            {
                cart = Uow.DbContext.CartSessions.Add(new CartSession()
                {
                    DateCreated = DateTime.Now,
                });
            }

            // if there is a usern assign cart to them.
            StoreUser user = null;
            if (userId != null)
            {
                user = Uow.DbContext.StoreUsers.AsQueryable(false).FirstOrDefault(a => a.Id == userId);
                if (user != null)
                {
                    user.CartSessionID = cart.Id;
                }
            }

            var cartitem = new CartItem();
            cartitem.DateCreated = DateTime.Now;
            cartitem.CartSessionId = cart.Id;
            cartitem.IsCellarStock = isCellarStock;

            // add item to cart.
            if (isCellarStock)
            {
                var CellarStock = Uow.DbContext.CellarStockItems.AsQueryable(false).FirstOrDefault(a => a.Id == stockItemId);
                cartitem.CellarStockItemPricePerUnit = CellarStock.WholesalePrice;
                cartitem.CellarStockUnits = units;
                cartitem.CellarStockItemId = stockItemId;
            }
            else
            {
                var TappedStock = Uow.DbContext.TappedStockItems.AsQueryable(false).FirstOrDefault(a => a.Id == stockItemId);
                cartitem.TappedStockItemPricePerUnit = TappedStock.RetailPrice;
                cartitem.TappedStockUnits = units;
                cartitem.TappedStockItemId = stockItemId;
            }
            Uow.DbContext.CartItems.Add(cartitem);


            return cart.Id;
        }


        public bool CartHasItem(Guid? cartsessionId)
        {
            if (cartsessionId.HasValue)
            {
                return Uow.DbContext.CartItems.AsQueryable(true).Any(a => a.CartSessionId == cartsessionId);
            }

            return false;
            throw new NotImplementedException();
        }

        public CartSession GetCart(Guid cartid, params string[] includes)
        {
            return Uow.DbContext.CartSessions
                .AsQueryable(true, includes)
                .FirstOrDefault(a => a.Id == cartid);
        }

        public void UpdateTappedStockToCart(Guid itemId, int units)
        {
            var cartitem = Uow.DbContext.CartItems.AsQueryable(false).FirstOrDefault(a => a.Id == itemId);
            if (cartitem != null)
            {
                cartitem.TappedStockUnits = units;

                if (units == 0)
                {
                    Uow.DbContext.CartItems.Remove(cartitem);
                }
            }
        }

        public void JoinCarts(Guid value1, Guid value2)
        {
            var cart1 = Uow.DbContext.CartSessions.AsQueryable(false, "Items").FirstOrDefault(a => a.Id == value2);
            var cart2 = Uow.DbContext.CartSessions.AsQueryable(false).FirstOrDefault(a => a.Id == value2);
            cart2.IsArchived = true;
            var cart2items = Uow.DbContext.CartItems.AsQueryable(false).Where(a => a.CartSessionId == value2);
            cart1.Items.AddRange(cart2items);
        }

        public void RemoveCartItem(Guid id)
        {
            var cartitem = Uow.DbContext.CartItems.AsQueryable().FirstOrDefault(a => a.Id == id);
            Uow.DbContext.CartItems.Remove(cartitem);
        }

        public void DeleteCart(Guid? cartId)
        {
            var cart = Uow.DbContext.CartSessions.AsQueryable().FirstOrDefault(a => a.Id == cartId);
            Uow.DbContext.CartSessions.Remove(cart);
        }
    }
}
