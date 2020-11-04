using Microsoft.Practices.EnterpriseLibrary.Data;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace rita.DataAccess
{
    public class LogTrackDB:baseDB
    {
        public void Insert(LogTrackInfo e)
        {
            //不使用Transaction , 以正確的記錄發生那些Track事件
            using (System.Transactions.TransactionScope Ts = new System.Transactions.TransactionScope(TransactionScopeOption.Suppress))
            {

                //設定連結字串
                Database db = base.GetDatabase();

                StringBuilder sbCommand = new StringBuilder();

                sbCommand.Append("INSERT INTO Track_Log ");
                sbCommand.Append("  (ModifyFromIP, ModifyUser, ModifyMode, ModifyTable, ModifyKeyValue ,ModifyBefore , ModifyAfter) ");
                sbCommand.Append("VALUES ");
                sbCommand.Append("  (@ModifyFromIP, @ModifyUser, @ModifyMode,  @ModifyTable, @ModifyKeyValue,  @ModifyBefore, @ModifyAfter) ");

                DbCommand dbCommand = db.GetSqlStringCommand(sbCommand.ToString());

                db.AddInParameter(dbCommand, "@ModifyFromIP", DbType.String, e.ModifyFromIP);
                db.AddInParameter(dbCommand, "@ModifyUser", DbType.String, e.ModifyUser);
                db.AddInParameter(dbCommand, "@ModifyMode", DbType.String, e.ModifyMode);
                db.AddInParameter(dbCommand, "@ModifyTable", DbType.String, e.ModifyTable);
                db.AddInParameter(dbCommand, "@ModifyKeyValue", DbType.String, e.ModifyKeyValue);
                db.AddInParameter(dbCommand, "@ModifyKeyValue", DbType.String, e.ModifyBefore);
                db.AddInParameter(dbCommand, "@ModifyKeyValue", DbType.String, e.ModifyAfter);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
