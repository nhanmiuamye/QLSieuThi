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
    public partial class frmBanHang : DevExpress.XtraEditors.XtraForm
    {
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        public frmBanHang()
        {
            InitializeComponent();
        }
        void LoadDuLieuDataGridView()
        {
            string strSelect = "select * from HOADONBANHANG";
            DataTable dt = conn.getDataTable(strSelect);
            dgvBanHang.DataSource = dt;

        }
        void Databingding(DataTable pDT)
        {
            txtSoHD.DataBindings.Clear();
            txtNgayLap.DataBindings.Clear();
            txtNVlapHD.DataBindings.Clear();
            txtMaKH.DataBindings.Clear();
            txtMaHang.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();

            txtSoHD.Text = dgvBanHang.CurrentRow.Cells[0].Value.ToString();
            txtNgayLap.Text = dgvBanHang.CurrentRow.Cells[1].Value.ToString();
            txtNVlapHD.Text = dgvBanHang.CurrentRow.Cells[2].Value.ToString();
            txtMaKH.Text = dgvBanHang.CurrentRow.Cells[3].Value.ToString();
            txtMaHang.Text = dgvBanHang.CurrentRow.Cells[4].Value.ToString();
            txtTongTien.Text = dgvBanHang.CurrentRow.Cells[5].Value.ToString();


        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            LoadDuLieuDataGridView();

            Databingding(ds.Tables["HOADONBANHANG"]);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoHD.Clear();
            txtNgayLap.Clear();
            txtNVlapHD.Clear();
            txtMaKH.Clear();
            txtMaHang.Clear();
            txtTongTien.Clear();
            LoadDuLieuDataGridView();
        }

        bool KT_SHD(string sohd)
        {
            try
            {
                conn.Connect();

                string selectString = "select count(*) from HOADONBANHANG where SOHD = '" + sohd + "'";
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
                if (KT_SHD(txtSoHD.Text))
                {
                    string sql = "INSERT INTO HOADONBANHANG VALUES('" + txtSoHD.Text + "', '" + txtNgayLap.Text + "', N'" + txtNVlapHD.Text + "', '" + txtMaKH.Text + "', '" + txtMaHang.Text + "', " + txtTongTien.Text + ")";

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
            if (txtSoHD.Text == null)
            {
                MessageBox.Show("Nhập số hóa đơn muốn xóa");
            }
            else
            {
                string sql = "DELETE FROM HOADONBANHANG WHERE SOHD ='" + txtSoHD.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công!!");

                LoadDuLieuDataGridView();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSoHD.Text == null)
            {
                MessageBox.Show("Nhập mã hóa đơn muốn sửa");
            }
            else
            {
                string sql = "UPDATE HOADONBANHANG SET SOHD ='" + txtSoHD.Text + "', NGAYLAP = '" + txtNgayLap.Text + "', NVLAPHD = '" + txtNVlapHD.Text + "', MAKH = '" + txtMaKH.Text + "', MAHANG ='" + txtMaHang.Text + "', TONGTIEN = " + txtTongTien.Text + " WHERE SOHD = '" + txtSoHD.Text + "'";

                conn.ExecuteNonQuery(sql);

                MessageBox.Show("Thành công !!");

                LoadDuLieuDataGridView();
            }
        }

        private void dgvBanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtSoHD.Text = dgvBanHang.Rows[index].Cells[0].Value.ToString().Trim();
            txtNgayLap.Text = dgvBanHang.Rows[index].Cells[1].Value.ToString().Trim();
            txtNVlapHD.Text = dgvBanHang.Rows[index].Cells[2].Value.ToString().Trim();
            txtMaKH.Text = dgvBanHang.Rows[index].Cells[3].Value.ToString().Trim();
            txtMaHang.Text = dgvBanHang.Rows[index].Cells[4].Value.ToString().Trim();
            txtTongTien.Text = dgvBanHang.Rows[index].Cells[5].Value.ToString().Trim();
        }

        private void dgvBanHang_SelectionChanged(object sender, EventArgs e)
        {
            Databingding(ds.Tables["HOADONBANHANG"]);
        }
    }
}