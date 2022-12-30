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
    public partial class frmHangHoa : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        

        public frmHangHoa()
        {
            InitializeComponent();
        }
        void LoadDuLieuDataGridView()
        {
            string strSelect = "select * from HANGHOA";
            DataTable dt = conn.getDataTable(strSelect);
            dgvHangHoa.DataSource = dt;

        }

        void Databingding(DataTable pDT)
        {
            txtMaHang.DataBindings.Clear();
            txtTenHang.DataBindings.Clear();
            txtGiaBan.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();
            txtDVT.DataBindings.Clear();
            txtMaLoaiHH.DataBindings.Clear();
            txtMaNCC.DataBindings.Clear();

            txtMaHang.Text = dgvHangHoa.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dgvHangHoa.CurrentRow.Cells[1].Value.ToString();
            txtMaLoaiHH.Text = dgvHangHoa.CurrentRow.Cells[2].Value.ToString();
            txtMaNCC.Text = dgvHangHoa.CurrentRow.Cells[3].Value.ToString();
            txtGiaBan.Text = dgvHangHoa.CurrentRow.Cells[4].Value.ToString();
            txtSoLuong.Text = dgvHangHoa.CurrentRow.Cells[5].Value.ToString();
            txtDVT.Text = dgvHangHoa.CurrentRow.Cells[6].Value.ToString();

        }

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            LoadDuLieuDataGridView();

            Databingding(ds.Tables["HANGHOA"]);
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtDVT.Clear();
            txtMaLoaiHH.Clear();
            txtMaNCC.Clear();
            LoadDuLieuDataGridView();
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            Databingding(ds.Tables["HANGHOA"]);
        }
        bool KT_Ma(string ma)
        {
            try
            {
                conn.Connect();

                string selectString = "select count(*) from HANGHOA where MAHANG = '" + ma + "'";
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
                if (KT_Ma(txtMaHang.Text))
                {
                    string sql = "INSERT INTO HANGHOA VALUES('" + txtMaHang.Text + "', N'" + txtTenHang.Text + "', '" + txtMaLoaiHH.Text + "', '" + txtMaNCC.Text + "', '" + txtGiaBan.Text + "', '" + txtSoLuong.Text + "', '" + txtDVT.Text + "')";

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
            if (txtMaHang.Text == null)
            {
                MessageBox.Show("Nhập mã hàng muốn xóa");
            }
            else
            {
                string sql = "DELETE FROM HANGHOA WHERE MAHANG ='" + txtMaHang.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công!!");

                LoadDuLieuDataGridView();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text == null)
            {
                MessageBox.Show("Nhập mã khối muốn sửa");
            }
            else
            {
                string sql = "UPDATE HANGHOA SET TENHANG =N'" + txtTenHang.Text + "', GIABAN = '" + txtGiaBan.Text + "', SOLUONG = '" + txtSoLuong.Text + "', DVT = '" + txtDVT.Text + "', MALOAIHH ='" + txtMaLoaiHH.Text + "', MANCC = '" + txtMaNCC.Text + "' WHERE MAHANG = '" + txtMaHang.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công !!");

                LoadDuLieuDataGridView();
            }
        }

        private void cbMaLoaiHH_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cbMaLoaiHH_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtMaHang.Text = dgvHangHoa.Rows[index].Cells[0].Value.ToString().Trim();
            txtTenHang.Text = dgvHangHoa.Rows[index].Cells[1].Value.ToString().Trim();
            txtMaLoaiHH.Text = dgvHangHoa.Rows[index].Cells[2].Value.ToString().Trim();
            txtMaNCC.Text = dgvHangHoa.Rows[index].Cells[3].Value.ToString().Trim();
            txtGiaBan.Text = dgvHangHoa.Rows[index].Cells[4].Value.ToString().Trim();
            txtSoLuong.Text = dgvHangHoa.Rows[index].Cells[5].Value.ToString().Trim();
            txtDVT.Text = dgvHangHoa.Rows[index].Cells[6].Value.ToString().Trim();
        }
    }
}