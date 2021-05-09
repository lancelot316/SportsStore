<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs"
    MasterPageFile="/Pages/Store.Master"
    Inherits="SportsStore.WebUI.Pages.Listing" %>

<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <asp:Repeater ItemType="SportsStore.Domain.Product"
            SelectMethod="GetProducts" runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <h4><%# Item.Price.ToString("c") %></h4>
                    <button name="add" type="submit" value="<%# Item.ProductID %>">
                        Add to Cart
                    </button>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pager">
        <asp:Repeater ItemType="System.String" SelectMethod="GetPagerLinkHtml" runat="server">
            <ItemTemplate>
                <%# Item %>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
