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
    public class TestMasterDB : baseDB
    {
        public TestMasterDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TestMasterInfo entity)
        {

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                INSERT INTO [TestMaster]
                           ([ID]
                           ,[NO]
                           ,[Name]
                           ,[Phone]
                           ,[Address]
                           ,[Birthday]
                           ,[Age]
                           ,[CreateTime]
                           ,[UpdateTime])
                     VALUES
                           (@ID
                           ,@NO
                           ,@Name
                           ,@Phone
                           ,@Address
                           ,@Birthday
                           ,@Age
                           ,@CreateTime
                           ,@UpdateTime)
               ");
            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            db.AddInParameter(dbCommand, "@NO", DbType.String, entity.NO);
            db.AddInParameter(dbCommand, "@Name", DbType.String, entity.Name);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, entity.Phone);
            db.AddInParameter(dbCommand, "@Address", DbType.String, entity.Address);
            db.AddInParameter(dbCommand, "@Birthday", DbType.DateTime, entity.Birthday);
            db.AddInParameter(dbCommand, "@Age", DbType.Decimal, entity.Age);
            db.AddInParameter(dbCommand, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(dbCommand, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="NO"></param>
        /// <returns></returns>
        public int Delete(int? SID, string ID, string NO)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO))
                return 0;

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                DELETE FROM [TestMaster]
                 WHERE (1=1) 
               ");
            if (SID.HasValue)
                sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND ID=@ID ");
            if (!string.IsNullOrEmpty(NO))
                sbCmd.Append("AND NO=@NO ");


            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO))
                db.AddInParameter(dbCommand, "@NO", DbType.String, NO);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 修改更新
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="NO"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TestMasterInfo entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.ID))
                return 0;

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                    UPDATE [TestMaster]
                       SET [Name] = @Name
                          ,[Phone] = @Phone
                          ,[Address] = @Address
                          ,[Birthday] = @Birthday
                          ,[Age] = @Age
                          ,[UpdateTime] = @UpdateTime
                     WHERE ID=@ID 
               ");
            //if (SID.HasValue)
                //sbCmd.Append("AND SID=@SID ");
            //if (!string.IsNullOrEmpty(entity.ID))
                //sbCmd.Append("");
            //if (!string.IsNullOrEmpty(NO))
                //sbCmd.Append("AND NO=@NO ");


            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());
            #region
            //if (SID.HasValue)
               // db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            //if (!string.IsNullOrEmpty(entity.ID))
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            if (!string.IsNullOrEmpty(entity.NO))
                db.AddInParameter(dbCommand, "@NO", DbType.String, entity.NO);
            db.AddInParameter(dbCommand, "@Name", DbType.String, entity.Name);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, entity.Phone);
            db.AddInParameter(dbCommand, "@Address", DbType.String, entity.Address);
            db.AddInParameter(dbCommand, "@Birthday", DbType.DateTime, entity.Birthday);
            db.AddInParameter(dbCommand, "@Age", DbType.Decimal, entity.Age);
            db.AddInParameter(dbCommand, "@UpdateTime", DbType.DateTime, DateTime.Now);
            #endregion
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 讀取
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="NO"></param>
        /// <returns></returns>

        public TestMasterInfo Load(int? SID=null, string ID=null, string NO=null)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO))
                return null;

            TestMasterInfo Result = new TestMasterInfo();

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [TestMaster] WITH (Nolock) ");
            sbCmd.Append("	WHERE  (1=1) 		");
            if (SID.HasValue)
                sbCmd.Append("AND SID = @SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND ID = @ID ");
            if (!string.IsNullOrEmpty(NO))
                sbCmd.Append("AND NO = @NO");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO))
                db.AddInParameter(dbCommand, "@NO", DbType.String, NO);

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
                    Result.NO = Convert.ToString(dr["NO"]);
                    Result.Name = Convert.ToString(dr["Name"]);
                    Result.Phone = Convert.ToString(dr["Phone"]);
                    Result.Address = Convert.ToString(dr["Address"]);
                    Result.Birthday = dr["Birthday"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["Birthday"]);
                    Result.Age = dr["Age"] == DBNull.Value ? new Nullable<decimal>() : Convert.ToDecimal(dr["Age"]);
                    Result.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    Result.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
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

        public DataTable Load(ref PaginationInfo pagination,int? SID, string ID, string NO, string Name = null, string Address = null,string Phone=null,
            DateTime? BirthdayFrom = null, DateTime? BirthdayTo = null, Decimal? AgeFrom = null, Decimal? AgeTo = null)
        {
            //if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO))
            //    return null;

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();
            #region 條件式
            sbCmd.Append(@"	SELECT ROW_NUMBER() over(order by updatetime desc,SID desc) rowno,*
                              FROM [TestMaster] WITH (Nolock) 
                              WHERE  (1=1)		"
                         );
            if (SID.HasValue)
                sbCmd.Append("AND SID like @SID ");
            if (!string.IsNullOrEmpty(ID))
                sbCmd.Append("AND ID like @ID ");
            if (!string.IsNullOrEmpty(NO))
                sbCmd.Append("AND NO like @NO ");
            if (!string.IsNullOrEmpty(Name))
                sbCmd.Append("AND Name like @Name ");
            if (!string.IsNullOrEmpty(Phone))
                sbCmd.Append("AND Phone like @Phone ");
            if (!string.IsNullOrEmpty(Address))
                sbCmd.Append("AND Address like @Address ");
            if (BirthdayFrom.HasValue)
                sbCmd.Append("AND Birthday>=@BirthdayFrom ");
            if (BirthdayTo.HasValue)
                sbCmd.Append("AND Birthday<=@BirthdayTo ");
            if (AgeFrom.HasValue)
                sbCmd.Append("AND Age>=@AgeFrom ");
            if (AgeTo.HasValue)
                sbCmd.Append("AND Age<=@AgeTo ");

            #endregion

            string sql = sbCmd.ToString();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            #region 
            if (SID.HasValue)
                db.AddInParameter(dbCommand, "@SID", DbType.String, SID.Value);
            if (!string.IsNullOrEmpty(ID))
                db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO))
                db.AddInParameter(dbCommand, "@NO", DbType.String, NO);
            if (!string.IsNullOrEmpty(Name))
                db.AddInParameter(dbCommand, "@Name", DbType.String, $"%{Name}%");
            if (!string.IsNullOrEmpty(Phone))
                db.AddInParameter(dbCommand, "@Phone", DbType.String, Phone);
            if (!string.IsNullOrEmpty(Address))
                db.AddInParameter(dbCommand, "@Address", DbType.String, Address);
            if (BirthdayFrom.HasValue)
                db.AddInParameter(dbCommand, "@BirthdayFrom", DbType.DateTime, BirthdayFrom);
            if (BirthdayTo.HasValue)
                db.AddInParameter(dbCommand, "@BirthdayTo", DbType.DateTime, BirthdayTo);
            if (AgeFrom.HasValue)
                db.AddInParameter(dbCommand, "@ID", DbType.Decimal, AgeFrom);
            if (AgeTo.HasValue)
                db.AddInParameter(dbCommand, "@NO", DbType.Decimal, AgeTo);
            #endregion
            if (pagination.Total ==0)
            { 
                dbCommand.CommandText = "with x as ( " + sql + ") select count(*) from x";
                pagination.Total = (int)ExecuteScalar(db, dbCommand);
            }

            dbCommand.CommandText = "with x as ( " + sql + ") select * from x where rowno between @s and @e";
            db.AddInParameter(dbCommand, "@s", DbType.Int32, (pagination.Index - 1) * pagination.Size + 1);
            db.AddInParameter(dbCommand, "@e", DbType.Int32, pagination.Size * pagination.Index);



            return ExecuteDataSet(db,dbCommand).Tables[0];
        }


        public DataTable Load(ref PaginationInfo pagination, TestMasterInfo.Conditions conditions)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                SELECT ROW_NUMBER() over(order by updatetime desc, SID desc) rowno,
                * 
                FROM [TestMaster] WITH (Nolock)  
                WHERE (1=1) "
                );
            #region 條件式

            if (conditions.SID.HasValue) sbCmd.Append(" AND SID=@SID");
            if (!string.IsNullOrEmpty(conditions.ID)) sbCmd.Append(" AND ID=@ID");
            if (!string.IsNullOrEmpty(conditions.NO)) sbCmd.Append(" AND NO=@NO");
            if (!string.IsNullOrEmpty(conditions.Name)) sbCmd.Append(" AND Name like @Name");
            if (!string.IsNullOrEmpty(conditions.Phone)) sbCmd.Append(" AND Phone like @Phone");
            if (!string.IsNullOrEmpty(conditions.Address)) sbCmd.Append(" AND Address like @Address");
            if (conditions.BirthdayFrom.HasValue) sbCmd.Append(" AND Cast(Birthday as date) >= @BirthdayFrom");
            if (conditions.BirthdayTo.HasValue) sbCmd.Append(" AND Cast(Birthday as date) <= @BirthdayTo");
            if (conditions.AgeFrom.HasValue) sbCmd.Append(" AND Age >= @AgeFrom");
            if (conditions.AgeTo.HasValue) sbCmd.Append(" AND Age <= @AgeTo");

            #endregion

            string sql = sbCmd.ToString();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            #region Add In Parameter

            if (conditions.SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, conditions.SID.Value);
            if (!string.IsNullOrEmpty(conditions.ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, conditions.ID);
            if (!string.IsNullOrEmpty(conditions.NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, conditions.NO);
            if (!string.IsNullOrEmpty(conditions.Name)) db.AddInParameter(dbCommand, "@Name", DbType.String, $"%{conditions.Name}%");
            if (!string.IsNullOrEmpty(conditions.Phone)) db.AddInParameter(dbCommand, "@Phone", DbType.String, $"%{conditions.Phone}%");
            if (!string.IsNullOrEmpty(conditions.Address)) db.AddInParameter(dbCommand, "@Address", DbType.String, $"%{conditions.Address}%");
            if (conditions.BirthdayFrom.HasValue) db.AddInParameter(dbCommand, "@BirthdayFrom", DbType.DateTime, conditions.BirthdayFrom.Value.Date);
            if (conditions.BirthdayTo.HasValue) db.AddInParameter(dbCommand, "@BirthdayTo", DbType.DateTime, conditions.BirthdayTo.Value.Date);
            if (conditions.AgeFrom.HasValue) db.AddInParameter(dbCommand, "@AgeFrom", DbType.Decimal, conditions.AgeFrom.Value);
            if (conditions.AgeTo.HasValue) db.AddInParameter(dbCommand, "@AgeTo", DbType.Decimal, conditions.AgeTo.Value);

            #endregion

            if (pagination.Total == 0)
            {
                dbCommand.CommandText = "with x as ( " + sql + " ) select count(*) from x";
                pagination.Total = (int)ExecuteScalar(db, dbCommand);
            }

            dbCommand.CommandText = "with x as (" + sql + ") select * from x where rowno between @s and @e";
            db.AddInParameter(dbCommand, "@s", DbType.Int32, (pagination.Index - 1) * pagination.Size + 1);
            db.AddInParameter(dbCommand, "@e", DbType.Int32, pagination.Size * pagination.Index);

            return ExecuteDataSet(db, dbCommand).Tables[0];
        }


    }
}
