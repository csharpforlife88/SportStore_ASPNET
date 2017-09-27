using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Pages.Helpers;

namespace SportsStore.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        private readonly Repository _repo = new Repository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) return;

            int productId;

            if (!int.TryParse(Request.Form["remove"], out productId)) return;

            var selectedProduct = _repo.Products.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct == null) return;

            SessionHelper.GetCart(Session).RemoveLine(selectedProduct);
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(Session).Lines;
        }

        public decimal CartTotal => SessionHelper.GetCart(Session).ComputeTotalValue();

        public string ReturnUrl => SessionHelper.Get<string>(Session, SessionKey.RETURN_URL);

        public string CheckoutUrl => RouteTable.Routes.GetVirtualPath(null, "checkout", null).VirtualPath;
    }
}