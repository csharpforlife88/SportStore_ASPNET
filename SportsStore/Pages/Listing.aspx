<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Listing.aspx.cs"
    MasterPageFile="/Pages/Store.Master"
    Inherits="SportsStore.Pages.Listing" %>

<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <asp:Repeater SelectMethod="GetProducts"
            ItemType="SportsStore.Models.Product"
            runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <h4><%# Item.Price.ToString("c") %></h4>
                    <button name="add" type="submit" value="<%#Item.ProductId %>">Add To Cart</button>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pager">
        <% for (var i = 1; i <= MaxPage; i++)
            {
                var path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary
               {
                   {
                       "page", i
                   }
               }).VirtualPath;



                var c = i == CurrentPage ? "class='selected'" : "";

                Response.Write(
                    $"<a href='{path}' {c}>{i}</a>");
            } %>
    </div>
</asp:Content>
