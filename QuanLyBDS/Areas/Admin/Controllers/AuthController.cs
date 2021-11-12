using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBDS.Models;

namespace QuanLyBDS.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        AD25Team19Entities model = new AD25Team19Entities();

        // GET: Admin/Auth
        public ActionResult Login()
        {
            Session["username-incorrect"] = false;
            Session["password-incorrect"] = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = model.Accounts.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null)
            {
                if (user.Password.Equals(password))
                {
                    Session["user-fullname"] = user.Full_Name;
                    Session["user-id"] = user.ID;
                    Session["user-role"] = user.Role;
                    return RedirectToAction("Index", "PropertyTypeAdmin");
                }
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }
            }
            else
            {
                Session["username-incorrect"] = true;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["user-fullname"] = null;
            Session["user-id"] = null;
            Session["user-role"] = null;

            return RedirectToAction("Login");
        }
    }
}