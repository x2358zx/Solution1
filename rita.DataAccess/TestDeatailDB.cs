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
    public class TestDeatailDB : baseDB
    {
        public TestDeatailDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TestDeatailInfo entity)
        {

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                INSERT INTO [TestDeatails]
                           ([ID]
                           ,[MasterID]
                           ,[A]
                           ,[B]
                           ,[C]
                           ,[D]
                           ,[E]
                           ,[F]
                           ,[G])
                     VALUES
                           (@ID
                           ,@MasterID
                           ,@A
                           ,@B
                           ,@C
                           ,@D
                           ,@E
                           ,@F
                           ,@G)
               ");
            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            db.AddInParameter(dbCommand, "@MasterID", DbType.String, entity.MasterID);
            db.AddInParameter(dbCommand, "@A", DbType.String, entity.A);
            db.AddInParameter(dbCommand, "@B", DbType.String, entity.B);
            db.AddInParameter(dbCommand, "@C", DbType.String, entity.C);
            db.AddInParameter(dbCommand, "@D", DbType.DateTime, entity.D);
            db.AddInParameter(dbCommand, "@E", DbType.Decimal, entity.E);
            db.AddInParameter(dbCommand, "@F", DbType.DateTime, entity.F);
            db.AddInParameter(dbCommand, "@G", DbType.DateTime, entity.G);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="MasterID"></param>
        /// <returns></returns>
        public int Delete(int? SID, string ID, string MasterID)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(MasterID))
                return 0;

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                DELETE FROM [TestDeatail]
                 WHERE (1=1)
               ");
            if (SID.HasValue)
                sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND SID=@ID ");
            if (!string.IsNullOrEmpty(MasterID))
                sbCmd.Append("AND SID=@MasterID ");


            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID))
                db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 修改更新
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="MasterID"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(int? SID, string ID, string MasterID, TestDeatailInfo entity)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(MasterID))
                return 0;

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                    UPDATE [TestDeatail]
                       SET [ID] = @ID
                          ,[MasterID] = @MasterID
                          ,[Name] = @Name
                          ,[B] = @B
                          ,[C] = @C
                          ,[D] = @D
                          ,[E] = @E
                          ,[F] = @F
                          ,[G] = @G
                     WHERE (1=1) 
               ");
            if (SID.HasValue)
                sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND SID=@ID ");
            if (!string.IsNullOrEmpty(MasterID))
                sbCmd.Append("AND SID=@MasterID ");


            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            if (!string.IsNullOrEmpty(MasterID))
                db.AddInParameter(dbCommand, "@MasterID", DbType.String, entity.MasterID);
            db.AddInParameter(dbCommand, "@A", DbType.String, entity.A);
            db.AddInParameter(dbCommand, "@B", DbType.String, entity.B);
            db.AddInParameter(dbCommand, "@C", DbType.String, entity.C);
            db.AddInParameter(dbCommand, "@D", DbType.DateTime, entity.D);
            db.AddInParameter(dbCommand, "@E", DbType.Decimal, entity.E);
            db.AddInParameter(dbCommand, "@F", DbType.DateTime, entity.F);
            db.AddInParameter(dbCommand, "@G", DbType.DateTime, entity.G);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 讀取
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="MasterID"></param>
        /// <returns></returns>

        public TestDeatailInfo Load(int? SID, string ID, string MasterID)
        {
            TestDeatailInfo Result = new TestDeatailInfo();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [TestDeatail] WITH (MasterIDlock) ");
            sbCmd.Append("	WHERE  (1=1) 		");
            if (SID.HasValue)
                sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND ID=@ID ");
            if (!string.IsNullOrEmpty(MasterID))
                sbCmd.Append("AND MasterID=@MasterID ");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID))
                db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);

            #endregion

            try
            {
                base.ErrFlag = true;
                DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    DataRow dr = dtTemp.Rows[0];
                    Result.SID = Convert.ToInt32(dr["SID"]);
                    Result.ID = Convert.ToString(dr["ID"]);
                    Result.MasterID = Convert.ToString(dr["MasterID"]);
                    Result.A = Convert.ToString(dr["A"]);
                    Result.B = Convert.ToString(dr["B"]);
                    Result.C = Convert.ToString(dr["C"]);
                    Result.D = Convert.ToString(dr["D"]);
                    Result.E = Convert.ToString(dr["E"]);
                    Result.F = Convert.ToString(dr["F"]);
                    Result.G = Convert.ToString(dr["G"]);
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

        public DataTable Load(int? SID, string ID, string MasterID, string A, string B, string C, string D, string E)
        {

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [TestDeatail] WITH (MasterIDlock) ");
            sbCmd.Append("	WHERE  SID = @SID 		");
            if (SID.HasValue)
                sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND ID=@ID ");
            if (!string.IsNullOrEmpty(MasterID))
                sbCmd.Append("AND MasterID=@MasterID ");
            if (!string.IsNullOrEmpty(A))
                sbCmd.Append("AND A=@A ");
            if (!string.IsNullOrEmpty(B))
                sbCmd.Append("AND B=@B ");
            if (!string.IsNullOrEmpty(C))
                sbCmd.Append("AND C=@C ");
            if (!string.IsNullOrEmpty(D))
                sbCmd.Append("AND D=@D ");
            if (!string.IsNullOrEmpty(E))
                sbCmd.Append("AND E=@E ");
            


            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region
            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID))
                db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);
            if (!string.IsNullOrEmpty(A))
                db.AddInParameter(dbCommand, "@A", DbType.String, A);
            if (!string.IsNullOrEmpty(B))
                db.AddInParameter(dbCommand, "@B", DbType.String, B);
            if (!string.IsNullOrEmpty(C))
                db.AddInParameter(dbCommand, "@C", DbType.String, C);
            if (!string.IsNullOrEmpty(D))
                db.AddInParameter(dbCommand, "@D", DbType.String, D);
            if (!string.IsNullOrEmpty(E))
                db.AddInParameter(dbCommand, "@E", DbType.String, E);
            
            #endregion

            return ExecuteDataSet(db, dbCommand).Tables[0];
        }
    }
}
