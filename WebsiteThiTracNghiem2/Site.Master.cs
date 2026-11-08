
using System;

namespace WebsiteThiTracNghiem
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiMenu();
        }

        // Hiển thị giao diện theo trạng thái đăng nhập
        private void HienThiMenu()
        {
            string tenDangNhap =
                Session["TenDangNhap"] as string;

            string vaiTro =
                Session["VaiTro"] as string;

            bool daDangNhap =
                !string.IsNullOrWhiteSpace(tenDangNhap);

            // Hiển thị nút đăng nhập, đăng ký
            phKhach.Visible = !daDangNhap;

            // Hiển thị thông tin tài khoản
            phNguoiDung.Visible = daDangNhap;

            // Menu dành cho User và Admin
            phMenuUser.Visible = daDangNhap;

            // Chỉ Admin mới thấy menu quản trị
            phMenuAdmin.Visible =
                daDangNhap &&
                string.Equals(
                    vaiTro,
                    "Admin",
                    StringComparison.OrdinalIgnoreCase
                );

            // Hiển thị tên tài khoản
            if (daDangNhap)
            {
                lblTenNguoiDung.Text =
                    Server.HtmlEncode(tenDangNhap);
            }
        }

        // Xử lý đăng xuất
        protected void btnDangXuat_Click(
            object sender,
            EventArgs e)
        {
            // Xóa toàn bộ dữ liệu Session
            Session.Clear();

            // Hủy phiên đăng nhập
            Session.Abandon();

            // Chuyển về trang chủ
            Response.Redirect(
                "~/TrangChu.aspx",
                false
            );

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
