using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Pages.Helpers;

namespace SportsStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            checkoutForm.Visible = true;

            checkoutMessage.Visible = false;

            if (!IsPostBack) return;

            var myOrder = new Order();

            if (!TryUpdateModel(myOrder,
                new FormValueProvider(ModelBindingExecutionContext))) return;

            myOrder.OrderLines = new List<OrderLine>();

            var myCart = SessionHelper.GetCart(Session);

            foreach (var line in myCart.Lines)
            {
                myOrder.OrderLines.Add(new OrderLine
                {
                    Order = myOrder,
                    Product = line.Product,
                    Quantity = line.Quantity
                });
            }

            new Repository().SaveOrder(myOrder);

            myCart.Clear();

            checkoutForm.Visible = false;

            checkoutMessage.Visible = true;
        }
    }
}