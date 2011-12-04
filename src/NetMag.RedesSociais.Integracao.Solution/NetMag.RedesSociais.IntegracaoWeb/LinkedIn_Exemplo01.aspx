<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LinkedIn_Exemplo01.aspx.cs" Inherits="NetMag.RedesSociais.IntegracaoWeb.LinkedIn_Exemplo01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Suas conexões no LinkedIn</h2>
    <br />
    <asp:DataList ID="connectionsDataList" runat="server">
        <ItemTemplate>
            <strong>
                <%# Eval("Name") %></strong> - <em>
                    <%# Eval("Headline")%></em>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
