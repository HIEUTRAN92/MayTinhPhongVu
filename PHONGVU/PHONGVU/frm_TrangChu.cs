using DANGNHAP.Modelss;
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
    public partial class frm_TrangChu : Form
    {
        public frm_TrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {

        }

        public void kiemTraND(NguoiDung nd)
        {
            if (nd.LoaiTK == "2")
            {
                btnQLNX.Enabled = false;
                btnQLHT.Enabled = false;
                btnQLK.Enabled = false;
                btnQLNCC.Enabled = false;
                btnQLNS.Enabled = false;
                btnQLSP.Enabled = false;
            }
            else if(nd.LoaiTK == "3")
            {
                btnQLBH.Enabled = false;                
                btnQLKH.Enabled = false;
                btnQLNS.Enabled = false;
                btnQLHT.Enabled = false;                
            }
        }

        private void btnQLSP_Click(object sender, EventArgs e)
        {
            frm_QLSP frm = new frm_QLSP();
            frm.Show();
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {

        }

        private void btnQLNS_Click(object sender, EventArgs e)
        {

        }

        private void btnQLBH_Click(object sender, EventArgs e)
        {

        }

        private void btnQLNX_Click(object sender, EventArgs e)
        {

        }

        private void btnQLNCC_Click(object sender, EventArgs e)
        {

        }

        private void btnQLK_Click(object sender, EventArgs e)
        {

        }

        private void btnTKBB_Click(object sender, EventArgs e)
        {

        }

        private void btnQLHT_Click(object sender, EventArgs e)
        {

        }
    }
}
