using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanGiay.Areas.Admin.Controllers
{
    public class GiaysController : Controller
    {
        private GiayEntities db = new GiayEntities();

        // GET: Admin/Giays
        public ActionResult Index(int? page)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            var giays = db.Giays.Include(g => g.Admin).Include(g => g.DanhMuc).Include(g => g.PhanLoai);
            return View(giays.OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
        }

        // GET: Admin/Giays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // GET: Admin/Giays/Create
        public ActionResult Create()
        {
            ViewBag.MaAdmin = new SelectList(db.Admins, "MaAdmin", "TaiKhoanAdmin");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "TenPhanLoai");
            return View();
        }

        // POST: Admin/Giays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiay,TenGiay,HinhAnhGiay,SoLuong,MaMau,MaAdmin,GiaTien,KichCo,MaDanhMuc,MaPhanLoai,GiamGia,NgayHangVe,SoDonHang,Phai,Collection")] Giay giay)
        {
            if (ModelState.IsValid)
            {
                db.Giays.Add(giay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaAdmin = new SelectList(db.Admins, "MaAdmin", "TaiKhoanAdmin", giay.MaAdmin);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", giay.MaDanhMuc);
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "TenPhanLoai", giay.MaPhanLoai);
            return View(giay);
        }

        // GET: Admin/Giays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaAdmin = new SelectList(db.Admins, "MaAdmin", "TaiKhoanAdmin", giay.MaAdmin);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", giay.MaDanhMuc);
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "TenPhanLoai", giay.MaPhanLoai);
            return View(giay);
        }

        // POST: Admin/Giays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiay,TenGiay,HinhAnhGiay,SoLuong,MaMau,MaAdmin,GiaTien,KichCo,MaDanhMuc,MaPhanLoai,GiamGia,NgayHangVe,SoDonHang,Phai,Collection")] Giay giay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaAdmin = new SelectList(db.Admins, "MaAdmin", "TaiKhoanAdmin", giay.MaAdmin);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", giay.MaDanhMuc);
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoais, "MaPhanLoai", "TenPhanLoai", giay.MaPhanLoai);
            return View(giay);
        }

        // GET: Admin/Giays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // POST: Admin/Giays/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Giay giay = db.Giays.Find(id);
            db.Giays.Remove(giay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
