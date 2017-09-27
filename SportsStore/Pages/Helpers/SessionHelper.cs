using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SportsStore.Models;

namespace SportsStore.Pages.Helpers
{
    public class SessionHelper
    {
        public static void Set(HttpSessionState session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T Get<T>(HttpSessionState session, SessionKey key)
        {
            var data = session[Enum.GetName(typeof(SessionKey), key)];

            if (data is T)
                return (T) data;

            return default(T);
        }

        public static Cart GetCart(HttpSessionState session)
        {
            var cart = Get<Cart>(session, SessionKey.CART);

            if (cart == null)
            {
                cart = new Cart();

                Set(session, SessionKey.CART, cart);
            }

            return cart;
        }
    }

    public enum SessionKey
    {
        CART,
        RETURN_URL
    }
}