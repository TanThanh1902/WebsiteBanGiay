using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace WebsiteBanGiay.Controllers
{
    public class BinhLuanController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: BinhLuan
        public ActionResult Index(int? page, int? id)
        {
            int num = 10;
            int pagenum = (page ?? 1);
            var binhluan = db.BinhLuans.Where(t => t.MaGiay == id).Include(t => t.TraLois).OrderByDescending(t => t.NgayDang).ToList();
            return View(binhluan.ToPagedList(pagenum, num));
        }
        [HttpPost]
        public ActionResult ThemBinhLuan(BinhLuan bl)
        {
            KhachHang info = (KhachHang)Session["kh"];
            if(info == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            bl.MaNguoiDung = info.MaKhachHang;
            bl.MaGiay = Int32.Parse(Session["magiay"].ToString());
            bl.NgayDang = DateTime.Now;
            db.BinhLuans.Add(bl);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        // Create new Reply
        [HttpPost]
        public ActionResult ThemTraLoi(TraLoi tl)
        {
            KhachHang info = (KhachHang)Session["kh"];
            if (info == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            tl.MaNguoiDung = info.MaKhachHang;
            tl.NgayDang = DateTime.Now;
            db.TraLois.Add(tl);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}