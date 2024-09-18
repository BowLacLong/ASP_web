﻿using baitap07.Data;
using baitap07.Models;
using Microsoft.AspNetCore.Mvc;

namespace baitap07.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            var theloai = _db.TheLoai.ToList();
            ViewBag.TheLoai = theloai;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TheLoai theloai)
        {
            _db.TheLoai.Add(theloai);
            _db.SaveChanges();
            return View();
        }
    }
}