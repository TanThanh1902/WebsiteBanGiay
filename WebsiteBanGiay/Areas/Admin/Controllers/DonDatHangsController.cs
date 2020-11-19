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
    public class DonDatHangsController : Controller
    {
        private GiayEntities db = new GiayEntities();

        // GET: Admin/DonDatHangs
        public ActionResult Index(int? page)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            var donDatHangs = db.DonDatHangs.Include(d => d.KhachHang);
            return View(donDatHangs.OrderBy(t => t.NgayDH).ToPagedList(pagenum, num));
        }

        // GET: Admin/DonDatHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang");
            return View();
        }

        // POST: Admin/DonDatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaKH,NgayDH,NgayGH,DiaChiNhan,SDTNhan,HTGH,HTTT,TinhTrangGH,TongThanhToan")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", donDatHang.MaKH);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", donDatHang.MaKH);
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaKH,NgayDH,NgayGH,DiaChiNhan,SDTNhan,HTGH,HTTT,TinhTrangGH,TongThanhToan")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", donDatHang.MaKH);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
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
