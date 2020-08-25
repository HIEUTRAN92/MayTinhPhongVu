namespace DANGNHAP.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [StringLength(50)]
        public string MaSanPham { get; set; }

        [StringLength(50)]
        public string TenSanPham { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [Column(TypeName = "money")]
        public decimal? Gia { get; set; }

        [StringLength(100)]
        public string Anh { get; set; }

        [StringLength(50)]
        public string MaLoai { get; set; }
    }
}
