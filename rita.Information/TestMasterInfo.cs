using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Information
{
    public class TestMasterInfo
    {
        public TestMasterInfo()
        {
            DateTime cur = DateTime.Now;
            ID = Guid.NewGuid().ToString();
            CreateTime = cur;
            UpdateTime = cur;
        }
        
        #region Fields
        
        [Key]
        [Column(Order = 0)]
        [Display(Name = "索引值")]
        public int SID { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(36)]

        [Display(Name = "ID")]
        public string ID { get; set; }


        [Required]//必要欄位
        [StringLength(10)]//字的長度
        [Display(Name = "編號")]
        public string NO { get; set; }


        [Required(ErrorMessage = "白癡哦")]//必要
        [StringLength(10, ErrorMessage = "白癡哦十碼啦")]
        [Display(Name = "姓名")]
        public string Name { get; set; }


        [StringLength(50)]
        [Display(Name = "電話")]
        public string Phone { get; set; }


        [StringLength(256)]
        [Display(Name = "地址")]
        public string Address { get; set; }


        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime? Birthday { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        [Display(Name = "年齡")]
        public decimal? Age { get; set; }


        [Display(Name = "建立日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "修改日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime UpdateTime { get; set; }
        #endregion

        public class Conditions
        {

            [Display(Name = "索引值")]
            public int? SID { get; set; }

            [StringLength(36)]
            [Display(Name = "ID")]
            public string ID { get; set; }

            [StringLength(10)]
            [Display(Name = "編號")]
            public string NO { get; set; }

            [StringLength(10)]
            [Display(Name = "名稱")]
            public string Name { get; set; }

            [StringLength(50)]
            [Display(Name = "電話")]
            public string Phone { get; set; }

            [StringLength(256)]
            [Display(Name = "地址")]
            public string Address { get; set; }

            [Display(Name = "生日從")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime? BirthdayFrom { get; set; }

            [Display(Name = "生日到")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime? BirthdayTo { get; set; }

            [Display(Name = "年齡從")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
            public decimal? AgeFrom { get; set; }

            [Display(Name = "年齡到")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
            public decimal? AgeTo { get; set; }
        }

    }
}
