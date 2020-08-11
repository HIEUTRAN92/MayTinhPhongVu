namespace DANGNHAP.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiSanPham")]
    public partial class LoaiSanPham
    {
        [Key]
        [StringLength(50)]
        public string MaLoai { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }
    }
}
