using Microsoft.AspNetCore.Mvc;

namespace baiap03.Controllers
{
    public class NhomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
