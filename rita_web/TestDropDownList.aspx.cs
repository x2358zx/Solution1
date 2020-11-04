using rita.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rita_web
{
    public partial class TestDropDownList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindDropDownList();
        }

        private void BindDropDownList()
        {
            string KIND = ddlKIND.Text;

            #region 存取資料庫執行SQL
            TestDropDownListDB db = new TestDropDownListDB();
            DataTable dt = db.KIND_Load();
            #endregion

            #region 讓欄位說明呈現
            DataRow ndr = dt.NewRow();
            ndr["KIND"] = "種類";
            dt.Rows.InsertAt(ndr, 0);
            #endregion

            #region 繫結欄位
            ddlKIND.DataSource = dt;
            ddlKIND.DataTextField = "KIND";
            ddlKIND.DataValueField = "KIND";
            ddlKIND.DataBind();
            #endregion
        }
    }
}