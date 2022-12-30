using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public static string Quyen, Ten;
        public frmMain()
        {
            InitializeComponent();
            Quyen = frmDangNhap.quyen;
            Ten = frmDangNhap.tendn;
            barStaticItemTen.Caption = "Chào, " + Ten;
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có thật sự muốn thoát ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
        public void GiaoDienMacDinh()
        {
            DevExpress.LookAndFeel.DefaultLookAndFeel theme = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            theme.LookAndFeel.SkinName = "Black";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GiaoDienMacDinh();

        }

        private void barDockingMenuItem1_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {

        }

        private void btnHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHangHoa frm = new frmHangHoa();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnLoaiHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmLoaiHang frm = new frmLoaiHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Đăng Xuất!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmDangNhap dn = new frmDangNhap();
                dn.Show();
                this.Hide();
            }
        }

        private void btnThongTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThongTinPM frm = new frmThongTinPM();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHuongDan frm = new frmHuongDan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBanHang frm = new frmBanHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnNhapHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhapHang frm = new frmNhapHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNCC frm = new frmNCC();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnTKXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTKXuat frm = new frmTKXuat();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnTKNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTKNhap frm = new frmTKNhap();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnTKTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTKTonKho frm = new frmTKTonKho();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Quyen == "AD")
            {
                frmNhanVien frm = new frmNhanVien();
                frm.MdiParent = this;
                frm.Show();
            }
            else
                btnNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

    }
}
