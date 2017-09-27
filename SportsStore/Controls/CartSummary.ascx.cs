using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Pages.Helpers;

namespace SportsStore.Controls
{
    public partial class CartSummary : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cart = SessionHelper.GetCart(Session);

            csQuantity.InnerText = cart.Lines.Sum(x => x.Quantity).ToString();

            csTotal.InnerText = cart.Lines.Sum(x => x.Quantity * x.Product.Price).ToString("c");

            csLink.HRef = RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath;
        }
    }
}