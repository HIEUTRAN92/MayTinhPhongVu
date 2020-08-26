using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DANGNHAP.Modell;
using DANGNHAP.Properties;
using System.Data.SqlClient;

namespace DANGNHAP
{
    public partial class frm_QLSP : Form
    {
        public frm_QLSP()
        {
            InitializeComponent();
        }
        bool isUpdate = false;
        string hinh = "";
        private void QLSanPham_Load(object sender, EventArgs e)
        {
            txtMaLoai.Enabled = false;
            txtTenLoai.Enabled = false;
            btnXoaLoai.Enabled = false;
            btnCapNhatLoai.Enabled = false;
            if (!isUpdate)
            {
                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtMoTa.Enabled = false;
                txtGiaBan.Enabled = false;
                txtSoLuong.Enabled = false;
                cboLoaiSP.Enabled = false;
                cboHangSX.Enabled = false;

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else
            {
                txtMaSP.Enabled = true;
                txtTenSP.Enabled = true;
                txtMoTa.Enabled = true;
                txtGiaBan.Enabled = true;
                txtSoLuong.Enabled = true;
                cboLoaiSP.Enabled = true;
                cboHangSX.Enabled = true;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            
            PhongVuDB context = new PhongVuDB();
            List<SanPham> listSanPhams = context.SanPhams.ToList();
            ShowList(listSanPhams);            
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            ShowComBoLoai();
            ShowListLoai(listLoaiSPs);                   
        }

        public void ShowComBoLoai()
        {
            cboLoaiSP.Items.Clear();
            PhongVuDB context = new PhongVuDB();           
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            foreach (LoaiSP p in listLoaiSPs)
            {
                cboLoaiSP.Items.Add(p.TenLoaiSP.ToString());
            }            
        }

        public void ShowList(List<SanPham> list)
        {
            int i = 0;
            ltvSP.Items.Clear();
            foreach (SanPham p in list)
            {

                ltvSP.Items.Add(p.MaSP.ToString());
                ltvSP.Items[i].SubItems.Add(p.TenSP);
                ltvSP.Items[i].SubItems.Add(p.Mota);
                ltvSP.Items[i].SubItems.Add(p.Gia.ToString());
                ltvSP.Items[i].SubItems.Add(p.img);
                ltvSP.Items[i].SubItems.Add(p.MaLoaiSP.ToString());
                ltvSP.Items[i].SubItems.Add(p.SoLuong.ToString());
                ltvSP.Items[i].SubItems.Add(p.HangSX.ToString());
                i++;
            }
        }

        public void ShowListLoai(List<LoaiSP> list)
        {
            int i = 0;
            ltvLoaiSP.Items.Clear();
            foreach (LoaiSP p in list)
            {
                ltvLoaiSP.Items.Add(p.MaLoaiSP.ToString());
                ltvLoaiSP.Items[i].SubItems.Add(p.TenLoaiSP);                
                i++;
            }
        }

        private void ltvSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            isUpdate = true;
            if (!isUpdate)
            {
                txtMaSP.Enabled = false;
                txtTenSP.Enabled = false;
                txtMoTa.Enabled = false;
                txtGiaBan.Enabled = false;
                txtSoLuong.Enabled = false;
                cboLoaiSP.Enabled = false;
                cboHangSX.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else
            {
                txtMaSP.Enabled = true;
                txtTenSP.Enabled = true;
                txtMoTa.Enabled = true;
                txtGiaBan.Enabled = true;
                txtSoLuong.Enabled = true;
                cboLoaiSP.Enabled = true;
                cboHangSX.Enabled = true;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
            ListView.SelectedListViewItemCollection list = this.ltvSP.SelectedItems;
            foreach (ListViewItem item in ltvSP.SelectedItems)
            {
                txtMaSP.Text = item.SubItems[0].Text;
                txtTenSP.Text = item.SubItems[1].Text;
                txtMoTa.Text = item.SubItems[2].Text;
                txtGiaBan.Text = item.SubItems[3].Text;
                txtSoLuong.Text = item.SubItems[6].Text;
                cboHangSX.Text = item.SubItems[7].Text;
                hinh = item.SubItems[4].Text;
                Image img = hinh == ""?Image.FromFile(@"C:\Users\DELL\Pictures\logoPV.png") : Image.FromFile(hinh);
                picSP.Image = img;
                PhongVuDB context = new PhongVuDB();
                List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();                
                int a = Convert.ToInt32(item.SubItems[5].Text);
                LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.MaLoaiSP == a);
                cboLoaiSP.Text = s.TenLoaiSP;
            }
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frm_ThemSP frm = new frm_ThemSP();
            frm.themSanPham = new frm_ThemSP.ThemSanPham(themSanPham);
            frm.ShowDialog();           

        }

        public void themSanPham(string ma, string ten, string mota, int gia, string hinh, int sl, string hsx, int ml)
        {
            SanPham temp = new SanPham();
            temp.MaSP = Convert.ToInt32(ma);
            temp.TenSP = ten;
            temp.Mota = mota;
            temp.Gia = gia;
            temp.img = hinh;
            temp.SoLuong = sl;
            temp.HangSX = hsx;
            temp.MaLoaiSP = ml;
            InsertSanPham(temp);
        }

        public void InsertSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            context.SanPhams.Add(temp); // đưa đối tương temp vào DBSet san pham
            context.SaveChanges();
            List<SanPham> listSanPhams = context.SanPhams.ToList();
            ShowList(listSanPhams);
            MessageBox.Show("Thêm sản phẩm thành công", "Thông Báo");                      
        }

        public void UpdateSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == temp.MaSP); //lay ra lai thong tin cũ
            if (item != null)
            {
                //câp nhật
                item.TenSP = temp.TenSP; //muon thay doi Name
                item.Mota = temp.Mota; //muôn update mota
                item.Gia = temp.Gia; //muon thay doi gia
                item.img = temp.img; //muôn update anh
                item.MaLoaiSP = temp.MaLoaiSP; //muon thay doi ma loai 
                item.SoLuong = temp.SoLuong;
                item.HangSX = temp.HangSX;
                context.SaveChanges(); // Lưu thay đổi                
                List<SanPham> listSanPhams = context.SanPhams.ToList();
                ShowList(listSanPhams);
                isUpdate = false;
                if (!isUpdate)
                {
                    txtMaSP.Text = "";
                    txtTenSP.Text = "";
                    txtMoTa.Text = "";
                    txtGiaBan.Text = "";
                    txtSoLuong.Text = "";
                    cboLoaiSP.Text = "LapTop";
                    cboHangSX.Text = "DELL";                    

                    txtMaSP.Enabled = false;
                    txtTenSP.Enabled = false;
                    txtMoTa.Enabled = false;
                    txtGiaBan.Enabled = false;
                    txtSoLuong.Enabled = false;
                    cboLoaiSP.Enabled = false;
                    cboHangSX.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                }
                else
                {
                    txtMaSP.Enabled = true;
                    txtTenSP.Enabled = true;
                    txtMoTa.Enabled = true;
                    txtGiaBan.Enabled = true;
                    txtSoLuong.Enabled = true;
                    cboLoaiSP.Enabled = true;
                    cboHangSX.Enabled = true;

                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnThem.Enabled = false;
                }

                MessageBox.Show("Cập nhật sản phẩm thành công", "Thông Báo");
            }
            else
            {
                MessageBox.Show("Sản phẩm không tồn tại", "Thông Báo");
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông Báo");
                }
                else if (txtTenSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông Báo");
                }
                else if (txtGiaBan.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập giá bán", "Thông Báo");
                }
                else if (txtMoTa.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mô tả", "Thông Báo");
                }
                else if (cboLoaiSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm", "Thông Báo");
                }
                else if (cboHangSX.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn hãng sản xuất", "Thông Báo");
                }
                else if (txtSoLuong.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng", "Thông Báo");
                }
                else if (picSP.Image == null)
                {
                    MessageBox.Show("Bạn chưa chọn ảnh sản phẩm", "Thông Báo");
                }
                else
                {
                    SanPham temp = new SanPham();
                    temp.MaSP = Convert.ToInt32(txtMaSP.Text);
                    temp.TenSP = txtTenSP.Text;
                    temp.Gia = Convert.ToInt32(txtGiaBan.Text);
                    PhongVuDB context = new PhongVuDB();
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.TenLoaiSP == cboLoaiSP.Text);
                    temp.MaLoaiSP = s.MaLoaiSP;
                    temp.Mota = txtMoTa.Text;
                    temp.HangSX = cboHangSX.Text;
                    temp.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                    temp.img = hinh;
                    UpdateSanPham(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông Báo");
            }
                     
        }

