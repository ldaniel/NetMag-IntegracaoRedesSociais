<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LinkedIn_Exemplo02.aspx.cs" Inherits="NetMag.RedesSociais.IntegracaoWeb.LinkedIn_Exemplo02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Suas informações do Profile</h2>
<br />
<table cellpadding="0" cellspacing="0" border="0" style="float: left; width: 400px">
    <tr>
        <td class="label">Nome:</td>
        <td class="data"><asp:Literal ID="nameLiteral" runat="server" /></td>
    </tr>
    <tr>
        <td class="label">Cargo:</td>
        <td class="data"><asp:Literal ID="headlineLiteral" runat="server" /></td>
    </tr>
    <tr>
        <td class="label">Status:</td>
        <td class="data"><asp:Literal ID="statusLiteral" runat="server" /></td>
    </tr>
    <tr>
        <td class="label">Posições:</td>
        <td>
            <asp:DataList ID="positionsDataList" runat="server">
                <ItemTemplate>
                    <strong>
                        <%# Eval("Title") %></strong> at <strong>
                            <%# Eval("Company.Name")%></strong>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
<div style="float: left; width: 200px">
    <asp:Image ID="profileImage" runat="server" />
</div>
</asp:Content>
