using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai").ToList();
            return View(sanpham);   

        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            SanPham sanpham = new SanPham();
            IEnumerable<SelectListItem> dstheloai = _db.TheLoai.Select
            (
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }
            );
            ViewBag.DSTheLoai = dstheloai;
            if (id == 0)
            {
                return View(sanpham);
            }
            else
            {
                sanpham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == id);
                return View(sanpham);
            }
        }
        [HttpPost]
        public IActionResult Upsert(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.Id == 0)
                {
                    _db.SanPham.Add(sanpham);
                }
                else
                {
                    _db.SanPham.Update(sanpham);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var sanPham = _db.SanPham
                .Include(sp => sp.TheLoai)
                .FirstOrDefault(sp => sp.Id == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _db.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _db.SanPham.Remove(sanPham);
            await _db.SaveChangesAsync();
            return
         RedirectToAction(nameof(Index));
        }


    }
}
