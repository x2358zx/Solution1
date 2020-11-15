<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestDropDownList.aspx.cs" Inherits="rita_web.TestDropDownList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlKIND" runat="server" OnSelectedIndexChanged="ddlKIND_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
            <asp:DropDownList ID="ddlITEM" runat="server"></asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
