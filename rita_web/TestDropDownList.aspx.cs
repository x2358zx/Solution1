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
            //非常重要!避免重新繫結時，讓繫結全部初始
            if (!IsPostBack)
            {
                BindddlKIND();
                BindddlITEM();
            }
        }

        private void BindddlKIND()
        {
            //string KIND = ddlKIND.Text;

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

        private void BindddlITEM()
        {
            string KIND = ddlKIND.Text;

            #region 存取資料庫執行SQL
            TestDropDownListDB db = new TestDropDownListDB();
            DataTable dt = db.ITEM_Load(KIND: KIND);
            #endregion

            #region 讓欄位說明呈現
            DataRow ndr = dt.NewRow();
            ndr["ITEM"] = "品項";
            dt.Rows.InsertAt(ndr, 0);
            #endregion

            #region 繫結欄位
            ddlITEM.DataSource = dt;
            ddlITEM.DataTextField = "ITEM";
            ddlITEM.DataValueField = "ITEM";
            ddlITEM.DataBind();
            #endregion
        }

        protected void ddlKIND_SelectedIndexChanged(object sender, EventArgs e)
        {
            //讓品項重新繫結資料庫，對應種類呈現出來
            BindddlITEM();
        }
    }
}