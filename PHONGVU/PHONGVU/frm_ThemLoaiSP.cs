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
    public partial class frm_ThemLoaiSP : Form
    {
        public frm_ThemLoaiSP()
        {
            InitializeComponent();
        }
        public delegate void ThemLoaiSanPham(int ma, string ten);
        public ThemLoaiSanPham themLoaiSanPham;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaLoaiSP.Text == "")
                {
                    MessageBox.Show("Bạn phải nhập mã loại sản phẩm!", "Thông báo");
                }
                else if (txtTenLoaiSP.Text == "")
                {
                    MessageBox.Show("Bạn phải nhập tên loại sản phẩm!", "Thông báo");
                }
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    int ma = Convert.ToInt32(txtMaLoaiSP.Text);
                    string ten = txtTenLoaiSP.Text;
                    LoaiSP item = context.LoaiSPs.FirstOrDefault(p => p.MaLoaiSP == ma); //kiem tra san pham da ton tai chua
                    if (item != null)
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông Báo");
                    }
                    else if (themLoaiSanPham != null)
                    {
                        themLoaiSanPham(ma, ten);
                        txtMaLoaiSP.Text = "";
                        txtTenLoaiSP.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
            
        }
    }
}
