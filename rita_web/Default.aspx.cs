using rita.Business;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rita_web
{
    public partial class Default : System.Web.UI.Page
    {
        protected PaginationInfo pagination
        {
            set
            {
                ViewState["pagination"] = value;
            }
            get
            {
                if (ViewState["pagination"] == null) ViewState["pagination"] = new PaginationInfo();
                return (PaginationInfo)ViewState["pagination"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
            
                bindMaster(null);
                bindMasterDetail(null, null);
                bindMasterForm(null, null);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindMaster(name: txtName.Text);//多條件查詢
            bindMasterDetail(null, null); //查詢時使Detail消失
            bindMasterForm(null, null); 
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            dvMaster.ChangeMode(DetailsViewMode.Insert);
            bindMasterDetail(null, null);
            fvMaster.ChangeMode(FormViewMode.Insert);
            bindMasterForm(null, null);
            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }

        #region Master

        #region Master GridView

        protected void bindMaster(string name)
        {
            TestBiz.Master biz = new TestBiz.Master();
            var _pagination = pagination;
            DataTable dt = biz.Load(pagination: ref _pagination, Name: name);//多條件查詢
            pagination = _pagination;
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
        }

        protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
            string ID = Convert.ToString(gvMaster.SelectedDataKey.Values[1]);
            dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
            bindMasterDetail(SID, ID);
            fvMaster.ChangeMode(FormViewMode.ReadOnly);
            bindMasterForm(SID, ID);
            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }

        protected void gvMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvMaster.PageIndex = e.NewPageIndex;
            //bindMaster(null);
        }

        protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int lastPage = pagination.Total / pagination.Size;//頁數
            if ((pagination.Total % pagination.Size) > 0) lastPage = lastPage + 1;//不能整除就加1
            if (btn.CommandName == "PagerFirst") pagination.Index = 1;
            if (btn.CommandName == "PagerPrev") pagination.Index = (pagination.Index - 1) == 0 ? 1 : (pagination.Index - 1);
            if (btn.CommandName == "PagerNext") pagination.Index = (pagination.Index + 1) > lastPage ? lastPage:(pagination.Index +1);
            if (btn.CommandName == "Last") pagination.Index = lastPage;

            bindMaster(null);
        }

        protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType== DataControlRowType.DataRow)
            {
                DateTime? birthday =
                     DataBinder.Eval(e.Row.DataItem, "Birthday") == DBNull.Value
                     ? default(DateTime?)
                     : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Birthday"));
                double? age = null;
                if (birthday.HasValue)
                {
                    age = (DateTime.Today.Year - birthday.Value.Date.Year) + (DateTime.Today.Month - birthday.Value.Date.Month)/12.0;
                }
                if (age.HasValue)
                {
                    e.Row.Cells[4].Text = age.Value.ToString("#,0.0");
                    e.Row.BackColor = Color.Pink;
                }
            }
        }

        #endregion
        #region MasterDetailsView

        protected void bindMasterDetail(int? SID,string ID)
        {
            TestBiz.Master biz = new TestBiz.Master();
            TestMasterInfo e = biz.Load(SID,ID,null); //單條件查詢(因查詢到後，點選取，只會load一筆出來顯示在Detail)
            dvMaster.DataSource = (e == null ? new List<TestMasterInfo> { } :new List<TestMasterInfo> { e });
            dvMaster.DataBind();

            

        }

        protected void dvMaster_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            int? SID = null;
            string ID = string.Empty;
            if (dvMaster.CurrentMode == DetailsViewMode.Insert)
            {
                SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
                ID = gvMaster.SelectedDataKey.Values[1].ToString();
            }
            else
            {
                SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
                ID = dvMaster.DataKey.Values[1].ToString();
            }
            dvMaster.ChangeMode(e.NewMode);
            bindMasterDetail(SID, ID);
        }

        protected void dvMaster_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            var entity = new TestMasterInfo
            {
                NO = e.Values["NO"]?.ToString(),
                Name = e.Values["Name"]?.ToString(),
                Address = e.Values["Address"]?.ToString(),
                Phone = e.Values["Phone"]?.ToString(),
                Birthday = e.Values["Birthday"]?.ToString() == null ? default(DateTime?) : Convert.ToDateTime(e.Values["Birthday"]?.ToString())
            };
            var biz = new TestBiz.Master();
            int i = biz.Insert(entity);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
                bindMasterDetail(null,entity.ID);
                bindMaster(null);
            }
        }

        protected void dvMaster_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            
        }

        protected void dvMaster_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
            string ID = dvMaster.DataKey.Values[1].ToString();

            var entity = new TestMasterInfo
            {
                SID = SID,
                ID = ID,
                NO = e.NewValues["NO"]?.ToString(),
                Name = e.NewValues["Name"]?.ToString(),
                Address = e.NewValues["Address"]?.ToString(),
                Phone = e.NewValues["Phone"]?.ToString(),
                Birthday = e.NewValues["Birthday"] == null ? default(DateTime?) : Convert.ToDateTime(e.NewValues["Birthday"])
            };
            var biz = new TestBiz.Master();
            int i = biz.Update(entity);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
                bindMasterDetail(SID, ID);
                bindMaster(null);
            }
        }

        protected void dvMaster_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
            int SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
            string ID = dvMaster.DataKey.Values[1].ToString();
            var biz = new TestBiz.Master();
            int i = biz.Delete(SID, ID, null);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                bindMaster(null);
                bindMasterDetail(null, null);
            }
        }

        protected void dvMaster_DataBound(object sender, EventArgs e)
        {

        }
        #endregion

        #region Master FormView

        protected void bindMasterForm(int? SID, string ID)
        {
            //var Control = fvMaster.FindControl("ddlAddress");
            //if (Control != null)
            //{
            //    var ddl = ((DropDownList)Control);
            //    ddl.DataSource = Address();
            //    ddl.DataBind();
            //}

            TestBiz.Master biz = new TestBiz.Master();
            TestMasterInfo e = biz.Load(SID, ID, null);
            fvMaster.DataSource = e == null ? new List<TestMasterInfo>() : new List<TestMasterInfo> { e };
            fvMaster.DataBind();
            
        }
        protected void fvMaster_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            int? SID = null;
            string ID = string.Empty;
            if (fvMaster.CurrentMode == FormViewMode.Insert)
            {
                if (gvMaster.SelectedDataKey != null)
                {
                    SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
                    ID = gvMaster.SelectedDataKey.Values[1].ToString();
                }
            }
            else
            {
                SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
                ID = fvMaster.DataKey.Values[1].ToString();
            }
            fvMaster.ChangeMode(e.NewMode);
            bindMasterForm(SID, ID);
        }
        protected void fvMaster_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            var entity = new TestMasterInfo
            {
                NO = e.Values["NO"]?.ToString(),
                Name = e.Values["Name"]?.ToString(),
                Address = e.Values["Address"]?.ToString(),
                Phone = e.Values["Phone"]?.ToString(),
                Birthday = string.IsNullOrEmpty(e.Values["Birthday"].ToString()) ? default(DateTime?) : Convert.ToDateTime(e.Values["Birthday"]),
            };
            var biz = new TestBiz.Master();
            int i = biz.Insert(entity);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                fvMaster.ChangeMode(FormViewMode.ReadOnly);
                bindMasterForm(null, entity.ID);
                bindMaster(null);
                upGVMaster.Update();
            }
        }
        protected void fvMaster_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {

            int SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
            string ID = fvMaster.DataKey.Values[1].ToString();

            var entity = new TestMasterInfo
            {
                SID = SID,
                ID = ID,
                NO = e.NewValues["NO"]?.ToString(),
                Name = e.NewValues["Name"]?.ToString(),
                Address = e.NewValues["Address"]?.ToString(),
                Phone = e.NewValues["Phone"]?.ToString(),
                Birthday = string.IsNullOrEmpty(e.NewValues["Birthday"].ToString()) ? default(DateTime?) : Convert.ToDateTime(e.NewValues["Birthday"]),
            };
            var biz = new TestBiz.Master();
            int i = biz.Update(entity);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                fvMaster.ChangeMode(FormViewMode.ReadOnly);
                bindMasterForm(null, entity.ID);
                bindMaster(null);
            }
        }
        protected void fvMaster_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            int SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
            string ID = fvMaster.DataKey.Values[1].ToString();
            var biz = new TestBiz.Master();
            int i = biz.Delete(SID, ID, null);
            if (i == 0)
            {
                e.Cancel = true;
            }
            else
            {
                bindMaster(null);
                bindMasterForm(null, null);
            }
        }

        #endregion

        #endregion

        #region function
        #endregion

        protected void btnCreate_Click1(object sender, EventArgs e)
        {

        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            var cv = (CustomValidator)source;
            var ctl = fvMaster.FindControl(cv.ControlToValidate);
            if (ctl != null)
            {
                var c = (TextBox)ctl;
                if (!string.IsNullOrEmpty(c.Text))
                {
                    var biz = new TestBiz.Master();
                    var e = biz.Load(null, null, c.Text.Trim());
                    if (e == null)
                        args.IsValid = true;
                    else
                        cv.ErrorMessage = "編號重複";
                }
                else
                    cv.ErrorMessage = "請輸入";
            }
        }

        private DataTable Address()
        {
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");

            var dr = dt.NewRow();
            dr["id"] = Guid.NewGuid().ToString();
            dr["Text"] = "--請選擇--";
            dr["Value"] = "0";
            dt.Rows.Add(dr);

            dr["id"] = Guid.NewGuid().ToString();
            dr["Text"] = "ccc";
            dr["Value"] = "1";
            dt.Rows.Add(dr);

            dr["id"] = Guid.NewGuid().ToString();
            dr["Text"] = "ccc";
            dr["Value"] = "2";
            dt.Rows.Add(dr);

            dr["id"] = Guid.NewGuid().ToString();
            dr["Text"] = "ccc";
            dr["Value"] = "3";
            dt.Rows.Add(dr);

            return dt;
        }

        protected void fvMaster_DataBound(object sender, EventArgs e)
        {
            var Control = fvMaster.FindControl("ddlAddress");
            if (Control != null)
            {
                var ddl = ((DropDownList)Control);
                ddl.DataSource = Address();
                ddl.DataBind();
            }

        }
    }
}