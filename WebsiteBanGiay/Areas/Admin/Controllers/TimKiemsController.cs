using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiay.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanGiay.Areas.Admin.Controllers
{
    public class TimKiemsController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: Admin/TimKiems
        public ActionResult Index(int? page, string tukhoa)
        {
            int num = 15;
            int pagenum = (page ?? 1);
            if(tukhoa != null)
            {
                Session["tkad"] = tukhoa;
            }
            string tk = Session["tkad"].ToString();
            List<Giay> giay = db.Giays.Where(t => t.TenGiay.Contains(tk)).ToList();
            return View(giay.OrderBy(t => t.TenGiay).ToPagedList(pagenum, num));
        }
    }
}