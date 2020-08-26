<<<<<<< HEAD
namespace DANGNHAP.Modell
=======
namespace DANGNHAP.Modelss
>>>>>>> origin/Kien_Repost
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PhongVuDB : DbContext
    {
<<<<<<< HEAD
<<<<<<<< HEAD:PHONGVU/PHONGVU/Modell/PhongVuDB.cs
        public PhongVuDB()
            : base("name=PhongVuDB1")
========
        public PhongVuContextDB()
            : base("name=PhongVuContextDB1")
>>>>>>>> origin/Kien_Repost:PHONGVU/PHONGVU/Modelss/PhongVuContextDB.cs
=======
        public PhongVuDB()
            : base("name=PhongVuDBContext")
>>>>>>> origin/Kien_Repost
        {
        }

        public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DbSet<CT_PhieuNhap> CT_PhieuNhap { get; set; }
        public virtual DbSet<CT_PhieuXuat> CT_PhieuXuat { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuXuats)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuNhap>()
                .HasMany(e => e.CT_PhieuNhap)
                .WithRequired(e => e.PhieuNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuXuat>()
                .HasMany(e => e.CT_PhieuXuat)
                .WithRequired(e => e.PhieuXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CT_PhieuNhap)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CT_PhieuXuat)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
