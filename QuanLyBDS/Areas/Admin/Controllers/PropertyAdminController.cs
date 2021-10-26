using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    public class PropertyAdminController : Controller
    {
        AD25Team19Entities model = new AD25Team19Entities();
        // GET: Admin/Property
        public ActionResult Index()
        {
            var properties = model.Properties.ToList();
            return View(properties);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PropertyTypeList = model.Property_Type.OrderByDescending(x => x.ID).ToList();
            ViewBag.DistrictList = model.Districts.OrderByDescending(x => x.ID).ToList();
            ViewBag.PropertyStatusList = model.Property_Status.OrderByDescending(x => x.ID).ToList();
            ViewBag.InstallmentRate = "7,99";

            return View();
        }
        [HttpPost]
        public ActionResult Create(Property property)
        {
            var data = new Property();

            data.Property_Code = property.Property_Code;
            data.Property_Name = property.Property_Name;
            data.Description = property.Description;
            data.Address = property.Address;
            data.Area = property.Area;
            data.Bed_Room = property.Bed_Room;
            data.Bath_Room = property.Bath_Room;
            data.Price = property.Price;
            data.Installment_Rate = property.Installment_Rate;
            data.Avatar = property.Avatar;
            data.Album = property.Album;
            data.District_ID = property.District_ID;
            data.Property_Status_ID = property.Property_Status_ID;
            data.Property_Type_ID = property.Property_Type_ID;

            model.Properties.Add(data);
            model.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Property property = model.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = model.Properties.Find(id);
            model.Properties.Remove(property);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);

            ViewBag.PropertyTypeList = model.Property_Type.OrderByDescending(x => x.ID).ToList();
            ViewBag.DistrictList = model.Districts.OrderByDescending(x => x.ID).ToList();
            ViewBag.PropertyStatusList = model.Property_Status.OrderByDescending(x => x.ID).ToList();

            return View(property);
        }

        [HttpPost]
        public ActionResult Edit(int id, Property property)
        {
            var data = model.Properties.FirstOrDefault(x => x.ID == id);

            data.Property_Code = property.Property_Code;
            data.Property_Name = property.Property_Name;
            data.Description = property.Description;
            data.Address = property.Address;
            data.Area = property.Area;
            data.Bed_Room = property.Bed_Room;
            data.Bath_Room = property.Bath_Room;
            data.Price = property.Price;
            data.Installment_Rate = property.Installment_Rate;
            data.Avatar = property.Avatar;
            data.Album = property.Album;
            data.District_ID = property.District_ID;
            data.Property_Status_ID = property.Property_Status_ID;
            data.Property_Type_ID = property.Property_Type_ID;
        
            model.Entry(data).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}