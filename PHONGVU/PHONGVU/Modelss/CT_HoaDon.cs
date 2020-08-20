namespace DANGNHAP.Modelss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HoaDon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? SoLuong { get; set; }

        public int MaSP { get; set; }

        public int SoHD { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
