using baitap06.Models;
using Microsoft.AspNetCore.Mvc;

namespace baitap06.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangKy( taikhoanViewModel taikhoan)
        {
            if (taikhoan != null && taikhoan.Password != null && taikhoan.Password.Length > 0)
            {
                taikhoan.Password = taikhoan.Password + "_đã_mã_hóa";
            }
            return View(taikhoan);
        }
    }
}
