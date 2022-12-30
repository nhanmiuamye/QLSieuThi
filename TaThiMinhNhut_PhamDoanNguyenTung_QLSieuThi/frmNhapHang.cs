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
    public partial class frmNhapHang : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        public frmNhapHang()
        {
            InitializeComponent();
        }
        void LoadDuLieuDataGridView()
        {
            string strSelect = "select * from HOADONNHAPHANG";
            DataTable dt = conn.getDataTable(strSelect);
            dgvNhapHang.DataSource = dt;

        }
        void Databingding(DataTable pDT)
        {
            txtSoHDNhap.DataBindings.Clear();
            txtNgayNhap.DataBindings.Clear();
            txtNVNhapHD.DataBindings.Clear();
            txtMaNCC.DataBindings.Clear();
            txtMaHang.DataBindings.Clear();
            txtSL.DataBindings.Clear();
            txtDVT.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();
            txtGiamGia.DataBindings.Clear();

            txtSoHDNhap.Text = dgvNhapHang.CurrentRow.Cells[3].Value?.ToString();
            txtNgayNhap.Text = dgvNhapHang.CurrentRow.Cells[4].Value?.ToString();
            txtNVNhapHD.Text = dgvNhapHang.CurrentRow.Cells[5].Value?.ToString();
            txtMaNCC.Text = dgvNhapHang.CurrentRow.Cells[6].Value?.ToString();
            txtMaHang.Text = dgvNhapHang.CurrentRow.Cells[7].Value?.ToString();
            txtSL.Text = dgvNhapHang.CurrentRow.Cells[8].Value?.ToString();
            txtDVT.Text = dgvNhapHang.CurrentRow.Cells[9].Value?.ToString();
            txtTongTien.Text = dgvNhapHang.CurrentRow.Cells[10].Value?.ToString();
            txtGiamGia.Text = dgvNhapHang.CurrentRow.Cells[11].Value?.ToString();

        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            LoadDuLieuDataGridView();

            Databingding(ds.Tables["HOADONNHAPHANG"]);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoHDNhap.Clear();
            txtNgayNhap.Clear();
            txtNVNhapHD.Clear();
            txtMaNCC.Clear();
            txtMaHang.Clear();
            txtSL.Clear();
            txtDVT.Clear();
            txtTongTien.Clear();
            txtGiamGia.Clear();
            LoadDuLieuDataGridView();
        }
        bool KT_SHD(string sohd)
        {
            try
            {
                conn.Connect();

                string selectString = "select count(*) from HOADONNHAPHANG where SOHDNHAP = '" + sohd + "'";
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
                if (KT_SHD(txtSoHDNhap.Text))
                {
                    string sql = "INSERT INTO HOADONNHAPHANG VALUES('" + txtSoHDNhap.Text + "', '" + txtNgayNhap.Text + "', '" + txtNVNhapHD.Text + "', '" + txtMaNCC.Text + "', '" + txtMaHang.Text + "', " + txtSL.Text + ", N'" + txtDVT.Text +"', " + txtTongTien.Text + ")";

                    conn.ExecuteNonQuery(sql);

                    MessageBox.Show("Thêm thành công !!");

                    LoadDuLieuDataGridView();

                }
                else
                {
                    MessageBox.Show("Trùng khóa chính");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtSoHDNhap.Text == null)
            {
                MessageBox.Show("Nhập số hóa đơn muốn xóa");
            }
            else
            {
                string sql = "DELETE FROM HOADONNHAPHANG WHERE SOHDNHAP ='" + txtSoHDNhap.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công!!");

                LoadDuLieuDataGridView();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSoHDNhap.Text == null)
            {
                MessageBox.Show("Nhập mã hóa đơn muốn sửa");
            }
            else
            {
                string sql = "UPDATE HOADONNHAPHANG SET SOHDNHAP ='" + txtSoHDNhap.Text + "', NGAYNHAP = '" + txtNgayNhap.Text + "', NVNHAPHD = '" + txtNVNhapHD.Text + "', MANCC = '" + txtMaNCC.Text + "', MAHANG ='" + txtMaHang.Text + "', SOLUONG =" + txtSL.Text + ", DVT = N'" + txtDVT.Text + "', TONGTIEN = " + txtTongTien.Text + ", GIAMGIA = " + txtGiamGia.Text + " WHERE SOHDNHAP = '" + txtSoHDNhap.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công !!");

                LoadDuLieuDataGridView();
            }
        }

        private void dgvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtSoHDNhap.Text = dgvNhapHang.Rows[index].Cells[3].Value?.ToString().Trim();
            txtNgayNhap.Text = dgvNhapHang.Rows[index].Cells[4].Value?.ToString().Trim();
            txtNVNhapHD.Text = dgvNhapHang.Rows[index].Cells[5].Value?.ToString().Trim();
            txtMaNCC.Text = dgvNhapHang.Rows[index].Cells[6].Value?.ToString().Trim();
            txtMaHang.Text = dgvNhapHang.Rows[index].Cells[7].Value?.ToString().Trim();
            txtSL.Text = dgvNhapHang.Rows[index].Cells[8].Value?.ToString().Trim();
            txtDVT.Text = dgvNhapHang.Rows[index].Cells[9].Value?.ToString().Trim();
            txtTongTien.Text = dgvNhapHang.Rows[index].Cells[10].Value?.ToString().Trim();
            txtGiamGia.Text = dgvNhapHang.Rows[index].Cells[11].Value?.ToString().Trim();
        }

        private void dgvNhapHang_SelectionChanged(object sender, EventArgs e)
        {
            Databingding(ds.Tables["HOADONNHAPHANG"]);
        }
    }
}