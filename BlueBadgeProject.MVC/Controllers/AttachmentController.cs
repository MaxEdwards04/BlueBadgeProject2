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
    public class AttachmentController : Controller
    {
        // GET: Attachment
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AttachmentService(userId);

            var model = service.GetAttachments();

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
        public ActionResult Create(AttachmentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAttachmentService();

            if (service.CreateAttachment(model))
            {
                ViewBag.SaveResult = "Your nattachment was created";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateAttachmentService();
            var model = svc.GetAttachmentById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAttachmentService();
            var detail = service.GetAttachmentById(id);
            var model =
                new AttachmentEdit
                {
                    AttachmentId = detail.AttachmentId,
                    Name = detail.Name,
                    Description = detail.Description,
                    IsPrimary = detail.IsPrimary,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AttachmentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AttachmentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAttachmentService();

            if (service.UpdateAttachment(model))
            {
                TempData["SaveResult"] = "Your attachment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your attachment could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateAttachmentService();

            service.DeleteAttachment(id);

            TempData["SaveResult"] = "Your attachment was deleted";

            return RedirectToAction("Index");
        }

        private AttachmentService CreateAttachmentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AttachmentService(userId);
            return service;
        }
    }
}