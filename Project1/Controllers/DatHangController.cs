﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using System.Security.Claims;

namespace Project1.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class DatHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DatHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập
            }
            IEnumerable<HoaDon> hoadon = _db.HoaDon
                    .Include(h => h.ApplicationUser)
                    .Where(h => h.ApplicationUserId == userId && // Chỉ lấy đơn hàng của người dùng hiện tại
                               !h.IsCancelled && (h.OrderStatus == "Đang xác nhận" || h.OrderStatus == "Đã xác nhận" || h.OrderStatus == "Đang chờ hủy"))
                    .ToList();
            return View(hoadon);
        }

        public IActionResult ChiTiet(int id)
        {
            var dathang = _db.HoaDon
                             .Include(h => h.ApplicationUser)
                             .Include(h => h.ChiTietHoaDon)
                             .ThenInclude(ct => ct.SanPham)
                             .FirstOrDefault(h => h.Id == id);

            if (dathang == null)
            {
                return NotFound();
            }

            // Kiểm tra quyền truy cập (chỉ chủ sở hữu đơn hàng mới được xem chi tiết)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (dathang.ApplicationUserId != userId)
            {
                return Unauthorized(); // Trả về 401 Unauthorized
            }

            return View(dathang);
        }

        [HttpPost]
        public async Task<IActionResult> HuyDonHang(int id, string cancellationReason)
        {
            var hoaDon = _db.HoaDon.Find(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hoaDon.ApplicationUserId != userId)
            {
                return Unauthorized();
            }

            if (hoaDon.OrderStatus == "Đang xác nhận")
            {
                // Hủy đơn hàng ngay
                hoaDon.IsCancelled = true;
                hoaDon.CancellationReason = cancellationReason;
                hoaDon.OrderStatus = "Đã bị hủy";
                hoaDon.CancelledDate = DateTime.Now;
            }
            else if (hoaDon.OrderStatus == "Đã xác nhận")
            {
                hoaDon.CancellationReason = cancellationReason;
                hoaDon.OrderStatus = "Đang chờ hủy";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
