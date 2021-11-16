using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    public class Full_ContractController : Controller
    {
        AD25Team19Entities model = new AD25Team19Entities();

        // GET: Admin/Full_Contract
        public ActionResult Index()
        {
            var fc = model.Full_Contract.ToList();
            return View(fc);
        }

        public ActionResult Print(int id)
        {
            var printData = model.Full_Contract.FirstOrDefault(x => x.ID == id);
            return View(printData);
        }
    }
}