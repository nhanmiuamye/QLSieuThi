using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmLoaiHang : Form
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        public frmLoaiHang()
        {
            InitializeComponent();
        }
        void LoadDuLieuDataGridView()
        {
            string strSelect = "select * from LOAIHANGHOA";
            DataTable dt = conn.getDataTable(strSelect);
            dgvLoaiHH.DataSource = dt;

        }
        void Databingding(DataTable pDT)
        {
            txtMaLoaiHH.DataBindings.Clear();
            txtTenLoaiHH.DataBindings.Clear();


            txtMaLoaiHH.Text = dgvLoaiHH.CurrentRow.Cells[0].Value.ToString();
            txtTenLoaiHH.Text = dgvLoaiHH.CurrentRow.Cells[1].Value.ToString();

        }

        private void frmLoaiHang_Load(object sender, EventArgs e)
        {
            LoadDuLieuDataGridView();

            Databingding(ds.Tables["LOAIHANGHOA"]);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLoaiHH.Clear();
            txtTenLoaiHH.Clear();
            LoadDuLieuDataGridView();
        }

        private void dgvLoaiHH_SelectionChanged(object sender, EventArgs e)
        {
            Databingding(ds.Tables["LOAIHANGHOA"]);
        }
        bool KT_Ma(string ma)
        {
            try
            {
                conn.Connect();

                string selectString = "select count(*) from LOAIHANGHOA where MALOAIHH = '" + ma + "'";
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
            try
            {
                if (KT_Ma(txtMaLoaiHH.Text))
                {
                    string sql = "INSERT INTO LOAIHANGHOA VALUES('" + txtMaLoaiHH.Text + "', N'" + txtTenLoaiHH.Text + "')";

                    conn.ExecuteNonQuery(sql);

                    MessageBox.Show("Thành công !!");

                    LoadDuLieuDataGridView();

                }
                else
                {
                    MessageBox.Show("Trùng khóa chính");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thất Bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiHH.Text == null)
            {
                MessageBox.Show("Nhập mã loại hàng hóa muốn xóa");
            }
            else
            {
                string sql = "DELETE FROM LOAIHANGHOA WHERE MALOAIHH ='" + txtMaLoaiHH.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công!!");

                LoadDuLieuDataGridView();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiHH.Text == null)
            {
                MessageBox.Show("Nhập mã loại hàng hóa muốn sửa");
            }
            else
            {
                string sql = "UPDATE LOAIHANGHOA SET MALOAIHH ='" + txtMaLoaiHH.Text + "', TENLOAIHH = N'" + txtTenLoaiHH.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công !!");

                LoadDuLieuDataGridView();
            }
        }

        private void dgvLoaiHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtMaLoaiHH.Text = dgvLoaiHH.Rows[index].Cells[0].Value.ToString().Trim();
            txtTenLoaiHH.Text = dgvLoaiHH.Rows[index].Cells[1].Value.ToString().Trim();
        }
    }
}
