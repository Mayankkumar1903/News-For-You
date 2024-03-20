using Microsoft.Ajax.Utilities;
using NewsFeedApplication.BLL;
using NewsFeedApplication.Models;
using NewsFeedApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApplication.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            var sessionModel = SessionState.SessionInfo;
            ViewBag.SessionModel = sessionModel;
            return View();
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            var categories = DashBoardBLL.GetCategoriesList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAgencies()
        {
            var agencies = DashBoardBLL.GetAgenciesList();
            return Json(agencies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetNewsByCategoryAndAgency(List<int> categoryIds, int agencyId)
        {
            var newsItems = DashBoardBLL.GetNewsItemsByCategoryAndAgency(categoryIds, agencyId);
            return Json(newsItems, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetNewsByAgency(int agencyId)
        {
            var newsItems = DashBoardBLL.GetNewsByAgency(agencyId);
            return Json(newsItems);
        }

        [HttpPost]
        public ActionResult IncrementClickCount(int newsId)
        {
            var isCountIncremented = DashBoardBLL.IncrementClickCount(newsId);
            if (isCountIncremented)
            {
                return Json(new { success = true, message = "Click count incremented successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to increment click count." });
            }
        }
    }
}
