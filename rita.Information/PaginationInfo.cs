using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Information
{
    [Serializable]
    public class PaginationInfo
    {
        /// <summary>
        /// 資料分頁
        /// </summary>
        public PaginationInfo()
        {
            Index = 1;
            Size = 3;
            Total = 0;
        }
        /// <summary>
        /// 目前頁面
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 總筆數
        /// </summary>
        public int Total { get; set; }
    }
}
