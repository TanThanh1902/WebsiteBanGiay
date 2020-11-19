using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;

namespace WebsiteBanGiay.Controllers
{
    public class GioHangController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;
            return View(giohang);
        }
        public ActionResult Themvaogio(int? maGiay)
        {
            if(Session["giohang"] == null)
            {
                Session["giohang"] = new List<GioHangs>();
                Session["slgh"] = "Trống";
            }

            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;

            if(giohang.FirstOrDefault(t => t.maGiay == maGiay) == null)
            {
                Giay giay = db.Giays.Find(maGiay);
                if(Session["slgh"].ToString() == "Trống")
                {
                    Session["slgh"] = 0;
                }
                Session["slgh"] = Int32.Parse(Session["slgh"].ToString()) + 1;
                GioHangs newItem = new GioHangs()
                {
                    maGiay = giay.MaGiay,
                    tenGiay = giay.TenGiay,
                    soLuong = 1,
                    hinhAnh = giay.HinhAnhGiay,
                    donGia = (decimal)giay.GiaTien
                };
                giohang.Add(newItem);
            }
            else
            {
                GioHangs giohg = giohang.FirstOrDefault(t => t.maGiay == maGiay);
                giohg.soLuong++;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public RedirectToRouteResult Suasoluong(int? maGiay, int soluongmoi)
        {
            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;
            if(giohang.FirstOrDefault(t => t.maGiay == maGiay) != null)
            {
                giohang.FirstOrDefault(t => t.maGiay == maGiay).soLuong = soluongmoi;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Xoakhoigio(int? maGiay)
        {
            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;
            if (giohang.FirstOrDefault(t => t.maGiay == maGiay) != null)
            {
                giohang.Remove(giohang.FirstOrDefault(t => t.maGiay == maGiay));
                Session["slgh"] = Int32.Parse(Session["slgh"].ToString()) - 1;
                if(Int32.Parse(Session["slgh"].ToString()) == 0)
                {
                    Session["slgh"] = "Trống";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ThanhToan()
        {
            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;
            KhachHang kh = (KhachHang)Session["kh"];
            DonDatHang dh = new DonDatHang();
            dh.KhachHang = kh;
            dh.SDTNhan = kh.SoDienThoai;
            dh.DiaChiNhan = kh.DiaChi;
            dh.NgayDH = DateTime.Now;
            var i = giohang;
            dh.TongThanhToan = (decimal)i.Sum(t => t.thanhTien);
            return View(dh);
        }
        [HttpPost]
        public RedirectToRouteResult ThanhToan(DonDatHang dh)
        {
            if(ModelState.IsValid)
            {
                KhachHang kh = (KhachHang)Session["kh"];
                dh.MaKH = kh.MaKhachHang;
                db.DonDatHangs.Add(dh);
                db.SaveChanges();
            }
            return RedirectToAction("DonHang", new { maDH = dh.MaDH });
        }
        public RedirectToRouteResult DonHang(int maDH)
        {
            List<GioHangs> giohang = Session["giohang"] as List<GioHangs>;
            foreach(var i in giohang)
            {
                CTDatHang ctdh = new CTDatHang();
                ctdh.MaDH = maDH;
                ctdh.MaGiay = i.maGiay;
                ctdh.SoLuong = i.soLuong;
                ctdh.DonGia = i.donGia;
                ctdh.ThanhTien = (decimal)i.thanhTien;
                db.CTDatHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["giohang"] = null;
            Session["slgh"] = "Trống";
            return RedirectToAction("Index");
        }
    }
}