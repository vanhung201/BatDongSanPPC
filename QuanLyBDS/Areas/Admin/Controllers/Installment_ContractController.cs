using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    public class Installment_ContractController : Controller
    {
        AD25Team19Entities model = new AD25Team19Entities();

        // GET: Admin/Installment_Contract
        public ActionResult Index()
        {
            var ic = model.Installment_Contract.ToList();
            return View(ic);
        }

        public ActionResult Print(int id)
        {
            var printData = model.Installment_Contract.FirstOrDefault(x => x.ID == id);
            return View(printData);
        }
    }
}