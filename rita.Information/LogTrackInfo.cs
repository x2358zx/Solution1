using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Information
{
    public class LogTrackInfo
    {
        #region 公用變數

        /// <summary>
        /// 修改來源IP
        /// </summary>
        public string ModifyFromIP { get; set; }

        /// <summary>
        /// 修改USER ID
        /// </summary>
        public string ModifyUser { get; set; }

        /// <summary>
        /// 作業模式
        /// </summary>
        public string ModifyMode { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public string ModifyTime { get; set; }

        /// <summary>
        /// 存取TABLE
        /// </summary>
        public string ModifyTable { get; set; }

        /// <summary>
        /// 修改內容
        /// </summary>
        public string ModifyKeyValue { get; set; }


        /// <summary>
        /// 修改前

        /// </summary>
        public string ModifyBefore { get; set; }

        /// <summary>
        /// 修改後

        /// </summary>
        public string ModifyAfter { get; set; }

        #endregion


        /// <summary>
        /// 新增異動紀錄

        /// </summary>
        /// <param name="infoObject">異動紀錄資訊</param>
        /// <returns></returns>


    }
}
