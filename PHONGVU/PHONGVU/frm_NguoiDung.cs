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
    public partial class frm_NguoiDung : Form
    {
        public frm_NguoiDung()
        {
            InitializeComponent();
        }

        private void frm_NguoiDung_Load(object sender, EventArgs e)
        {
            PhongVuDB context = new PhongVuDB();
            List<NguoiDung> listNguoiDungs = context.NguoiDungs.ToList();
            HienThiDS(listNguoiDungs);
            SetGridViewStyle(dgvDSND);
            cboLoaiTK.Items[0].ToString();
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void HienThiDS(List<NguoiDung> listNguoiDungs)
        {
            dgvDSND.Rows.Clear();
           foreach(var item in listNguoiDungs)
            {
                int index = dgvDSND.Rows.Add();
                dgvDSND.Rows[index].Cells[0].Value = index + 1;
                dgvDSND.Rows[index].Cells[1].Value = item.TenDN;
                dgvDSND.Rows[index].Cells[2].Value = item.MatKhau;
                dgvDSND.Rows[index].Cells[3].Value = item.LoaiTK;
            }
        }

        private void dgvDSND_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvDSND.CurrentRow.Index;
            txtTaiKhoan.Text = dgvDSND.Rows[index].Cells[1].Value.ToString();
            txtMatKhau.Text = dgvDSND.Rows[index].Cells[2].Value.ToString();
            cboLoaiTK.Text = dgvDSND.Rows[index].Cells[3].Value.ToString();
            cboLoaiTK.Items[0].ToString();
            btnCapNhat.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
        }

        public void SetGridViewStyle(DataGridView dgview)// Hàm format DataGridView có style hướng dẫn
        {
            dgview.BorderStyle = BorderStyle.None;
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

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtTaiKhoan.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn tài khoản nào", "Thông báo");
                }else if(txtMatKhau.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu", "Thông báo");
                }else if(cboLoaiTK.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại tài khoản", "Thông báo");
                }
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    NguoiDung nd = context.NguoiDungs.FirstOrDefault(p=>p.TenDN==txtTaiKhoan.Text);
                    if(nd != null)
                    {
                        nd.MatKhau = txtMatKhau.Text;
                        nd.LoaiTK = cboLoaiTK.Text;
                        context.SaveChanges();
                        List<NguoiDung> listNguoiDungs = context.NguoiDungs.ToList();
                        HienThiDS(listNguoiDungs);
                        MessageBox.Show("Đã cập nhật", "Thông Báo");
                        cboLoaiTK.Items[0].ToString();
                        btnCapNhat.Enabled = false;
                        btnXoa.Enabled = false;
                        btnThem.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản không tồn tại", "Thông Báo");
                    }                                       
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên tài khoản", "Thông báo");
                }
                else if (txtMatKhau.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu", "Thông báo");
                }
                else if (cboLoaiTK.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn loại tài khoản", "Thông báo");
                }
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    NguoiDung nd = context.NguoiDungs.FirstOrDefault(p => p.TenDN == txtTaiKhoan.Text);
                    if (nd != null)
                    {
                        MessageBox.Show("Tài khoản đã tồn tại", "Thông Báo");                        
                    }
                    else
                    {
                        NguoiDung item = new NguoiDung();
                        item.TenDN = txtTaiKhoan.Text;
                        item.MatKhau = txtMatKhau.Text;
                        item.LoaiTK = cboLoaiTK.Text;
                        context.NguoiDungs.Add(item);
                        context.SaveChanges();
                        List<NguoiDung> listNguoiDungs = context.NguoiDungs.ToList();
                        HienThiDS(listNguoiDungs);
                        MessageBox.Show("Thêm tài khoản thành công", "Thông Báo");
                        txtMatKhau.Text = "";
                        txtTaiKhoan.Text = "";
                        cboLoaiTK.Items[0].ToString();
                        btnCapNhat.Enabled = false;
                        btnXoa.Enabled = false;
                        btnThem.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "")
                {
                    MessageBox.Show("Bạn phải chọn một tài khoản", "Thông báo");
                }                
                else
                {
                    PhongVuDB context = new PhongVuDB();
                    NguoiDung nd = context.NguoiDungs.FirstOrDefault(p => p.TenDN == txtTaiKhoan.Text);
                    if (nd != null)
                    {
                        DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa tài khoản này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK) 
                        {
                            context.NguoiDungs.Remove(nd);
                            context.SaveChanges();
                            List<NguoiDung> listNguoiDungs = context.NguoiDungs.ToList();
                            HienThiDS(listNguoiDungs);
                            MessageBox.Show("Đã xóa", "Thông Báo");

                            txtMatKhau.Text = "";
                            txtTaiKhoan.Text = "";
                            cboLoaiTK.Items[0].ToString();
                            btnCapNhat.Enabled = false;
                            btnXoa.Enabled = false;
                            btnThem.Enabled = true;
                        }                                                     
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản không tồn tại", "Thông Báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo");
            }
        }
    }
}
