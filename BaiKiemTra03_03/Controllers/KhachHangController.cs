using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra03_03.Controllers
{
    public class KhachHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
