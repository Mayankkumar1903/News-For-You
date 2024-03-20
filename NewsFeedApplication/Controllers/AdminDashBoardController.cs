using NewsFeedApplication.BLL;
using NewsFeedApplication.Models;
using NewsFeedApplication.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsFeedApplication.Helper;

namespace NewsFeedApplication.Controllers
{
    [CustomAuth]
    public class AdminDashBoardController : Controller
    {
        // GET: AdminDashBoard
        public ActionResult Index()
        {
            var sessionModel = SessionState.SessionInfo;
            ViewBag.SessionModel = sessionModel;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewsCategory(string categoryName)
        {
            bool isCategoryAdded = AdminDashBoardBLL.AddNewsCategory(categoryName);
            return Json(new { success = isCategoryAdded }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewsAgency(string agencyName, string agencyUrl)
        {
            bool isAgencyAdded = AdminDashBoardBLL.AddNewsAgency(agencyName, agencyUrl);
            return Json(new { success = isAgencyAdded }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRssFeedForNewsCategory(CategoryAgencyRssFeedModel agencyRssFeedModel)
        {
            bool isRssFeedUrlSaved = AdminDashBoardBLL.AddRssFeedForAgencyCategory(agencyRssFeedModel);
            return Json(new { success = isRssFeedUrlSaved }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAllNewsFromTable()
        {
            bool isTableTruncated = AdminDashBoardBLL.DeleteAllNewsTable();
            return Json(new { success = isTableTruncated }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateNewsClickCountReport(DateTime? selectedDateFrom, DateTime? selectedDateTo)
        {
            if (selectedDateFrom == null || selectedDateTo == null)
            {
                return View("Error"); 
            }
            var startDate = selectedDateFrom.Value.Date;
            var endDate = selectedDateTo.Value.Date;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            var generatedReport = AdminDashBoardBLL.GetNewsClickCountReport(startDate, endDate);
            return View(generatedReport);
        }

    }
}