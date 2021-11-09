using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.InstallmentRate = "7,99";
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


/*
public partial class Property
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Property()
    {
        this.Full_Contract = new HashSet<Full_Contract>();
        this.Installment_Contract = new HashSet<Installment_Contract>();
        this.Property_Service = new HashSet<Property_Service>();
    }

    public int ID { get; set; }
    public string Property_Code { get; set; }
    [Required(ErrorMessage = "Tên bất động sản không được để trống")]
    [MinLength(5, ErrorMessage = "Chiều dài tối thiểu là 5")]
    [MaxLength(50, ErrorMessage = "Chiều dài tối đa là 50")]
    public string Property_Name { get; set; }
    public int Property_Type_ID { get; set; }
    [Required(ErrorMessage = "Mô tả không được để trống")]
    [MinLength(50, ErrorMessage = "Chiều dài tối thiểu là 50")]
    [MaxLength(200, ErrorMessage = "Chiều dài tối đa là 200")]
    public string Description { get; set; }
    public int District_ID { get; set; }
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Diện tích không được để trống")]
    [Range(50, 200, ErrorMessage = "Diện tích trong khoảng 50-200")]
    public Nullable<int> Area { get; set; }
    [Required(ErrorMessage = "Phải nhập số lượng phòng ngủ")]
    [Range(0, 10, ErrorMessage = "Số lượng phải nhỏ hơn 10")]
    public Nullable<int> Bed_Room { get; set; }
    [Required(ErrorMessage = "Phải nhập số lượng phòng tắm")]
    [Range(0, 10, ErrorMessage = "Số lượng phải nhỏ hơn 10")]
    public Nullable<int> Bath_Room { get; set; }
    [Display(Name = "Giá")]

    [Required(ErrorMessage = "Phải điền giá tiền")]
    [Range(10000000, 10000000000, ErrorMessage = "Giá tiền phải trong khoảng 10.000.000 đến 10.000.000.000")]
    public Nullable<decimal> Price { get; set; }
    public Nullable<double> Installment_Rate { get; set; }
    public int Property_Status_ID { get; set; }
    [DisplayName("Upload File")]
    public string Avatar { get; set; }
    public string Album { get; set; }
    public HttpPostedFileBase ImageFile { get; set; }
    public HttpPostedFileBase ImageAlbum { get; set; }

    public virtual District District { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Full_Contract> Full_Contract { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Installment_Contract> Installment_Contract { get; set; }
    public virtual Property_Status Property_Status { get; set; }
    public virtual Property_Type Property_Type { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Property_Service> Property_Service { get; set; }
}*/