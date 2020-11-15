using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.DataAccess
{
    public class TestDropDownListDB : baseDB
    {
        public TestDropDownListDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }

        public DataTable KIND_Load()
        {
            DataTable dt1 = new DataTable();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"	SELECT DISTINCT KIND
                              FROM ITEM WITH (Nolock)  
            ");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            //db.AddInParameter(dbCommand, "@SID", DbType.Int32, iSID);

            #endregion

            try
            {
                dt1 = db.ExecuteDataSet(dbCommand).Tables[0];

            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }

            return dt1;
        }

        public DataTable ITEM_Load(string KIND)
        {
            DataTable dt2 = new DataTable();

            Database db = base.GetDatabase();
            StringBuilder sbCmd2 = new StringBuilder();

            sbCmd2.Append(@"SELECT DISTINCT ITEM
                              FROM ITEM WITH (Nolock) 
                             WHERE (1=1)
            ");

            if (!string.IsNullOrEmpty(KIND))
                sbCmd2.Append("AND KIND=@KIND ");

            DbCommand dbCommand2 = db.GetSqlStringCommand(sbCmd2.ToString());

            #region Add In Parameter

            db.AddInParameter(dbCommand2, "@KIND", DbType.String, KIND);

            #endregion

            try
            {
                dt2 = db.ExecuteDataSet(dbCommand2).Tables[0];

            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }

            return dt2;
        }

    }
}
