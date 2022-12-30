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
using System.Xml;
using System.Data.SqlClient;
namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();

        public frmKhachHang()
        {
            InitializeComponent();
        }

        DataSet GetAllNhanVien()
        {
            DataSet data = new DataSet();

            //SQLConnection trong using thì sau khi sài xong là bỏ
            string query = "select * from KHACHHANG";
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
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            dgvKhachHang.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.DataSource = GetAllNhanVien().Tables[0];
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            dgvKhachHang.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.DataSource = GetAllNhanVien().Tables[0];
        }



        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //lưu lại dòng dữ liệu vừa click 
                DataGridViewRow row = this.dgvKhachHang.Rows[e.RowIndex];
                //đưa dữ liệu lên textbox
                txtMaKH.Text = row.Cells[0].Value.ToString();
                txtTenKH.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSDT.Text = row.Cells[3].Value.ToString();

                //không cho sửa mã NV
                txtMaKH.Enabled = false;
            }
        }
    }
}