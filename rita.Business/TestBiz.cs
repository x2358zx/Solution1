using rita.DataAccess;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace rita.Business
{
    public class TestBiz
    {
        public class Master
        {
            /// <summary>
            /// 新增
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Insert(TestMasterInfo entity)
            {
                var result = 0;
                try
                {
                    //TestMasterInfo result = null;

                    //int x = 1;
                    //int y = 0;
                    using (TransactionScope scope =
                        new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions
                        {
                            IsolationLevel =System.Transactions.IsolationLevel.ReadCommitted
                        }))
                    {
                        var dbMaster = new TestMasterDB();
                        int rMaster = dbMaster.Insert(entity);

                        //decimal z = x / y;

                        var dbDteail = new TestDeatailDB();
                        int rDteail = dbDteail.Insert(new TestDeatailInfo
                        {
                            ID = Guid.NewGuid().ToString(),
                            MasterID = entity.ID,
                            A = "adaasad"
                        });
                        result = rMaster + rDteail;
                        scope.Complete();
                    }

                    
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Insert", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 修改
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Update(TestMasterInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Update(entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Update", ErrMsg = ex.Message });
                }
                return result;
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
                var result = 0;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Delete(SID, ID, NO);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Delete", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 取一筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public TestMasterInfo Load(int? SID=null, string ID=null, string NO=null)
            {
                TestMasterInfo result = null;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(SID, ID, NO);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 依條件取多筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="Name"></param>
            /// <param name="Phone"></param>
            /// <param name="Address"></param>
            /// <param name="BirthdayFrom"></param>
            /// <param name="BirthdayTo"></param>
            /// <param name="AgeFrom"></param>
            /// <param name="AgeTo"></param>
            /// <returns></returns>
            public DataTable Load(ref PaginationInfo pagination, int? SID = null, string ID = null, string NO = null, string Name = null, string Phone = null,
                string Address = null, DateTime? BirthdayFrom = null, DateTime? BirthdayTo = null,
                decimal? AgeFrom = null, decimal? AgeTo = null)
            {
                DataTable result = new DataTable();
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(ref pagination, SID, ID: ID, NO: NO, Name: Name, Phone: Phone, Address: Address,
                        BirthdayFrom: BirthdayFrom, BirthdayTo: BirthdayTo, AgeFrom: AgeFrom, AgeTo: AgeTo);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
            public DataTable Load(ref PaginationInfo pagination, TestMasterInfo.Conditions conditions)
            {
                DataTable result = new DataTable();
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(ref pagination, conditions);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }

        }
        public class Detail
        {
            /// <summary>
            /// 新增
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Insert(TestDeatailInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestDeatailDB();
                    result = db.Insert(entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Insert", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 修改
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Update(int? SID, string ID, string MasterID, TestDeatailInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestDeatailDB();
                    result = db.Update(SID, ID, MasterID, entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Update", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 刪除
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public int Delete(int? SID, string ID, string MasterID)
            {
                var result = 0;
                try
                {
                    var db = new TestDeatailDB();
                    result = db.Delete(SID, ID, MasterID);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Delete", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 取一筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public TestDeatailInfo Load(int? SID, string ID, string MasterID)
            {
                TestDeatailInfo result = null;
                try
                {
                    var db = new TestDeatailDB();
                    result = db.Load(SID, ID, MasterID);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 依條件取多筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="Name"></param>
            /// <param name="Phone"></param>
            /// <param name="Address"></param>
            /// <param name="BirthdayFrom"></param>
            /// <param name="BirthdayTo"></param>
            /// <param name="AgeFrom"></param>
            /// <param name="AgeTo"></param>
            /// <returns></returns>
            //public DataTable Load(int? SID, string ID = null, string MasterID = null, string Search = null)
            //{
            //    DataTable result = new DataTable();
            //    try
            //    {
            //        var db = new TestDeatailDB();
            //        result = db.Load(SID, ID, MasterID, Search);
            //    }
            //    catch (Exception ex)
            //    {
            //        var dbExpLog = new ExpLogDB();
            //        dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Load", ErrMsg = ex.Message });
            //    }
            //    return result;
            //}
        }
    }
}
