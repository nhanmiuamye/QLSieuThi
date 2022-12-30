using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmKetNoi : DevExpress.XtraEditors.XtraForm
    {
        public frmKetNoi()
        {
            InitializeComponent();
        }

        private void frmKetNoi_Load(object sender, EventArgs e)
        {
            txtUserID.Enabled = true;
            txtPassword.Enabled = true;
            txtDatabase.Text = "DB_QLST";
            txtDataSource.Text = @"DESKTOP-VL7UGOA\NHANMIU263";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string strConnect = "";
            if (chkAut.Checked)
                strConnect = @"Data Source=" + txtDataSource.Text + ";Initial Catalog=" + txtDatabase.Text + ";Integrated Security=True";
            else
                strConnect = @"Data Source=" + txtDataSource.Text + ";Initial Catalog=" + txtDatabase.Text + ";User Id=" + txtUserID.Text + ";Password = " + txtPassword.Text + "; ";
            SqlConnection ConnSQL = new SqlConnection(strConnect);
            try
            {
                ConnSQL.Open();
                ConnSQL.Close();
                TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi.Properties.Settings.Default.strConnect = strConnect;
                TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi.Properties.Settings.Default.Save();
                MessageBox.Show("Kết nối thành công");
                frmDangNhap frm = new frmDangNhap();
                this.Hide();
                frm.Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối không thành công, xin kiểm tra lại!");
                ConnSQL.Close();
            }
        }

        private void frmKetNoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát chương trình!", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void chkAut_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAut.Checked)
            {
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;

            }
            else
            {
                txtUserID.Enabled = true;
                txtPassword.Enabled = true;
            }
        }
    }
}