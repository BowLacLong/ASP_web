using BTVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTVN.Controllers
{
    public class BTVNController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Hoten = "Vũ Hải Luân";
            ViewBag.MSSV = "1822041389";
            ViewData["Nam"] = "2004";
            return View();
        }

        public IActionResult MayTinh(int a, int b, string pheptinh)
        {
            int result = 0;

            switch (pheptinh)
            {
                case "cong":
                    result = a + b;
                    break;
                case "tru":
                    result = a - b;
                    break;
                case "nhan":
                    result = a * b;
                    break;
                case "chia":
                    if (b != 0)
                    {
                        result = a / b;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Không thể chia cho 0.";
                    }
                    break;
                default:
                    ViewBag.ErrorMessage = "Phép tính không hợp lệ.";
                    break;
            }

            ViewBag.Result = result;

            return View("MayTinh");
        }
        public IActionResult Profile()
        {
            return View();
        }
    }

}
