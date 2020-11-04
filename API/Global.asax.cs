using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //API JSon
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            string SecIniPath = ConfigurationManager.AppSettings["SecIniPath"].ToString();
            //string SystemID = ConfigurationManager.AppSettings["SystemID"].ToString();  //A,B,C
            string ConnList = ConfigurationManager.AppSettings["ConnList"].ToString();  //1,2,3
            //string[] system = SystemID.Split(new char[] { ',' });
            string[] conns = ConnList.Split(new char[] { ',' });

            #region 設定Key1 and Key2

            Vista.SEC.Coder coder = new Vista.SEC.Coder();
            Vista.SEC.IniUtil INI = new Vista.SEC.IniUtil(SecIniPath);
            Application.Add("SECKey1", coder.Decrypt(INI.ReadValue("Main", "Key1")));
            Application.Add("SECKey2", coder.Decrypt(INI.ReadValue("Main", "Key2")));

            #endregion

            #region 設定Connection Pool

            Vista.SEC.ConnectionPool CP = new Vista.SEC.ConnectionPool(SecIniPath);
            foreach (var conn in conns)
            {
                CP.SetConnection(conn);
            }
            //for (int i = 0; i < system.Length; i++)
            //{
            //    Application.Add(system[i], Vista.SEC.ConnectionPool.GetConnection(conns[i]));
            //}
            #endregion
        }
    }
}
