using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanGiay.Controllers
{
    public class LocWebsiteController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: LocWebsite
        public PartialViewResult DanhSachDanhMuc()
        {
            return PartialView(db.DanhMucs.ToList());
        }
        public PartialViewResult HangMoiVe()
        {
            return PartialView(db.Giays.OrderByDescending(n=>n.NgayHangVe).Take(8).ToList());
        }
        public PartialViewResult HangBanChay()
        {
            return PartialView(db.Giays.OrderByDescending(n => n.SoDonHang).Take(8).ToList());
        }
        public PartialViewResult HangBanChaySlider()
        {
            return PartialView(db.Giays.OrderByDescending(n => n.NgayHangVe).Take(3).ToList());
        }
        public PartialViewResult SanPhamGiamGia()
        {
            return PartialView(db.Giays.Where(n=>n.GiamGia > 0).OrderByDescending(n=>n.NgayHangVe).Take(9).ToList());
        }
        public PartialViewResult SanPhamMoiTheoDM(int? maDM)
        {
            return PartialView(db.Giays.Where(n => n.MaDanhMuc == maDM).OrderByDescending(n => n.NgayHangVe).Take(6).ToList());
        }
        public PartialViewResult DanhMucSanPham()
        {
            return PartialView(db.DanhMucs.ToList());
        }
        public ViewResult XemChiTiet(int magiay = 0)
        {
            Session["magiay"] = magiay;
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == magiay);
            return View(giay);
        }
        public ActionResult XemThem(int? page, int? maDM)
        {
            ViewBag.TB = db.DanhMucs.Find(maDM).TenDanhMuc;
            int num = 15;
            int pagenum = (page ?? 1);
            return View(db.Giays.Where(n => n.MaDanhMuc == maDM).OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
        }
        public ActionResult GiayThuocPhai(int? page, int? maPhai)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            if(maPhai == 0)
            {
                ViewBag.TB = "giày nam";
                return View(db.Giays.Where(t => t.Phai == 0 || t.Phai == 2).OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
            }
            else
            {
                ViewBag.TB = "giày nữ";
                return View(db.Giays.Where(t => t.Phai == 1 || t.Phai == 2).OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
            }
        }
        public ActionResult GiayCollections(int? page)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            return View(db.Giays.Where(t => t.Collection == true).OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
        }
    }
}