        public void DeleteSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == temp.MaSP); //lay ra lai thong tin cũ
            if (item != null)
            {
                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa sản phẩm này!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                
                if(result == DialogResult.Yes)
                {
                    context.SanPhams.Remove(item);
                    context.SaveChanges(); // Lưu thay đổi
                    List<SanPham> listSanPhams = context.SanPhams.ToList();
                    ShowList(listSanPhams);
                    isUpdate = false;
                    if (!isUpdate)
                    {
                        txtMaSP.Text = "";
                        txtTenSP.Text = "";
                        txtMoTa.Text = "";
                        txtGiaBan.Text = "";
                        txtSoLuong.Text = "";
                        cboLoaiSP.Text = "LapTop";
                        cboHangSX.Text = "DELL";

                        txtMaSP.Enabled = false;
                        txtTenSP.Enabled = false;
                        txtMoTa.Enabled = false;
                        txtGiaBan.Enabled = false;
                        txtSoLuong.Enabled = false;
                        cboLoaiSP.Enabled = false;
                        cboHangSX.Enabled = false;
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        btnThem.Enabled = true;
                    }
                    else
                    {
                        txtMaSP.Enabled = true;
                        txtTenSP.Enabled = true;
                        txtMoTa.Enabled = true;
                        txtGiaBan.Enabled = true;
                        txtSoLuong.Enabled = true;
                        cboLoaiSP.Enabled = true;
                        cboHangSX.Enabled = true;

                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnThem.Enabled = false;
                    }
                    MessageBox.Show("Xóa sản phẩm thành công", "Thông Báo");
                }

            }
            else
            {
                MessageBox.Show("Sản phẩm không tồn tại", "Thông Báo");
            }
            

           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông Báo");
                }
                else
                {
                    SanPham temp = new SanPham();
                    temp.MaSP = Convert.ToInt32(txtMaSP.Text);
                    temp.TenSP = txtTenSP.Text;
                    temp.Gia = Convert.ToInt32(txtGiaBan.Text);
                    PhongVuDB context = new PhongVuDB();
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.TenLoaiSP == cboLoaiSP.Text);
                    temp.MaLoaiSP = s.MaLoaiSP;                    
                    temp.Mota = txtMoTa.Text;
                    temp.img = "aaaa";
                    DeleteSanPham(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            frm_ThemLoaiSP frm = new frm_ThemLoaiSP();
            frm.themLoaiSanPham = new frm_ThemLoaiSP.ThemLoaiSanPham(themLoaiSanPham);
            frm.ShowDialog();                  
        }

        public void themLoaiSanPham(int ma , string ten)
        {
            PhongVuDB context = new PhongVuDB();
            LoaiSP item = new LoaiSP();
            item.MaLoaiSP = ma;
            item.TenLoaiSP = ten;
            context.LoaiSPs.Add(item); // đưa đối tương temp vào DBSet san pham
            context.SaveChanges();
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            ShowListLoai(listLoaiSPs);
            ShowComBoLoai();
            MessageBox.Show("Thêm loại sản phẩm thành công", "Thông Báo");
        }

        public void InsertLoaiSP(LoaiSP temp)
        {
            PhongVuDB context = new PhongVuDB();            
            LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == temp.MaLoaiSP); //lay ra lai thong tin cũ
            if (item != null)
            {                
                MessageBox.Show("Loại sản phẩm này đã tồn tại", "Thông Báo");
            }
            else
            {
                context.LoaiSPs.Add(temp); // đưa đối tương temp vào DBSet san pham
                context.SaveChanges();                            
                List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                ShowListLoai(listLoaiSPs);
                ShowComBoLoai();
                MessageBox.Show("Thêm loại sản phẩm thành công","Thông Báo");
            }
            
        }

        private void btnCapNhatLoai_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm nào", "Thông Báo");

                }
                else if (txtTenLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa cập nhật tên loại sản phẩm", "Thông Báo");
                }
                else
                {
                    LoaiSP temp = new LoaiSP();
                    temp.MaLoaiSP = Convert.ToInt32(txtMaLoai.Text);
                    temp.TenLoaiSP = txtTenLoai.Text;
                    UpdateLoaiSP(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông Báo");
            }            
        }

        public void UpdateLoaiSP(LoaiSP temp)
        {
            PhongVuDB context = new PhongVuDB();                       
            LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == temp.MaLoaiSP); //lay ra lai thong tin cũ
            if (item != null)
            {                
                item.TenLoaiSP = temp.TenLoaiSP; //Cap nhat lai ten loai
                context.SaveChanges(); // Lưu thay đổi
                txtMaLoai.Enabled = false;
                txtTenLoai.Enabled = false;
                btnXoaLoai.Enabled = false;
                btnCapNhatLoai.Enabled = false;
                btnThemLoai.Enabled = true;
                txtMaLoai.Text = "";
                txtTenLoai.Text = "";
                MessageBox.Show("Cập nhật loại sản phẩm thành công", "Thông Báo");
                List<LoaiSP> listLoaiSP = context.LoaiSPs.ToList();
                ShowListLoai(listLoaiSP);
                ShowComBoLoai();
            }
            else
            {
                MessageBox.Show("Không tìm thấy loại sản phẩm bạn muốn cập nhật", "Thông Báo");
            }
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm nào", "Thông Báo");

                }
                else
                {
                    LoaiSP temp = new LoaiSP();
                    temp.MaLoaiSP = Convert.ToInt32(txtMaLoai.Text);
                    temp.TenLoaiSP = txtTenLoai.Text;
                    DeleteLoaiSP(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
            
        }

        public void DeleteLoaiSP(LoaiSP temp)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa loại sản phẩm này!","Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                PhongVuDB context = new PhongVuDB();
                LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == temp.MaLoaiSP); //lay ra lai thong tin cũ
                if (item != null)
                {
                    context.LoaiSPs.Remove(item);
                    context.SaveChanges(); // Lưu thay đổi                    
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    ShowListLoai(listLoaiSPs);
                    ShowComBoLoai();
                    txtMaLoai.Enabled = false;
                    txtTenLoai.Enabled = false;
                    btnXoaLoai.Enabled = false;
                    btnCapNhatLoai.Enabled = false;
                    btnThemLoai.Enabled = true;
                    txtMaLoai.Text = "";
                    txtTenLoai.Text = "";
                    MessageBox.Show("Xóa loại sản phẩm thành công", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Loại sản phẩm này không tồn tại", "Thông Báo");
                }
                
            }            
        }

        private void picSP_Click(object sender, EventArgs e)
        {
                                   
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Tạo hộp thoại mở file
            OpenFileDialog dlg = new OpenFileDialog();
            //lọc hiện thị các loại file
            dlg.Filter = "PNG file|*.png|JPEG File|*.jpg|All Files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                hinh = dlg.FileName;                
                Image img = Image.FromFile(dlg.FileName);
                picSP.Image = img; //Lấy tên file cần mở               
            }
        }

        private void ltvLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaLoai.Enabled = true;
            txtTenLoai.Enabled = true;
            btnCapNhatLoai.Enabled = true;
            btnXoaLoai.Enabled = true;
            btnThemLoai.Enabled = false;
            ListView.SelectedListViewItemCollection list = this.ltvLoaiSP.SelectedItems;
            foreach (ListViewItem item in ltvLoaiSP.SelectedItems)
            {
                txtMaLoai.Text = item.SubItems[0].Text;
                txtTenLoai.Text = item.SubItems[1].Text;              
            }
        }
    }
}
