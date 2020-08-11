using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DANGNHAP.Data;
using System.Data.SqlClient;

namespace DANGNHAP
{
    public partial class QLSanPham : Form
    {
        public QLSanPham()
        {
            InitializeComponent();
        }

        private void QLSanPham_Load(object sender, EventArgs e)
        {
            PhongVuDB context = new PhongVuDB();
            List<SanPham> listSanPhams = context.SanPhams.ToList();
            ShowList(listSanPhams);           
                   
        }

        public void ShowList(List<SanPham> list)
        {
            int i = 0;
            ltvSP.Items.Clear();
            foreach (SanPham p in list)
            {

                ltvSP.Items.Add(p.MaSanPham);
                ltvSP.Items[i].SubItems.Add(p.TenSanPham);
                ltvSP.Items[i].SubItems.Add(p.MoTa);
                ltvSP.Items[i].SubItems.Add(p.Gia.ToString());
                ltvSP.Items[i].SubItems.Add(p.Anh);
                ltvSP.Items[i].SubItems.Add(p.MaLoai);
                i++;
            }
        }

        private void ltvSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = ltvSP.SelectedItems[0];
            txtMaSP.Text = item.SubItems[0].Text;
            txtTenSP.Text = item.SubItems[1].Text;
            txtMoTa.Text = item.SubItems[2].Text;
            txtGiaBan.Text = item.SubItems[3].Text;
            cboLoaiSP.Text = item.SubItems[5].Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SanPham temp = new SanPham();
            temp.MaSanPham = txtMaSP.Text;
            temp.TenSanPham = txtTenSP.Text;
            temp.Gia = Convert.ToInt32(txtGiaBan.Text);
            temp.MaLoai = cboLoaiSP.Text;
            temp.MoTa = txtMoTa.Text;
            temp.Anh = "aaaa";
            InsertSanPham(temp);


        }

        public void InsertSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            context.SanPhams.Add(temp); // đưa đối tương temp vào DBSet san pham
            context.SaveChanges();
        }

        public void UpdateSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSanPham == temp.MaSanPham); //lay ra lai thong tin cũ
            if (item != null)
            {
                //câp nhật
                item.TenSanPham = temp.TenSanPham; //muon thay doi Name
                item.MoTa = temp.MoTa; //muôn update mota
                item.Gia = temp.Gia; //muon thay doi gia
                item.Anh = temp.Anh; //muôn update anh
                item.MaLoai = temp.MaLoai; //muon thay doi ma loai
                item.MoTa = temp.MoTa; //muôn update mota
                                     
                context.SaveChanges(); // Lưu thay đổi
                MessageBox.Show("Update thanh cong");
                
                List<SanPham> listSanPhams = context.SanPhams.ToList();
                ShowList(listSanPhams);

            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SanPham temp = new SanPham();
            temp.MaSanPham = txtMaSP.Text;
            temp.TenSanPham = txtTenSP.Text;
            temp.Gia = Convert.ToInt32(txtGiaBan.Text);
            temp.MaLoai = cboLoaiSP.Text;
            temp.MoTa = txtMoTa.Text;
            temp.Anh = "aaaa";
            UpdateSanPham(temp);           
        }

        public void DeleteSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSanPham ==
            temp.MaSanPham); //lay ra lai thong tin cũ
            if (item != null)
            {
                context.SanPhams.Remove(item);
                context.SaveChanges(); // Lưu thay đổi
            }
            MessageBox.Show("Xoa thanh cong");

            List<SanPham> listSanPhams = context.SanPhams.ToList();
            ShowList(listSanPhams);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SanPham temp = new SanPham();
            temp.MaSanPham = txtMaSP.Text;
            temp.TenSanPham = txtTenSP.Text;
            temp.Gia = Convert.ToInt32(txtGiaBan.Text);
            temp.MaLoai = cboLoaiSP.Text;
            temp.MoTa = txtMoTa.Text;
            temp.Anh = "aaaa";
            DeleteSanPham(temp);
        }
    }
}
