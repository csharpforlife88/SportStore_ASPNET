using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Pages.Helpers;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private readonly Repository _repo = new Repository();

        private int pageSize = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) return;

            int selectedProductId;

            if (!int.TryParse(Request.Form["add"], out selectedProductId)) return;

            var selectedProduct = _repo.Products.FirstOrDefault(p => p.ProductId == selectedProductId);

            if (selectedProduct == null) return;

            SessionHelper.GetCart(Session).AddItem(selectedProduct, 1);

            SessionHelper.Set(Session, SessionKey.RETURN_URL, Request.RawUrl);

            Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath);
        }

        public IEnumerable<Product> GetProducts()
        {
            return FilterProducts()
                .OrderBy(p => p.ProductId)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        protected int CurrentPage
        {
            get
            {
                var page = GetPageFromRequest();

                return page > MaxPage ? MaxPage : page;
            }
        }

        private int GetPageFromRequest()
        {
            int page;

            var regValue = (string)RouteData.Values["page"] ??
                           Request.QueryString["page"];

            return regValue != null && int.TryParse(regValue, out page) ? page : 1;
        }

        protected int MaxPage => (int)Math.Ceiling((decimal)FilterProducts().Count() / pageSize);

        private IEnumerable<Product> FilterProducts()
        {
            var currentCategory = (string)RouteData.Values["category"] ??
                                  Request.QueryString["category"];

            return currentCategory == null
                ? _repo.Products
                : _repo.Products.Where(p => p.Category == currentCategory);
        }
    }
}