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
using System.Xml;
using System.Security.Cryptography;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        public static string quyen, tendn;
        public frmDangNhap()
        {
            InitializeComponent();
            txtTKDN.Focus();

            txtMKDN.Text = "";
            txtMKDN.PasswordChar = '*';
            txtMKDN.MaxLength = 14;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtMKDN.Text = "";
            txtMKDN.PasswordChar = '*';
            txtMKDN.MaxLength = 14;
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát chương trình!", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            
            string username = txtTKDN.Text;
            string password = txtMKDN.Text;
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hashPass = "";

            foreach(byte item in hashData)
            {
                hashPass += item;
            }    

            string sqlSelect = "SELECT * FROM NHANVIEN WHERE ID = '" + username + "' AND PASS = '" + hashPass + "'";
            SqlDataAdapter da = conn.getDataAdapter(sqlSelect);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (username.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Thông báo");
            }
            else if (password.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo");
            }
            else
            {
                if (dt.Rows.Count != 0)
                {
                    string sqlSelect2 = "SELECT NHANVIEN.MAQUYEN FROM PHANQUYEN,NHANVIEN WHERE PHANQUYEN.MAQUYEN = NHANVIEN.MAQUYEN AND NHANVIEN.ID = '" + username + "'";
                    string sqlSelect3 = "SELECT TENNV FROM NHANVIEN WHERE NHANVIEN.ID = '" + username + "'";
                    quyen = conn.getDataTable(sqlSelect2).Rows[0][0].ToString().Trim();
                    tendn = conn.getDataTable(sqlSelect3).Rows[0][0].ToString().Trim();
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    frmMain frm = new frmMain();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo");
                    txtTKDN.Clear();
                    txtMKDN.Clear();
                    txtTKDN.Focus();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}