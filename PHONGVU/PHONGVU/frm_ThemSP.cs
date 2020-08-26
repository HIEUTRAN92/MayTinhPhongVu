using DANGNHAP.Modell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DANGNHAP
{
    public partial class frm_ThemSP : Form
    {
        public frm_ThemSP()
        {
            InitializeComponent();
        }
        public delegate void ThemSanPham(string ma, string ten, string mota, int gia, string hinh,int sl, string hsx, int ml);
        public ThemSanPham themSanPham;
        string hinh = "";
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã sản phẩm", "Thông Báo");
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
                else if (cboLoai.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm", "Thông Báo");
                }
                else if (cboHSX.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn hãng sản xuất", "Thông Báo");
                }
                else if (txtSoLuong.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng", "Thông Báo");
                }
                else if (hinh=="")
                {
                    MessageBox.Show("Bạn chưa chọn ảnh sản phẩm", "Thông Báo");
                }
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    int ma = Convert.ToInt32(txtMaSP.Text);
                    SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == ma); //kiem tra san pham da ton tai chua
                    if (item != null)
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông Báo");
                    }
                    else if (themSanPham != null)
                    {                       
                        List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
                        LoaiSP s = listLoaiSPs.FirstOrDefault(p => p.TenLoaiSP == cboLoai.Text);                        
                        themSanPham(txtMaSP.Text, txtTenSP.Text, txtMoTa.Text, Convert.ToInt32(txtGiaBan.Text), hinh, Convert.ToInt32(txtSoLuong.Text), cboHSX.Text, s.MaLoaiSP);

                        txtMaSP.Text = "";
                        txtTenSP.Text = "";
                        txtSoLuong.Text = "";
                        txtGiaBan.Text = "";
                        txtMoTa.Text = "";
                        cboHSX.Text = "DELL";
                        hinh = "";
                        Image img = Image.FromFile(@"C:\Users\DELL\Pictures\img.png");
                        picAnhSP.Image = img;
                        cboLoai.Text = "Laptop";
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
        }

        private void InsertSanPham(SanPham temp)
        {
            PhongVuDB context = new PhongVuDB();
            SanPham item = context.SanPhams.FirstOrDefault(p => p.MaSP == temp.MaSP); //kiem tra san pham da ton tai chua
            if (item != null)
            {
                MessageBox.Show("Sản phẩm đã tồn tại", "Thông Báo");
            }
            else
            {
                context.SanPhams.Add(temp); // đưa đối tương temp vào DBSet san pham
                context.SaveChanges();
                List<SanPham> listSanPhams = context.SanPhams.ToList();
                //ShowList(listSanPhams);
                MessageBox.Show("Thêm sản phẩm thành công", "Thông Báo");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //Tạo hộp thoại mở file
            OpenFileDialog dlg = new OpenFileDialog();
            //lọc hiện thị các loại file
            dlg.Filter = "PNG file|*.png|JPEG File|*.jpg|All Files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                hinh = dlg.FileName;
                Image img = Image.FromFile(dlg.FileName);
                picAnhSP.Image = img; //Lấy tên file cần mở               
            }
        }

        private void frm_ThemSP_Load(object sender, EventArgs e)
        {
            PhongVuDB context = new PhongVuDB();           
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            ShowComBoLoai();
            cboLoai.Text = "LAPTOP";
            cboHSX.Text = "DELL";
        }

        private void ShowComBoLoai()
        {
            cboLoai.Items.Clear();
            PhongVuDB context = new PhongVuDB();
            List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList();
            foreach (LoaiSP p in listLoaiSPs)
            {
                cboLoai.Items.Add(p.TenLoaiSP.ToString());
            }
        }
    }
}
