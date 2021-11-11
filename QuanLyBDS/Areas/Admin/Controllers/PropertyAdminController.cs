using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Areas.Admin.Middleware;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    [LoginVerification]
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
            //ViewBag.InstallmentRate = "7,99";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Property property, HttpPostedFileBase imgfile , HttpPostedFileBase[] photos)
        {
            if (ModelState.IsValid)
            {
                String value = "";
                var data = new Property();
                if (photos != null)
                {
                    List<string> files = new List<string>();
                    foreach (var photo in photos)
                    {
                        photo.SaveAs(Server.MapPath("~/Images/" + photo.FileName));
                        files.Add(photo.FileName);
                        value += photo.FileName + ",";
                    }
                    data.Album = value;

                }
                string path = uploadimage(imgfile);
                //data.Property_Code = property.Property_Code;
                data.Property_Name = property.Property_Name;
                data.Description = property.Description;
                data.Address = property.Address;
                data.Area = property.Area;
                data.Bed_Room = property.Bed_Room;
                data.Bath_Room = property.Bath_Room;
                data.Price = property.Price;
                data.Installment_Rate = 7.99;
                data.Avatar = path;

                data.District_ID = property.District_ID;
                data.Property_Status_ID = property.Property_Status_ID;
                data.Property_Type_ID = property.Property_Type_ID;
                model.Properties.Add(data);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyTypeList = model.Property_Type.OrderByDescending(x => x.ID).ToList();
            ViewBag.DistrictList = model.Districts.OrderByDescending(x => x.ID).ToList();
            ViewBag.PropertyStatusList = model.Property_Status.OrderByDescending(x => x.ID).ToList();
            return View();
           
        }


        public string uploadimage(HttpPostedFileBase file)

        {

            string path = "-1";
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    { 
                        path = Server.MapPath("~/Images/" + System.IO.Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
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
            String[] dataImage = property.Album == null ? null :property.Album.Split(',');
            ViewBag.arrIma = dataImage;

            ViewBag.PropertyTypeList = model.Property_Type.OrderByDescending(x => x.ID).ToList();
            ViewBag.DistrictList = model.Districts.OrderByDescending(x => x.ID).ToList();
            ViewBag.PropertyStatusList = model.Property_Status.OrderByDescending(x => x.ID).ToList();
            //ViewBag.InstallmentRate = "7,99";
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Property property, HttpPostedFileBase imgfile, HttpPostedFileBase[] photos)
        {
            if (ModelState.IsValid)
            {
                var data = model.Properties.FirstOrDefault(x => x.ID == id);
                string bfValue = data.Album;
                String ava = imgfile == null ? data.Avatar : uploadimage(imgfile);
                string value = "";
                if (photos[0] != null)
                {
                    List<string> files = new List<string>();
                    foreach (var photo in photos)
                    {
                        photo.SaveAs(Server.MapPath("~/Images/" + photo.FileName));
                        files.Add(photo.FileName);
                        value += photo.FileName + ",";
                    }
                    data.Album = value;

                }
                else
                {
                    data.Album = bfValue;
                }
                //data.Property_Code = property.Property_Code;
                data.Property_Name = property.Property_Name;
                data.Description = property.Description;
                data.Address = property.Address;
                data.Area = property.Area;
                data.Bed_Room = property.Bed_Room;
                data.Bath_Room = property.Bath_Room;
                data.Price = property.Price;
                data.Installment_Rate = 7.99;
                data.Avatar = ava;
                data.District_ID = property.District_ID;
                data.Property_Status_ID = property.Property_Status_ID;
                data.Property_Type_ID = property.Property_Type_ID;

                model.Entry(data).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyTypeList = model.Property_Type.OrderByDescending(x => x.ID).ToList();
            ViewBag.DistrictList = model.Districts.OrderByDescending(x => x.ID).ToList();
            ViewBag.PropertyStatusList = model.Property_Status.OrderByDescending(x => x.ID).ToList();
            return View();
           
        }
    }
}