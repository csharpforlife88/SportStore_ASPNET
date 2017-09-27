﻿using System.Web.Routing;

namespace SportsStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(null, "list/{category}/{page}", "~/Pages/Listing.aspx");

            routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");//localhost:000

            routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");//localhost:000/list

            routes.MapPageRoute("cart", "cart", "~/Pages/CartView.aspx");

            routes.MapPageRoute("checkout", "checkout", "~/Pages/Checkout.aspx");
        }
    }
}