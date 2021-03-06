using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string SecIniPath = ConfigurationManager.AppSettings["SecIniPath"].ToString(); //"D:\\Solution1\\INI\\RES.ini"
            //string SystemID = ConfigurationManager.AppSettings["SystemID"].ToString();     //"RES"
            string ConnList = ConfigurationManager.AppSettings["ConnList"].ToString();     //"CONNCLEAN"
            //string[] system = SystemID.Split(new char[] { ',' });
            string[] conns = ConnList.Split(new char[] { ',' });

            #region �]�wKey1 and Key2

            Vista.SEC.Coder coder = new Vista.SEC.Coder();
            Vista.SEC.IniUtil INI = new Vista.SEC.IniUtil(SecIniPath);
            Application.Add("SECKey1", coder.Decrypt(INI.ReadValue("Main", "Key1")));
            Application.Add("SECKey2", coder.Decrypt(INI.ReadValue("Main", "Key2")));
            #endregion

            #region �]�wConnection Pool

            Vista.SEC.ConnectionPool CP = new Vista.SEC.ConnectionPool(SecIniPath);
            foreach (var conn in conns)
            {
                CP.SetConnection(conn);
            }

            //CP = new Vista.DBSSEC.ConnectionPool(SecIniPath);
            //CP.SetConnection(SystemID, ConnList);

            //for (int i = 0; i< system.Length;i++)
            //{ 
            //    Application.Add(system[i],Vista.DBSSEC.ConnectionPool.GetConnection(conns[i]));
            //}

            //Application.Add("CONNSEC", Vista.DBSSEC.ConnectionPool.GetConnection("CONNSEC"));
            //Application.Add("CONNPIPA", Vista.DBSSEC.ConnectionPool.GetConnection("CONNPIPA"));
            #endregion
        }
    }
}
