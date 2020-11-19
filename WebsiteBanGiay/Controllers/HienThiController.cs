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
    public class HienThiController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: HienThi
        public ActionResult Index()
        {
            if(Session["slgh"] == null)
            {
                Session["slgh"] = "Trống";
            }
            return View();
        }
        public ViewResult TimKiemTK(int? page,string tukhoa)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            if(tukhoa != null)
            {
                Session["tukhoa"] = tukhoa;
            }
            string tk = Session["tukhoa"].ToString();
            List<Giay> giay = db.Giays.Where(t => t.TenGiay.Contains(tk)).ToList();
            if(giay.Count != 0)
            {
                ViewBag.tb = "true";
            }
            return View(giay.OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
        }
    }
}