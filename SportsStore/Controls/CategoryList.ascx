<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="CategoryList.ascx.cs" 
    Inherits="SportsStore.Controls.CategoryList" %>

<%=CreateHomeLinkHtml() %>

<% foreach (var cat in GetCategories())
   {
       Response.Write(CreateLinkHtml(cat));
   } %>
