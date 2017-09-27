using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models.Repository;

namespace SportsStore.Controls
{
    public partial class CategoryList : System.Web.UI.UserControl
    {
        private readonly Repository _repo = new Repository();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string CreateHomeLinkHtml()
        {
            var path = RouteTable.Routes.GetVirtualPath(null, null).VirtualPath;

            return $"<a href={path}>Home</a>";
        }

        protected IEnumerable<string> GetCategories()
        {
            return _repo.Products
                .Select(c => c.Category)
                .Distinct()
                .OrderBy(x => x);
        }

        protected string CreateLinkHtml(string cat)
        {
            var selectedCategory = (string) Page.RouteData.Values["category"] ??
                                   Request.QueryString["category"];

            var c = cat == selectedCategory ? "class='selected'" : "";

            var path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary
            {
                {
                    "category", cat
                },
                {
                    "page",1
                }
            }).VirtualPath;

            return $"<a href={path} {c}>{cat}</a>";
        }
    }
}