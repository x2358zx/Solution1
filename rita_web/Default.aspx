﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="rita_web.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <asp:UpdatePanel ID="upGVMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblName" runat="server" Text="姓名："></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:AutoCompleteExtender ID="userList_AutoCompleteExtender" runat="server" CompletionInterval="500"
                            TargetControlID="userList" ServiceMethod="strUser" ServicePath="~/webservice/ajax.asmx"
                            DelimiterCharacters="" Enabled="True" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            MinimumPrefixLength="1"></asp:AutoCompleteExtender>
            <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
            <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" DataKeyNames="SID,ID" OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" OnPageIndexChanging="gvMaster_PageIndexChanging" PageSize="5" OnRowDataBound="gvMaster_RowDataBound">
                <%--OnPageIndexChanged="gvMaster_PageIndexChanged"--%>
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="編號" />
                    <asp:BoundField DataField="Phone" HeaderText="電話" />
                    <asp:BoundField DataField="Name" HeaderText="名稱" />
                    <asp:BoundField DataField="Address" HeaderText="地址" />
                    <asp:BoundField DataField="Name" HeaderText="名稱" />
                    <asp:TemplateField HeaderText="生日">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Birthday") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            (西元年)
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Birthday", "{0:yyy-mm-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="age" HeaderText="年齡" DataFormatString="{0:#,#0.#}" HtmlEncode="False" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnFirst" runat="server" Text="第一頁" CommandName="PagerFirst" OnClick="gvMaster_PageIndexChanged" />
            &nbsp;<asp:Button ID="btnPrev" runat="server" Text="上一頁" CommandName="PagerPrev" OnClick="gvMaster_PageIndexChanged" />
            &nbsp;<asp:Button ID="btnNext" runat="server" Text="下一頁" CommandName="PagerNext" OnClick="gvMaster_PageIndexChanged" />
            &nbsp;<asp:Button ID="btnLast" runat="server" Text="最後頁" CommandName="PagerLast" OnClick="gvMaster_PageIndexChanged" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DetailsView ID="dvMaster" runat="server" Height="100%" Width="50%"
                AutoGenerateDeleteButton="True" AutoGenerateEditButton="True"
                AutoGenerateInsertButton="True" AutoGenerateRows="False"
                OnModeChanging="dvMaster_ModeChanging" DataKeyNames="SID,ID"
                OnItemInserting="dvMaster_ItemInserting" OnItemInserted="dvMaster_ItemInserted"
                OnItemUpdating="dvMaster_ItemUpdating" OnItemDeleting="dvMaster_ItemDeleting">
                <Fields>
                    <asp:BoundField DataField="NO" HeaderText="編號" />
                    <asp:BoundField DataField="Name" HeaderText="名稱" />
                    <asp:BoundField DataField="Address" HeaderText="地址" />
                    <asp:BoundField DataField="Phone" HeaderText="電話" />
                    <asp:BoundField DataField="Birthday" HeaderText="生日" DataFormatString="{0:yyy-MM-dd}" HtmlEncode="false" />
                </Fields>
            </asp:DetailsView>

        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="fvMaster" runat="server" DataKeyNames="SID,ID" OnItemInserting="fvMaster_ItemInserting" OnModeChanging="fvMaster_ModeChanging" OnItemUpdating="fvMaster_ItemUpdating" OnItemDeleting="fvMaster_ItemDeleting">
                <ItemTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td>NO:</td>
                                <td>
                                    <asp:Label ID="NO" runat="server" Text='<%# Bind("NO") %>' Width="100%"></asp:Label></td>
                                <td>Name:</td>
                                <td>
                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' Width="100%"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Address:</td>
                                <td colspan="3">
                                    <asp:Label ID="Address" runat="server" Text='<%# Bind("Address") %>' Width="100%"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Phone:</td>
                                <td>
                                    <asp:Label ID="Phone" runat="server" Text='<%# Bind("Phone") %>' Width="100%"></asp:Label></td>
                                <td>Birthday:</td>
                                <td>
                                    <asp:Label ID="Birthday" runat="server" Text='<%# Bind("Birthday") %>' Width="100%"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:Button ID="Button1" runat="server" Text="新增" CommandName="New" />
                        <asp:Button ID="btnCreate" runat="server" Text="編輯" CommandName="Edit" />
                        <asp:Button ID="btnCancel" runat="server" Text="刪除" CommandName="Delete" />
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="NO:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="NO" runat="server" Text='<%# Bind("NO") %>'></asp:TextBox>

                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Address:"></asp:Label></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlAddress" runat="server">
                                        <%--<asp:ListItem Value="" Text="--請選擇--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="aaa"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="bbb"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="ccc"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="cddAssress" runat="server" Enabled="true"
                                        ContextKey="" LoadingText="loading" Category="address" PromptText="---" ServicePath="~/WebServices/ajax.asmx"
                                        ServiceMethod="listAdderss" UseContextKey="true" TargetControlID="ddlAddress"/>
                                    <%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Address") %>' Width="100%"></asp:TextBox></td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Phone:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Birthday:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Birthday") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td></td>
                            </tr>
                        </tfoot>
                    </table>
                    <div>
                        <asp:Button ID="btnCreate" runat="server" Text="儲存" CommandName="Update" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="NO:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="NO" runat="server" Text='<%# Bind("NO") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        SetFocusOnError="True" Display="Dynamic" ValidationGroup="testgroup"></asp:RequiredFieldValidator>
                                    <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="fvInsterName" runat="server" Text='<%# Bind("Name") %>' ValidationGroup="testgroup"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Address:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Address") %>' Width="100%" ValidationGroup="testgroup"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Phone:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="fvInsterPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Birthday:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="fvMasterInsertBirthday" runat="server" Text='<%# Bind("Birthday") %>' ValidationGroup="testgroup"></asp:TextBox>
                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" />--%>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                        </tfoot>
                    </table>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="CustomValidator"
                        OnServerValidate="CustomValidator2_ServerValidate" ControlToValidate="NO" ValidationGroup="testgroup"
                        ValidateEmptyText="true" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Font-Size="Larger" Font-Bold="True"></asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fvInsterPhone"
                        ErrorMessage="手機格式不正確" ValidationExpression="((\d{10})|(((\(\d{2}\))|(\d{2}-))?\d{4}(-)?\d{3}(\d)?))"
                        ValidationGroup="testgroup" Display="Dynamic" ForeColor="Red" Font-Size="Larger" Font-Bold="True"></asp:RegularExpressionValidator>
                    <div>
                        <asp:Button ID="btnCreate" runat="server" Text="建立" CommandName="Insert" ValidationGroup="testgroup" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" ValidationGroup="testgroup" />
                    </div>
                </InsertItemTemplate>
            </asp:FormView>

        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    
</asp:Content>