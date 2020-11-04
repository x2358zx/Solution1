using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace rita.DataAccess
{
    public class baseDB
    {
        #region 基本處理
        public string UserID { get; set; }
        public string FunctionID { get; set; }
        /// <summary>
        /// 連線字串
        /// </summary>
        private string _dbInstanceName;

        /// <summary>
        /// 連線字串
        /// </summary>
        protected string DBInstanceName
        {
            get
            {
                return _dbInstanceName;
            }
            set
            {
                _dbInstanceName = value;
            }
        }

        /// <summary>
        /// 使用InstanceName取得資料連線設定
        /// </summary>
        /// <param name="InstanceName"></param>
        /// <returns></returns>
        protected Database GetDatabase()
        {
            if (!string.IsNullOrEmpty(DBInstanceName))
            {
                //Web UI 使用的Connection String不要用 Connection Pool
                return new SqlDatabase(Vista.SEC.ConnectionPool.GetConnection(DBInstanceName));
            }
            else
            {
                return null;
            }

        }

        public DbTransaction GetDbTransaction(DbConnection conn)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            return conn.BeginTransaction();
        }

        protected Database GetPIPADatabase()
        {
            if (!string.IsNullOrEmpty("CONNPIPA"))
            {
                string strConnectionString = Vista.SEC.ConnectionPool.GetConnection("CONNPIPA");
                if (strConnectionString.ToUpper().IndexOf("TIMEOUT") < 0)
                {
                    strConnectionString += ";Connection Timeout=3;";
                }
                return new SqlDatabase(strConnectionString);
                //return new SqlDatabase("Server=192.168.0.110;database=DBS_RES;uid=sa;pwd=1");
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(Database db, DbCommand dbCommand, bool InsLog = true)
        {
            int iAffectedNum;
            return ExecuteNonQuery(db, dbCommand, null, out iAffectedNum, InsLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="trans"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(Database db, DbCommand dbCommand, DbTransaction trans, bool InsLog = true)
        {
            int iAffectedNum;
            return ExecuteNonQuery(db, dbCommand, trans, out iAffectedNum, InsLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="iAffectedNum"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(Database db, DbCommand dbCommand, out int iAffectedNum, bool InsLog = true)
        {
            return ExecuteNonQuery(db, dbCommand, null, out iAffectedNum, InsLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="trans"></param>
        /// <param name="iAffectedNum"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(Database db, DbCommand dbCommand, DbTransaction trans, out int iAffectedNum, bool InsLog = true)
        {
            DbTransaction _trans;
            DbConnection conn = null;
            if (trans == null)
            {
                conn = db.CreateConnection();
                conn.Open();
                _trans = GetDbTransaction(conn);
            }
            else
            {
                _trans = trans;
            }

            try
            {
                iAffectedNum = db.ExecuteNonQuery(dbCommand, _trans);

                ErrFlag = (iAffectedNum <= 0 ? false : true);

                if (trans == null)
                {
                    _trans.Commit();
                    conn.Close();
                }
                return ErrFlag;
            }
            catch (Exception ex)
            {
                //預防遇到某些致命錯誤導致Transaction中斷,所以重新再取得Trans物件作Rollback
                if (_trans.Connection == null)
                {
                    conn = db.CreateConnection();
                    conn.Open();
                    _trans = GetDbTransaction(conn);
                }

                if (trans == null)
                {
                    _trans.Rollback();
                    conn.Close();
                }

                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)
                //取得目前MethodName
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();

                ErrFlag = false;
                ErrMsg = ex.ToString();
                ErrMethodName = myMethodBase.Name.ToString();
                LogExpInf();
                #endregion

                throw; //將原來的 exception 再次的抛出去
            }
            finally
            {
                if (InsLog) InsPIPALog(dbCommand, UserID, FunctionID);
            }
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="InsLog">預設 True</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(Database db, DbCommand dbCommand, bool InsLog = true)
        {
            return ExecuteDataSet(db, dbCommand, null, InsLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="trans"></param>
        /// <param name="InsLog">預設 True</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(Database db, DbCommand dbCommand, DbTransaction trans, bool InsLog = true)
        {
            DataSet dsTemp = new DataSet();
            DbTransaction _trans;
            DbConnection conn = null;
            if (trans == null)
            {
                conn = db.CreateConnection();
                conn.Open();
                _trans = GetDbTransaction(conn);
            }
            else
            {
                _trans = trans;
            }

            try
            {
                dsTemp = db.ExecuteDataSet(dbCommand, _trans);


                if (trans == null)
                {
                    _trans.Commit();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //預防遇到某些致命錯誤導致Transaction中斷,所以重新再取得Trans物件作Rollback
                if (_trans.Connection == null)
                {
                    conn = db.CreateConnection();
                    conn.Open();
                    _trans = GetDbTransaction(conn);
                }

                if (trans == null)
                {
                    _trans.Rollback();
                    conn.Close();
                }
                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)
                //取得目前MethodName
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();

                ErrFlag = false;
                ErrMsg = ex.ToString();
                ErrMethodName = myMethodBase.Name.ToString();
                LogExpInf();
                #endregion

                throw; //將原來的 exception 再次的抛出去
            }
            finally
            {
                if (InsLog) InsPIPALog(dbCommand, UserID, FunctionID);
            }

            return dsTemp;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public object ExecuteScalar(Database db, DbCommand dbCommand, bool InsLog = false)
        {
            return ExecuteScalar(db, dbCommand, null, InsLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="trans"></param>
        /// <param name="InsLog">預設 False</param>
        /// <returns></returns>
        public object ExecuteScalar(Database db, DbCommand dbCommand, DbTransaction trans, bool InsLog = false)
        {
            object o = null;
            DbTransaction _trans;
            DbConnection conn = null;
            if (trans == null)
            {
                conn = db.CreateConnection();
                conn.Open();
                _trans = GetDbTransaction(conn);
            }
            else
            {
                _trans = trans;
            }

            try
            {
                o = db.ExecuteScalar(dbCommand, _trans);

                if (trans == null)
                {
                    _trans.Commit();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //預防遇到某些致命錯誤導致Transaction中斷,所以重新再取得Trans物件作Rollback
                if (_trans.Connection == null)
                {
                    conn = db.CreateConnection();
                    conn.Open();
                    _trans = GetDbTransaction(conn);
                }

                if (trans == null)
                {
                    _trans.Rollback();
                    conn.Close();
                }

                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)
                //取得目前MethodName
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();

                ErrFlag = false;
                ErrMsg = ex.ToString();
                ErrMethodName = myMethodBase.Name.ToString();
                LogExpInf();
                #endregion

                throw; //將原來的 exception 再次的抛出去
            }
            finally
            {
                if (InsLog) InsPIPALog(dbCommand, UserID, FunctionID);
            }

            return o;
        }
        #endregion

        #region PIPA LOG
        public bool InsPIPALog(DbCommand logdbCommand, string LogUser, string LogFunction)
        {
            string LogParameter = "";
            foreach (DbParameter iPara in logdbCommand.Parameters)
            {
                LogParameter += iPara.ParameterName + ":" + iPara.Value.ToString() + "; ";
            }

            try
            {
                Database db = GetPIPADatabase();
                if (db == null) return false;
                StringBuilder sbCmd = new StringBuilder();

                sbCmd.AppendLine(@"INSERT INTO dbo.PIPA_LOG
 (LogTime ,LogUser ,LogFunction ,LogSql ,LogParameter)
 VALUES
 (getdate() ,@LogUser ,@LogFunction ,@LogSql ,@LogParameter) ");

                DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
                dbCommand.CommandTimeout = 3;

                #region 參數指定
                db.AddInParameter(dbCommand, "@LogUser", DbType.String, LogUser);
                db.AddInParameter(dbCommand, "@LogFunction", DbType.String, LogFunction);
                db.AddInParameter(dbCommand, "@LogSql", DbType.String, logdbCommand.CommandText);
                db.AddInParameter(dbCommand, "@LogParameter", DbType.String, LogParameter);
                #endregion



                int i = db.ExecuteNonQuery(dbCommand);
                ErrFlag = (i == 0 ? false : true);

                return ErrFlag;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 物件狀態處理
        /// <summary>
        /// 列舉物件編輯狀態
        /// </summary>
        protected enum EditType
        {
            Insert,
            Update
        }

        protected EditType EditMode = EditType.Insert;

        #endregion

        #region Log Functions

        #region ErrorMessage
        /// <summary>
        /// 錯誤狀態Info
        /// </summary>
        public rita.Information.ErrInfo ErrInfo = new rita.Information.ErrInfo();

        /// <summary>
        /// 錯誤檢查 Trus為執行成功 / False為發生錯誤
        /// </summary>
        public bool ErrFlag
        {
            get { return ErrInfo.ErrFlag; }
            set
            {
                ErrInfo.ErrFlag = value;

                //狀態清除時重設相關欄位
                if (value)
                {
                    this.ErrInfo.ErrMethodName = "";
                    this.ErrInfo.ErrMsg = "";
                }
            }
        }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrMsg
        {
            get { return ErrInfo.ErrMsg; }
            set { ErrInfo.ErrMsg = value; }
        }

        /// <summary>
        /// 錯誤Method
        /// </summary>
        public string ErrMethodName
        {
            get { return ErrInfo.ErrMethodName; }
            set { ErrInfo.ErrMethodName = value; }
        }
        #endregion

        #region TrackMessage
        /// <summary>
        /// Track Mode ( Add / Mod / Del )
        /// </summary>
        [NonSerialized()]
        public string TrackMode;

        /// <summary>
        /// Track Table Name
        /// </summary>
        [NonSerialized()]
        public string TrackTable;

        /// <summary>
        /// Track MSG / Key
        /// </summary>
        [NonSerialized()]
        public string TrackMsg;


        /// <summary>
        /// Track Before
        /// </summary>
        [NonSerialized()]
        public string TrackBefore;


        /// <summary>
        /// Track After
        /// </summary>
        [NonSerialized()]
        public string TrackAfter;


        #endregion

        #region Execute Log

        /// <summary>
        /// 記錄Exp資訊
        /// </summary>
        public void LogExpInf()   //UAT SIT peotected to public for odr002 test old data
        {
            //記錄狀態為Exception
            this.ErrFlag = false;

            //寫入Log   
            //Mike.Information.ExpLogInfo myLogExpInfo = new Mike.Information.ExpLogInfo();
            //myLogExpInfo.ClassName = this.GetType().FullName.ToString();
            //myLogExpInfo.MethodName = this.ErrMethodName;
            //myLogExpInfo.ErrMsg = this.ErrMsg;
            //myLogExpInfo.UDate = DateTime.Now;
            //myLogExpInfo.Insert();
            rita.DataAccess.ExpLogDB db = new ExpLogDB();
            //db.Insert(myLogExpInfo);
            db.Insert(new Information.ExpLogInfo
            {
                ClassName = this.GetType().FullName.ToString(),
                MethodName = this.ErrMethodName,
                ErrMsg = this.ErrMsg,
                UDate = DateTime.Now
            });
        }

        /// <summary>
        /// 記錄Track資訊
        /// </summary>
        protected void LogTrackInf()
        {
            ////寫入Track          
            //Mike.Information.LogTrackInfo myTrackInfo = new Mike.Information.LogTrackInfo();
            //myTrackInfo.ModifyMode = this.TrackMode;
            //myTrackInfo.ModifyTable = this.TrackTable;
            //myTrackInfo.ModifyKeyValue = this.TrackMsg;
            //myTrackInfo.ModifyBefore = this.TrackBefore;
            //myTrackInfo.ModifyAfter = this.TrackAfter;
            //myTrackInfo.Insert();

            LogTrackDB db = new LogTrackDB();
            db.Insert(new Information.LogTrackInfo
            {
                ModifyMode = this.TrackMode,
                ModifyTable = this.TrackTable,
                ModifyKeyValue = this.TrackMsg,
                ModifyBefore = this.TrackBefore,
                ModifyAfter = this.TrackAfter
            });

        }
        #endregion

        ////記錄Track Log Sample
        //base.TrackMode = "Select" / "ADD" / "MOD" / "DEL" 
        //base.TrackTable = "Cust";
        //base.TrackMsg = "OK" / "other description"
        //base.LogTrackInf();

        ///記錄Exception Log Sample        ///
        #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)
        ////取得目前MethodName
        //System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();

        //base.ErrFlag = false;
        //base.ErrMsg = ex.ToString();
        //base.ErrMethodName = myMethodBase.Name.ToString();
        //base.LogExpInf(); 
        #endregion

        #endregion

        #region 序列化處理
        /// <summary>
        /// 序列化物件
        /// </summary>
        /// <param name="strNameSpace">class名稱</param>
        /// <returns></returns>
        public string DoSerial(string strNameSpace)
        {
            System.Reflection.Assembly myAs = Assembly.Load("Vista.Information");
            Type myType = myAs.GetType("Vista.Information." + strNameSpace, true);

            XmlSerializer mySerializer = new XmlSerializer(myType);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            //序列化

            mySerializer.Serialize(sw, this);
            string xmlForm = sw.ToString();
            sw.Close();

            #region 移除不需轉換的欄位 ErrFlag / ErrMsg / ErrMethodName
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlForm);

            xmlForm = xDoc.InnerXml.ToString();
            #endregion
            return xmlForm;
        }

        //解序列化
        /// <summary>
        /// 解序列化
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="strNameSpace">class名稱</param>
        /// <returns></returns>
        public object DeSerialize(string temp, string strNameSpace)
        {
            //取得物件名稱
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.LoadXml(temp);

            System.Reflection.Assembly myAs = Assembly.Load("Vista.Information");

            Type myType = myAs.GetType("Vista.Information." + strNameSpace, true);
            StringReader sr = new StringReader(temp);

            object myPredict = new object();

            XmlSerializer mySerializer;

            mySerializer = new XmlSerializer(myType);
            myPredict = mySerializer.Deserialize(sr);

            sr.Close();

            return myPredict;
        }
        #endregion
    }
}
