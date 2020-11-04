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
    public class ExpLogBiz
    {
        /// <summary>
        /// 新增 Exception Log
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public bool Insert(ExpLogInfo entity)
        {
            var result = true;
            try
            {
                var db = new ExpLogDB();
                db.Insert(entity);
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(ExpLogInfo entity)
        {
            var result = true;
            try
            {
                var db = new ExpLogDB();
                db.Update(entity);
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool Delete(int SID)
        {
            var result = true;
            try
            {
                var db = new ExpLogDB();
                db.Delete(SID);
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 以SID取一筆資料
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public ExpLogInfo Load(int SID)
        {
            ExpLogInfo result = new ExpLogInfo();
            try
            {
                var db = new ExpLogDB();
                result = db.Load(SID);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        /// <summary>
        /// 依條件取多筆資料 (目前 TOP 10)
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            DataTable result = new DataTable();
            try
            {
                var db = new ExpLogDB();
                result = db.Load();
            }
            catch
            {
                result = null;
            }
            return result;
        }

    }
}
