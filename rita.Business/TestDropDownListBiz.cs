using rita.DataAccess;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Business
{
    public class TestDropDownListBiz
    {
        /// <summary>
        /// 取一筆資料
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="ID"></param>
        /// <param name="NO"></param>
        /// <returns></returns>
        //public TestMasterInfo Load(int? SID = null, string ID = null, string NO = null)
        //{
        //    TestMasterInfo result = null;
        //    try
        //    {
        //        var db = new TestMasterDB();
        //        result = db.Load(SID, ID, NO);
        //    }
        //    catch (Exception ex)
        //    {
        //        var dbExpLog = new ExpLogDB();
        //        dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
        //    }
        //    return result;
        //}
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
        public DataTable Load(string KIND)
        {
            DataTable result = new DataTable();
            try
            {
                var db = new TestDropDownListDB();
                //result = db.Load(KIND: KIND);
            }
            catch (Exception ex)
            {
                var dbExpLog = new ExpLogDB();
                dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
            }
            return result;
        }

    }
}
