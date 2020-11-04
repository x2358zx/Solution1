using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rita.Information
{
    public class TestDeatailInfo
    {
        [Key]
        [Column(Order = 0)]
        public int SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string MasterID { get; set; }

        [StringLength(36)]
        public string A { get; set; }

        [StringLength(36)]
        public string B { get; set; }

        [StringLength(36)]
        public string C { get; set; }

        [StringLength(36)]
        public string D { get; set; }

        [StringLength(36)]
        public string E { get; set; }

        [Required]
        [StringLength(36)]
        public string F { get; set; }

        [StringLength(36)]
        public string G { get; set; }
    }
}
