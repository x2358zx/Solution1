using AjaxControlToolkit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace rita_web.WebServices
{
    /// <summary>
    ///ajax 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
     [System.Web.Script.Services.ScriptService]
    public class ajax : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] listAddress(string knownCategoryValues, string category, string contextKey)
        {
            List<CascadingDropDownNameValue> items = new List<CascadingDropDownNameValue>();
            try
            {
                items.Add(new CascadingDropDownNameValue("aaa", "1"));
                items.Add(new CascadingDropDownNameValue("bbb", "2"));
                items.Add(new CascadingDropDownNameValue("ccc", "3"));
            }
            catch { }
            return items.ToArray();
        }
    }
}

