namespace DANGNHAP.Modell
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PhieuXuat
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPX { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGiaXuat { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual PhieuXuat PhieuXuat { get; set; }
    }
}
