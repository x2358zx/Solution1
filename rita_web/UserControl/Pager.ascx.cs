using Microsoft.Ajax.Utilities;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rita_web.UserControl
{
    public partial class Pager : System.Web.UI.UserControl
    {

        #region event

        public delegate void btnPageClick(object sender, EventArgs e, PaginationInfo pagination);
        public event btnPageClick PageIndexChanged;
        protected void eventPageIndexChanged(object sender, EventArgs e)
        {
            int lastPage = pagination.Total / pagination.Size;
            Button btn = (Button)sender;
            if (btn.CommandName == "PagerFirst") pagination.Index = 1;
            if (btn.CommandName == "PagerPrev") pagination.Index = (pagination.Index - 1) == 0 ? 1 : pagination.Index - 1;
            if (btn.CommandName == "PagerNext") pagination.Index = (pagination.Index + 1) > lastPage ? lastPage : (pagination.Index + 1);
            if (btn.CommandName == "PagerLast") pagination.Index = lastPage;
            if (btn.CommandName == "refresh") { pagination.Index = 1; pagination.Total = 0; }
            if (PageIndexChanged != null)
            {
                PageIndexChanged(sender, e, pagination);
            }
        }

        #endregion
        #region property

        public PaginationInfo pagination
        {
            set
            {
                ViewState["pagination"] = value;
            }
            get
            {
                if (ViewState["pagination"] == null) ViewState["pagination"] = 1;
                return (PaginationInfo)ViewState["pagination"];
            }
        }
        public int Index
        {
            set
            {
                ViewState["Index"] = value;
            }
            get
            {
                if (ViewState["Index"] == null) ViewState["Index"] = 1;
                return (int)ViewState["Index"];
            }
        }
        public int Size
        {
            set
            {
                ViewState["Size"] = value;
            }
            get
            {
                if (ViewState["Size"] == null) ViewState["Size"] = 1;
                return (int)ViewState["Size"];
            }
        }
        public int Total
        {
            set
            {
                ViewState["Total"] = value;
            }
            get
            {
                if (ViewState["Total"] == null) ViewState["Total"] = 1;
                return (int)ViewState["Total"];
            }
        }
        public bool Bind
        {
            set
            {
                if (value)
                {
                    BindData();
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void BindData()
        {
            btnRefresh.Text = DateTime.Now.ToString("HH:mm:ss");
        }


    }
}