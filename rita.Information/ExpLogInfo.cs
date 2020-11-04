using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Information
{
    public class ExpLogInfo
    {
        public ExpLogInfo()
        {
            UDate = DateTime.Now;
        }

        /// <summary>
        /// SID:Primary key
        /// </summary>
        public int SID { get; set; }
        /// <summary>
        /// Class Name
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// Method Name
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrMsg { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? UDate { get; set; }

    }
}
