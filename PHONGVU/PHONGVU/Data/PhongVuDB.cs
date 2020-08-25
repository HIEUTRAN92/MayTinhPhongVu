namespace DANGNHAP.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PhongVuDB : DbContext
    {
        public PhongVuDB()
            : base("name=PhongVuDB")
        {
        }

        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiSanPham>()
                .Property(e => e.MaLoai)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiSanPham>()
                .Property(e => e.TenLoai)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaLoai)
                .IsUnicode(false);
        }
    }
}
