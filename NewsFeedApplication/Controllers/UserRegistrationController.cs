using NewsFeedApplication.BLL;
using NewsFeedApplication.Helper;
using NewsFeedApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApplication.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        [CustomAuth]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUniqueEmail(string email)
        {
            bool isEmailUnique = UserDetailsBLL.IsEmailUnique(email);
            return Json(new { isUnique = isEmailUnique });
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            var userModel = new UserModel()
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            bool isRegistered = UserDetailsBLL.RegisterUser(userModel);
            return Json(new { success = isRegistered });
        }
    }
}