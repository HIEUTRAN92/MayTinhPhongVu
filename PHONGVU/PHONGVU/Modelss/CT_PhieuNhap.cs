namespace DANGNHAP.Modelss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PhieuNhap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPN { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGiaNhap { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
