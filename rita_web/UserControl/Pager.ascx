<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="rita_web.UserControl.Pager" %>
<asp:Button ID="btnFirst" runat="server" Text="第一頁" CommandName="PagerFirst" OnClick="eventPageIndexChanged" />
&nbsp;<asp:Button ID="btnPrev" runat="server" Text="上一頁" CommandName="PagerPrev" OnClick="eventPageIndexChanged" />
&nbsp;<asp:Button ID="btnNext" runat="server" Text="下一頁" CommandName="PagerNext" OnClick="eventPageIndexChanged" />
&nbsp;<asp:Button ID="btnLast" runat="server" Text="最後頁" CommandName="PagerLast" OnClick="eventPageIndexChanged" />
&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="更新資料" CommandName="refresh" OnClick="eventPageIndexChanged" />