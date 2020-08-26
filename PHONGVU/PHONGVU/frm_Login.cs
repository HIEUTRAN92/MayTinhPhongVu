using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DANGNHAP.Modell;

namespace DANGNHAP
{
    public partial class frmLogin : Form
    {
        public delegate void nguoiDungLogin(NguoiDung nd);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên tài khoản hoặc mật khẩu!", "Thông Báo");
                }
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    List<NguoiDung> listNguoiDungs = context.NguoiDungs.ToList();
                    NguoiDung s = listNguoiDungs.FirstOrDefault(p => p.TenDN == txtTaiKhoan.Text && p.MatKhau == txtMatKhau.Text);
                    if (s != null)
                    {                        
                        frm_TrangChu frm = new frm_TrangChu();
                        nguoiDungLogin nd = new nguoiDungLogin(frm.kiemTraND);
                        nd(s);
                        this.Visible = false;
                        frm.ShowDialog();
                        this.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại", "Thông Báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }                      
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
