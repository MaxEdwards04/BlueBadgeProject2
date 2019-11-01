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
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ClassService(userId);

            var model = service.GetClasses();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateClassService();

            if (service.CreateClass(model))
            {
                ViewBag.SaveResult = "Your class was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Class could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateClassService();
            var model = svc.GetClassById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateClassService();
            var detail = service.GetClassById(id);
            var model =
                new ClassEdit
                {
                    ClassId = detail.ClassId,
                    Name = detail.Name,
                    Description = detail.Description,
                    PrimaryGun = detail.PrimaryGun,
                    PrimaryAttach = detail.PrimaryAttach,
                    SecondaryGun = detail.SecondaryGun,
                    SecondaryAttach = detail.SecondaryAttach
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClassEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ClassId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateClassService();

            if (service.UpdateClass(model))
            {
                TempData["SaveResult"] = "Your class was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your class could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateClassService();
            var model = svc.GetClassById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateClassService();

            service.DeleteClass(id);

            TempData["SaveResult"] = "Your class was deleted";

            return RedirectToAction("Index");
        }

        private ClassService CreateClassService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ClassService(userId);
            return service;
        }
    }
}