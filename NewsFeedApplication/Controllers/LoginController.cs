using NewsFeedApplication.BLL;
using NewsFeedApplication.Helper;
using NewsFeedApplication.Models;
using NewsFeedApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [CustomAuth]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]  
        public ActionResult UserLogin(string email, string password)
        {
            bool isValidUser = UserDetailsBLL.ValidateUser(email, password);
            var adminEmails = AdminUtility.AdminEmails;
            bool checkForAdmin = adminEmails.Contains(email);
            if (isValidUser)
            {
                SessionModel session = new SessionModel
                {
                    currentUserEmail = email,
                    isUserAdmin = checkForAdmin,
                };
                SessionState.SessionInfo = session;
            }
            return Json(new { success = isValidUser });
        }

        [HttpPost]
        public ActionResult Logout()
        {
            SessionState.SessionInfo = null;
            return RedirectToAction("Index", "Login");
        }

    }
}