using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Data.Migrations;
using Project1.Models;

namespace Project1.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Models.HoaDon> hoadon = _db.HoaDon
                                    .Include(h => h.ApplicationUser)
                                    .Where(h => !h.IsCancelled && (h.OrderStatus == "Đang xác nhận" || h.OrderStatus == "Đã xác nhận"))
                                    .ToList();
            return View(hoadon);
        }

        public IActionResult ChiTiet(int? id)
        {
            var hoadon = _db.HoaDon.Include(h => h.ApplicationUser).Include(h => h.ChiTietHoaDon).ThenInclude(ct => ct.SanPham).FirstOrDefault(h => h.Id == id);
            if (hoadon == null)
            {
                return NotFound();
            }
            return View(hoadon);
        }

        [HttpPost]
        public IActionResult XacNhanDonHang(int id)
        {
            var hoadon = _db.HoaDon.Find(id);
            if (hoadon == null)
            {
                return NotFound();
            }

            if (hoadon.OrderStatus == "Đang xác nhận")
            {
                hoadon.IsConfirmed = true;
                hoadon.ConfirmedDate = DateTime.Now;
                hoadon.OrderStatus = "Đã xác nhận";
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult HuyDonHang(int id, string cancellationReason)
        {
            var hoadon = _db.HoaDon.Find(id);
            if (hoadon == null)
            {
                return NotFound();
            }
            if (hoadon.OrderStatus == "Đang xác nhận" || hoadon.OrderStatus == "Đã xác nhận" || hoadon.OrderStatus == "Đang chờ hủy")
            {
                // Cập nhật trạng thái và lý do hủy
                hoadon.IsCancelled = true;
                hoadon.CancellationReason = cancellationReason;
                hoadon.OrderStatus = "Đã bị hủy"; // Cập nhật trạng thái
                hoadon.CancelledDate = DateTime.Now;

                _db.SaveChanges();
            }

            // Chuyển hướng đến trang đơn hàng đã hủy
            return RedirectToAction("DonHangDaHuy");
        }

            public IActionResult DonHangDaHuy()
        {
            var danhSachDonHangDaHuy = _db.HoaDon
                               .Include(h => h.ApplicationUser) // Nếu bạn cần thông tin người dùng
                               .Where(h => h.IsCancelled) // Chỉ lấy đơn hàng đã hủy
                               .ToList();
            return View(danhSachDonHangDaHuy);
        }
    }
}
