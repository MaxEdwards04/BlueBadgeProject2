using Microsoft.AspNet.Identity;
using Project.Models;
using Project.Services;
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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GunService(userId);
            var model = service.GetGuns();

            return View(model);
        }

        public ActionResult Create()
        {
            if (!ModelState.IsValid)
            { 

            }
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GunCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateGunService();
            if (service.CreateGun(model))
            {
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateGunService();
            var model = svc.GetGunById(id);

            return View(model);
        }

        private GunService NewMethod()
        {
            GunService service = CreateGunService();
            return service;
        }

        private GunService CreateGunService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GunService(userId);
            return service;
        }
    }
}