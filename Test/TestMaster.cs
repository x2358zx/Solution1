namespace Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestMaster")]
    public partial class TestMaster
    {
        [Key]
        [Column(Order = 0)]
        public int SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string NO { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public DateTime? Birthday { get; set; }

        public decimal? Age { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
