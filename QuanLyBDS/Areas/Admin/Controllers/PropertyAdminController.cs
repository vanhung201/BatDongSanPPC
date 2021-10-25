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
            List<Property_Type> pt = model.Property_Type.ToList();
            SelectList ptList = new SelectList(pt, "ID", "PROPERTY_TYPE_NAME");
            ViewBag.PropertyTypeList = ptList;

            List<District> district = model.Districts.ToList();
            SelectList districtList = new SelectList(district, "ID", "DISTRICT_NAME");
            ViewBag.DistrictList = districtList;

            List<Property_Status> pst = model.Property_Status.ToList();
            SelectList pstList = new SelectList(pst, "ID", "PROPERTY_STATUS_NAME");
            ViewBag.PropertyStatusList = pstList;

            return View();
        }
        [HttpPost]
        public ActionResult Create(Property property)
        {
            var pproperty = new Property();

            pproperty.Property_Code = property.Property_Code;
            pproperty.Property_Name = property.Property_Name;
            pproperty.Description = property.Description;
            pproperty.Address = property.Address;
            pproperty.Area = property.Area;
            pproperty.Bed_Room = property.Bed_Room;
            pproperty.Bath_Room = property.Bath_Room;
            pproperty.Price = property.Price;
            pproperty.Installment_Rate = property.Installment_Rate;
            pproperty.Avatar = property.Avatar;
            pproperty.Album = property.Album;
            pproperty.District_ID = property.District_ID;
            pproperty.Property_Status_ID = property.Property_Status_ID;
            pproperty.Property_Type_ID = property.Property_Type_ID;

            model.Properties.Add(property);
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
        public ActionResult Edit(int? id)
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int id, Property property)
        {
            var data = model.Properties.Find(id);

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