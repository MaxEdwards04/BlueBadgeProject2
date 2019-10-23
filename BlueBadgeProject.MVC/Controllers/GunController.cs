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

        public ActionResult Edit(int id)
        {
            var service = CreateGunService();
            var detail = service.GetGunById(id);
            var model =
                new GunEdit
                {
                    GunId = detail.GunId,
                    Name = detail.Name,
                    Description = detail.Description,
                    IsPrimary = detail.IsPrimary
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GunEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.GunId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateGunService();

            if (service.UpdateGun(model))
            {
                TempData["SaveResult"] = "Your gun was updated";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your gun could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGunService();
            var model = svc.GetGunById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGunService();

            service.DeleteGun(id);

            TempData["SaveResult"] = "Your gun was deleted";

            return RedirectToAction("Index");
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