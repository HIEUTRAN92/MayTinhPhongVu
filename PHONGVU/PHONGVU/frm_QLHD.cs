using DANGNHAP.Modell;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DANGNHAP
{
    public partial class frm_QLHD : Form
    {
        public frm_QLHD()
        {
            InitializeComponent();
        }

        private void frm_QLHD_Load(object sender, EventArgs e)
        {
            PhongVuDB context = new PhongVuDB();
            List<HoaDon> listHoaDons = context.HoaDons.ToList();
            binDataDSHD(listHoaDons);
            SetGridViewStyle(dgvDSHD);
            SetGridViewStyle(dgvCTHD);
            txtNhanVien.Enabled = false;
            txtHoTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtSoHD.Enabled = false;
            cboGioiTinh.Enabled = false;
            dtpNgayLap.Enabled = false;
            dtpNamSinh.Enabled = false;
            txtTongTien.Enabled = false;
        }

        private void binDataDSHD(List<HoaDon> list)
        {
            PhongVuDB context = new PhongVuDB();
            List<NhanVien> listNhanViens = context.NhanViens.ToList();
            List<KhachHang> listKhachHangs = context.KhachHangs.ToList();
            dgvDSHD.Rows.Clear();
            foreach (var item in list)
            {
                int index = dgvDSHD.Rows.Add();
                dgvDSHD.Rows[index].Cells[0].Value = index+1;
                dgvDSHD.Rows[index].Cells[1].Value = item.SoHD;
                NhanVien n = listNhanViens.FirstOrDefault(p => p.MaNV == item.MaNV);
                dgvDSHD.Rows[index].Cells[2].Value = n.Hoten;
                KhachHang k = listKhachHangs.FirstOrDefault(p => p.MaKH == item.MaKH);
                dgvDSHD.Rows[index].Cells[3].Value = k.HotenKH;
                dgvDSHD.Rows[index].Cells[4].Value = item.NgayLap;
            }
        }

        private void dgvDSHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PhongVuDB context = new PhongVuDB();
            List<HoaDon> listHoaDons = context.HoaDons.ToList();
            List<NhanVien> listNhanViens = context.NhanViens.ToList();
            List<CT_HoaDon> listCTHDs = context.CT_HoaDon.ToList();
            List<KhachHang> listKhachHangs = context.KhachHangs.ToList();
            List<SanPham> listSanPhams = context.SanPhams.ToList();

            int index = dgvDSHD.CurrentRow.Index;           
            int key = Convert.ToInt32(dgvDSHD.Rows[index].Cells[1].Value.ToString());            
            HoaDon hd = listHoaDons.FirstOrDefault(p => p.SoHD == key);
            txtSoHD.Text = hd.SoHD.ToString();
            dtpNgayLap.Text = hd.NgayLap.ToString();
            NhanVien nv = listNhanViens.FirstOrDefault(p => p.MaNV == hd.MaNV);
            txtNhanVien.Text = nv.Hoten.ToString();

            KhachHang kh = listKhachHangs.FirstOrDefault(p => p.MaKH == hd.MaKH);
            txtHoTenKH.Text = kh.HotenKH.ToString();
            txtEmail.Text = kh.Email.ToString();
            txtDiaChi.Text = kh.DiaChi.ToString();
            txtSDT.Text = kh.SoDienThoai.ToString();
            dtpNamSinh.Text = kh.NamSinh.ToString();
            cboGioiTinh.Text = kh.GioiTinh.ToString();

            List<CT_HoaDon> listCTHD2s = listCTHDs.FindAll(p => p.SoHD == key);
            dgvCTHD.Rows.Clear();
            double tong = 0;
            foreach(var item in listCTHD2s)
            {
                int i = dgvCTHD.Rows.Add();
                dgvCTHD.Rows[i].Cells[0].Value = i + 1;
                int ma = item.MaSP;
                SanPham sp = listSanPhams.FirstOrDefault(p => p.MaSP == ma);
                dgvCTHD.Rows[i].Cells[1].Value = sp.TenSP;
                dgvCTHD.Rows[i].Cells[2].Value = sp.Gia;
                dgvCTHD.Rows[i].Cells[3].Value = item.SoLuong;
                dgvCTHD.Rows[i].Cells[4].Value = sp.Gia*item.SoLuong;
                tong += (double)(sp.Gia * item.SoLuong);
            }
            txtTongTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tong) + " (VND)";//Convert về kiểu tiền tệ
        }

        public void SetGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgview.BackgroundColor = Color.White;
            dgview.EnableHeadersVisualStyles = false;
            dgview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgview.AllowUserToDeleteRows = false;
            dgview.AllowUserToAddRows = false;
            dgview.AllowUserToOrderColumns = true;
            dgview.MultiSelect = false;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            PhongVuDB context = new PhongVuDB();
            List<HoaDon> listHoaDons = context.HoaDons.ToList();
            if (txtTimKiem.Text == "")
            {               
                binDataDSHD(listHoaDons);
            }
            else
            {
                int key = Convert.ToInt32(txtTimKiem.Text);
                List<HoaDon> listTimKiems = listHoaDons.FindAll(p => p.SoHD == key);
                binDataDSHD(listTimKiems);
            }           
            
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
           // frm_ThemHoaDon frm = new frm_ThemHoaDon();
           // this.Visible = false;
           //// frm.ShowDialog();
           // this.Visible = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
