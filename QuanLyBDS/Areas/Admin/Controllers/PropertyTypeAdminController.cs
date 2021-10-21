using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    public class PropertyTypeAdminController : Controller
    {
        AD25Team19Entities model = new AD25Team19Entities();
        // GET: Admin/PropertyTypeAdmin
        public ActionResult Index()
        {
            var pt = model.Property_Type.ToList();
            return View(pt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Property_Type pt)
        {
            var ptype = new Property_Type();
            ptype.Property_Type_Name = pt.Property_Type_Name;
            ptype.Property_Amount = pt.Property_Amount;

            model.Property_Type.Add(pt);
            model.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Property_Type pt = model.Property_Type.Find(id);
            if (pt == null)
            {
                return HttpNotFound();
            }
            return View(pt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property_Type pt = model.Property_Type.Find(id);
            model.Property_Type.Remove(pt);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Property_Type pt = model.Property_Type.Find(id);
            if (pt == null)
            {
                return HttpNotFound();
            }
            return View(pt);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int id, Property_Type pt)
        {
            var data = model.Property_Type.Find(id);
            data.ID = pt.ID;
            data.Property_Type_Name = pt.Property_Type_Name;
            data.Property_Amount = pt.Property_Amount;
            model.Entry(data).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}