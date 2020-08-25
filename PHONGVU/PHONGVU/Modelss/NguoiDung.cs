namespace DANGNHAP.Modelss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [Key]
        [StringLength(20)]
        public string TenDN { get; set; }

        [StringLength(20)]
        public string MatKhau { get; set; }

        [StringLength(10)]
        public string LoaiTK { get; set; }
    }
}
