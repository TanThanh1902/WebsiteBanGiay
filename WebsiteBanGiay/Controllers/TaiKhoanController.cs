using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;

namespace WebsiteBanGiay.Controllers
{
    public class TaiKhoanController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangKy(KhachHang kh)
        {
            if(ModelState.IsValid)
            {
                kh.Quyen = 0;
                kh.Token = Guid.NewGuid().ToString();
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                Session["kh"] = kh;
            }
            return Redirect("/HienThi/Index");
        }
        [HttpPost]
        public JsonResult ktraTK(string Email)
        {
            bool isExists = db.KhachHangs.FirstOrDefault(t => t.Email == Email) != null;
            return Json(!isExists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTK = f["txtTaiKhoan"].ToString();
            string sMK = f["txtMatKhau"].ToString();
            KhachHang kh = db.KhachHangs.FirstOrDefault(t => t.Email == sTK && t.MatKhau == sMK);
            Admin ad = db.Admins.FirstOrDefault(t => t.TaiKhoanAdmin == sTK && t.MatKhauAdmin == sMK);
            if(ad != null)
            {
                Session["ad"] = ad;
                return Redirect("/Admin/Admin/Index");
            }
            else if(kh != null)
            {
                Session["kh"] = kh;
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.TB = "Tai khoan hoac mat khau khong chinh xac";
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult DangXuat()
        {
            Session["kh"] = null;
            return Redirect("/HienThi/Index");
        }
    }
}