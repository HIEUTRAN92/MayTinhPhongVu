using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DANGNHAP.Modelss;
using DANGNHAP.Properties;
using System.Data.SqlClient;

namespace DANGNHAP
{
    public partial class frmQLSP : Form
    {
        public frmQLSP()
        {
            InitializeComponent();
        }

        string hinh = "";
        private void QLSanPham_Load(object sender, EventArgs e)
        {
            PhongVuContextDB context = new PhongVuContextDB();
            List<SanPham> listSanPhams = context.SanPhams.ToList();
            ShowList(listSanPhams);            
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            ShowComBoLoai();
            ShowListLoai(listLoaiSPs);                   
        }

        public void ShowComBoLoai()
        {
            cboLoaiSP.Items.Clear();
            PhongVuContextDB context = new PhongVuContextDB();           
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
            ListView.SelectedListViewItemCollection list = this.ltvSP.SelectedItems;
            foreach (ListViewItem item in ltvSP.SelectedItems)
            {
                txtMaSP.Text = item.SubItems[0].Text;
                txtTenSP.Text = item.SubItems[1].Text;
                txtMoTa.Text = item.SubItems[2].Text;
                txtGiaBan.Text = item.SubItems[3].Text;
                hinh = item.SubItems[4].Text;
                Image img = hinh == ""?Image.FromFile(@"C:\Users\DELL\Pictures\logoPV.png") : Image.FromFile(hinh);
                picSP.Image = img;
                PhongVuContextDB context = new PhongVuContextDB();
                List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();                
                int a = Convert.ToInt32(item.SubItems[5].Text);
                LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.MaLoaiSP == a);
                cboLoaiSP.Text = s.TenLoaiSP;
            }
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã sản phẩm", "Thông Báo");
                }
                else if(txtTenSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông Báo");
                }
                else if(txtGiaBan.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập giá bán", "Thông Báo");
                }else if(txtMoTa.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mô tả", "Thông Báo");
                }else if(cboLoaiSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm", "Thông Báo");
                }else if(picSP.Image == null)
                {
                    MessageBox.Show("Bạn chưa chọn ảnh sản phẩm", "Thông Báo");
                }else
                {
                    SanPham temp = new SanPham();
                    temp.MaSP = Convert.ToInt32(txtMaSP.Text);
                    temp.TenSP = txtTenSP.Text;
                    temp.Gia = Convert.ToInt32(txtGiaBan.Text);
                    PhongVuContextDB context = new PhongVuContextDB();
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.TenLoaiSP == cboLoaiSP.Text);
                    temp.MaLoaiSP = s.MaLoaiSP;
                    temp.Mota = txtMoTa.Text;
                    temp.img = hinh;
                    InsertSanPham(temp);                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông Báo");
            }
                       
        }

        public void InsertSanPham(SanPham temp)
        {
            PhongVuContextDB context = new PhongVuContextDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == temp.MaSP); //kiem tra san pham da ton tai chua
            if(item != null)
            {
                MessageBox.Show("Sản phẩm đã tồn tại", "Thông Báo");
            }
            else
            {
                context.SanPhams.Add(temp); // đưa đối tương temp vào DBSet san pham
                context.SaveChanges();
                List<SanPham> listSanPhams = context.SanPhams.ToList();
                ShowList(listSanPhams);
                MessageBox.Show("Thêm sản phẩm thành công", "Thông Báo");                
            }            
        }

        public void UpdateSanPham(SanPham temp)
        {
            PhongVuContextDB context = new PhongVuContextDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == temp.MaSP); //lay ra lai thong tin cũ
            if (item != null)
            {
                //câp nhật
                item.TenSP = temp.TenSP; //muon thay doi Name
                item.Mota = temp.Mota; //muôn update mota
                item.Gia = temp.Gia; //muon thay doi gia
                item.img = temp.img; //muôn update anh
                item.MaLoaiSP = temp.MaLoaiSP; //muon thay doi ma loai                          
                context.SaveChanges(); // Lưu thay đổi
                MessageBox.Show("Cập nhật sản phẩm thành công","Thông Báo");
                List<SanPham> listSanPhams = context.SanPhams.ToList();
                ShowList(listSanPhams);
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
                    PhongVuContextDB context = new PhongVuContextDB();
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.TenLoaiSP == cboLoaiSP.Text);
                    temp.MaLoaiSP = s.MaLoaiSP;
                    temp.Mota = txtMoTa.Text;
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
            PhongVuContextDB context = new PhongVuContextDB();
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
                    PhongVuContextDB context = new PhongVuContextDB();
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
            try
            {
                if (txtMaLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập ma loại sản phẩm", "Thông Báo");

                }
                else if (txtTenLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên loại sản phẩm", "Thông Báo");
                }
                else
                {
                    LoaiSP temp = new LoaiSP();
                    temp.MaLoaiSP = Convert.ToInt32(txtMaLoai.Text);
                    temp.TenLoaiSP = txtTenLoai.Text;
                    InsertLoaiSP(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }            

        }

        public void InsertLoaiSP(LoaiSP temp)
        {
            PhongVuContextDB context = new PhongVuContextDB();            
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
            PhongVuContextDB context = new PhongVuContextDB();                       
            LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == temp.MaLoaiSP); //lay ra lai thong tin cũ
            if (item != null)
            {                
                item.TenLoaiSP = temp.TenLoaiSP; //Cap nhat lai ten loai
                context.SaveChanges(); // Lưu thay đổi
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
                PhongVuContextDB context = new PhongVuContextDB();
                LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == temp.MaLoaiSP); //lay ra lai thong tin cũ
                if (item != null)
                {
                    context.LoaiSPs.Remove(item);
                    context.SaveChanges(); // Lưu thay đổi                    
                    List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                    ShowListLoai(listLoaiSPs);
                    ShowComBoLoai();
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
            ListView.SelectedListViewItemCollection list = this.ltvLoaiSP.SelectedItems;
            foreach (ListViewItem item in ltvLoaiSP.SelectedItems)
            {
                txtMaLoai.Text = item.SubItems[0].Text;
                txtTenLoai.Text = item.SubItems[1].Text;              
            }
        }
    }
}
