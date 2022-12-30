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
    public partial class frmNCC : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        public frmNCC()
        {
            InitializeComponent();
        }

        DataSet GetAllNCC()
        {
            DataSet data = new DataSet();

            //SQLConnection trong using thì sau khi sài xong là bỏ
            string query = "select * from NHACUNGCAP";
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
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            dgvNCC.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNCC.DataSource = GetAllNCC().Tables[0];
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            dgvNCC.AutoSize = true;
            //dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNCC.DataSource = GetAllNCC().Tables[0];
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //lưu lại dòng dữ liệu vừa click 
                DataGridViewRow row = this.dgvNCC.Rows[e.RowIndex];
                //đưa dữ liệu lên textbox
                txtMaNCC.Text = row.Cells[0].Value.ToString();
                txtTenNCC.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSDT.Text = row.Cells[3].Value.ToString();

                //không cho sửa mã NV
                txtMaNCC.Enabled = false;
            }
        }
    }
}