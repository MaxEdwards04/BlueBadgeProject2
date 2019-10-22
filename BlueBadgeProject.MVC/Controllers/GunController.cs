using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeProject.MVC.Controllers
{
    [Authorize]
    public class GunController : Controller
    {
        // GET: Gun
        public ActionResult Index()
        {
            var model = new GunListItem[0];
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Create(GunCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}