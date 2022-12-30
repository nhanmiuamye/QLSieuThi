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
using System.Security.Cryptography;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dgvNV.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNV.DataSource = GetAllNhanVien().Tables[0];
        }
        DataSet GetAllNhanVien()
        {
            DataSet data = new DataSet();

            //SQLConnection trong using thì sau khi sài xong là bỏ
            string query = "select * from NHANVIEN";
            using (SqlConnection conn = new SqlConnection(TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi.Properties.Settings.Default.strConnect))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(data);

                conn.Close();
            }
            return data;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenNV.Clear();
            txtMaNV.Clear();
            txtChucVu.Clear();
            txtMaQuyen.Clear();
            txtNgayCong.Clear();
            txtLCB.Clear();
            txtID_NV.Clear();
            txtPass.Clear();
            dgvNV.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNV.DataSource = GetAllNhanVien().Tables[0];

        }
        bool KT_Ma(string ma)
        {
            Connection conn = new Connection();
            try
            {
                conn.Connect();

                string selectString = "select count(*) from NHANVIEN where MANV = '" + ma + "'";
                SqlCommand cmd = conn.getSqlCommand(selectString);
                int count = (int)cmd.ExecuteScalar();

                conn.Disconnect();

                if (count >= 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            try
            {
                if (KT_Ma(txtMaNV.Text))
                {
                    byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtPass.Text);
                    byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);

                    string hashPass = "";

                    foreach (byte item in hashData)
                    {
                        hashPass += item;
                    }
                    string sql = "INSERT INTO NHANVIEN VALUES('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "', '" + txtChucVu.Text + "', '" + txtMaQuyen.Text + "', '" + txtID_NV.Text + "','" + hashPass + "', " + txtNgayCong.Text + ", " + txtLCB.Text + "," + (int.Parse(txtNgayCong.Text)/30)*int.Parse(txtLCB.Text)+ ")";

                    conn.ExecuteNonQuery(sql);

                    MessageBox.Show("Thêm thành công !!");

                    dgvNV.AutoSize = true;
                    //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvNV.DataSource = GetAllNhanVien().Tables[0];

                }
                else
                {
                    MessageBox.Show("Trùng mã Nhân Viên!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thêm được!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == null)
            {
                MessageBox.Show("Nhập mã Nhân Viên muốn sửa");
            }
            else
            {
                Connection conn = new Connection();
                byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtPass.Text);
                byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);

                string hashPass = "";

                foreach (byte item in hashData)
                {
                    hashPass += item;
                }
                string sql = "UPDATE NHANVIEN SET TENNV =N'" + txtTenNV.Text + "', CHUCVU = '" + txtChucVu.Text + "', MAQUYEN = '" + txtMaQuyen.Text + "', ID = '" + txtID_NV.Text + "', PASS = '" + hashPass + "', NGAYCONG =" + txtNgayCong.Text + ", LUONGCOBAN = " + txtLCB.Text + " WHERE MANV = '" + txtMaNV.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công !!");

                dgvNV.AutoSize = true;
                //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvNV.DataSource = GetAllNhanVien().Tables[0];
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            if (txtMaNV.Text == null)
            {
                MessageBox.Show("Nhập mã nhân viên muốn xóa");
            }
            else
            {
                string sql = "DELETE FROM NHANVIEN WHERE MANV ='" + txtMaNV.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Xóa thành công!!");

                dgvNV.AutoSize = true;
                //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvNV.DataSource = GetAllNhanVien().Tables[0];
            }
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0)
            {
                //lưu lại dòng dữ liệu vừa click 
                DataGridViewRow row = this.dgvNV.Rows[e.RowIndex];
                //đưa dữ liệu lên textbox
                txtMaNV.Text = row.Cells[0].Value.ToString();
                txtTenNV.Text = row.Cells[1].Value.ToString();
                txtChucVu.Text = row.Cells[2].Value.ToString();
                txtMaQuyen.Text = row.Cells[3].Value.ToString();
                txtID_NV.Text = row.Cells[4].Value.ToString();
                txtNgayCong.Text = row.Cells[6].Value.ToString();
                txtLCB.Text = row.Cells[7].Value.ToString();
                txtPass.Text = row.Cells[5].Value.ToString();

                //không cho sửa mã NV
                txtMaNV.Enabled = false;
            }    
        }
    }
}