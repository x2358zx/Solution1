using Microsoft.Practices.EnterpriseLibrary.Data;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.DataAccess
{

    public class ExpLogDB : baseDB
    {
        public ExpLogDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }
        /// <summary>
        /// 新增Exception Log
        /// </summary>
        /// <param name="e"></param>
        public void Insert(rita.Information.ExpLogInfo e)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	INSERT INTO [ExpLog]		");
            sbCmd.Append("		(				");
            sbCmd.Append("		ClassName		");
            sbCmd.Append("		,MethodName		");
            sbCmd.Append("		,ErrMsg		");
            sbCmd.Append("		,UDate		");
            sbCmd.Append("		)				");
            sbCmd.Append("	VALUES		");
            sbCmd.Append("		(				");
            sbCmd.Append("		@ClassName		");
            sbCmd.Append("		,@MethodName		");
            sbCmd.Append("		,@ErrMsg		");
            sbCmd.Append("		,@UDate		");
            sbCmd.Append("		)				");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter
            db.AddInParameter(dbCommand, "@ClassName", DbType.String, e.ClassName);
            db.AddInParameter(dbCommand, "@MethodName", DbType.String, e.MethodName);
            db.AddInParameter(dbCommand, "@ErrMsg", DbType.String, e.ErrMsg);
            db.AddInParameter(dbCommand, "@UDate", DbType.DateTime, e.UDate);
            #endregion

            try
            {
                db.ExecuteNonQuery(dbCommand);
                base.ErrFlag = true;
            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }
        }
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="e"></param>
        public void Update(ExpLogInfo e)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	UPDATE [ExpLog] SET 		");
            sbCmd.Append("		ClassName = @ClassName 		");
            sbCmd.Append("		,MethodName = @MethodName 		");
            sbCmd.Append("		,ErrMsg = @ErrMsg 		");
            sbCmd.Append("		,UDate = @UDate 		");
            sbCmd.Append("	WHERE SID = @SID 		");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter
            db.AddInParameter(dbCommand, "@SID", DbType.Int32, e.SID);
            db.AddInParameter(dbCommand, "@ClassName", DbType.String, e.ClassName);
            db.AddInParameter(dbCommand, "@MethodName", DbType.String, e.MethodName);
            db.AddInParameter(dbCommand, "@ErrMsg", DbType.String, e.ErrMsg);
            db.AddInParameter(dbCommand, "@UDate", DbType.DateTime, e.UDate);
            #endregion

            try
            {
                db.ExecuteNonQuery(dbCommand);
                base.ErrFlag = true;
            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        public void Delete(int iSID)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	DELETE [ExpLog]		");
            sbCmd.Append("	WHERE SID = @SID 		");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            db.AddInParameter(dbCommand, "@SID", DbType.Int32, iSID);

            #endregion

            try
            {
                db.ExecuteNonQuery(dbCommand);
                base.ErrFlag = true;
            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }
        }
        /// <summary>
        /// 以SID取資料
        /// </summary>
        /// <param name="iSID"></param>
        /// <returns></returns>
        public ExpLogInfo Load(int iSID)
        {
            ExpLogInfo Result = new ExpLogInfo();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [ExpLog] WITH (Nolock) ");
            sbCmd.Append("	WHERE  SID = @SID 		");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            db.AddInParameter(dbCommand, "@SID", DbType.Int32, iSID);

            #endregion

            try
            {
                base.ErrFlag = true;
                DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    DataRow dr = dtTemp.Rows[0];
                    Result.SID = Convert.ToInt32(dr["SID"]);
                    Result.ClassName = Convert.ToString(dr["ClassName"]);
                    Result.MethodName = Convert.ToString(dr["MethodName"]);
                    Result.ErrMsg = Convert.ToString(dr["ErrMsg"]);
                    Result.UDate = dr["UDate"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["UDate"]);
                }
                else
                    Result = null;
            }
            catch (Exception ex)
            {
                Result = null;
                throw; //將原來的 exception 再次的抛出去
            }

            return Result;
        }
        public ExpLogInfo Load(DateTime UDate)
        {
            ExpLogInfo Result = new ExpLogInfo();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [ExpLog] WITH (Nolock) ");
            sbCmd.Append("	WHERE  cast(UDate as date)=@UDate		");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            db.AddInParameter(dbCommand, "@Udate", DbType.Date, UDate.Date);

            #endregion

            try
            {
                base.ErrFlag = true;
                DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    DataRow dr = dtTemp.Rows[0];
                    Result.SID = Convert.ToInt32(dr["SID"]);
                    Result.ClassName = Convert.ToString(dr["ClassName"]);
                    Result.MethodName = Convert.ToString(dr["MethodName"]);
                    Result.ErrMsg = Convert.ToString(dr["ErrMsg"]);
                    Result.UDate = dr["UDate"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["UDate"]);
                }
                else
                    Result = null;
            }
            catch (Exception ex)
            {
                Result = null;
                throw; //將原來的 exception 再次的抛出去
            }

            return Result;
        }
        /// <summary>
        /// 依條件取多筆資料
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            DataTable Result = new DataTable();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT TOP 10 * FROM [ExpLog] WITH (Nolock) ORDER BY UDate DESC");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            //db.AddInParameter(dbCommand, "@SID", DbType.Int32, iSID);

            #endregion

            try
            {
                Result = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw; //將原來的 exception 再次的抛出去
            }

            return Result;
        }

    }

}
