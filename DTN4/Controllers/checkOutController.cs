using AspNetCoreHero.ToastNotification.Abstractions;
using DTN4.Extension;
using DTN4.Models;
using DTN4.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DTN4.Controllers
{
    public class checkOutController : Controller
    {
        private readonly DTNContext _context;
        public INotyfService _notifyService { get; }

        public checkOutController(DTNContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        public IActionResult MuaHang()
        {
            var Cart = HttpContext.Session.Get<List<CartItem>>("cart");
            var taikhoanID = HttpContext.Session.GetString("CustomerID");
            muaHang model = new muaHang();
            if(taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
                model.CustomerID = khachhang.CustomId;
                model.fullName = khachhang.CustomName;
                model.Phone = khachhang.Phone;
                model.Email = khachhang.Email;
                model.Address = khachhang.Address;
                


            }
            ViewBag.cart = Cart;
            return View(model);
        }
        public IActionResult Index(muaHang muaHang)
        {
            var Cart = HttpContext.Session.Get<List<CartItem>>("cart");
            var taikhoanID = HttpContext.Session.GetString("CustomerID");
            muaHang model = new muaHang();
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomId == Convert.ToInt32(taikhoanID));
                
                model.CustomerID = khachhang.CustomId;
                model.fullName = khachhang.CustomName;
                model.Phone = khachhang.Phone;
                model.Email = khachhang.Email;
                model.Address = khachhang.Address; 

                //khachhang.Location = muaHang.TinhThanh;
                khachhang.District = muaHang.QuanHuyen;
                khachhang.Word = muaHang.ThiXa;
                khachhang.Address = muaHang.Address;
                _context.Update(khachhang);
            }
            return View();
        }




    }
}
