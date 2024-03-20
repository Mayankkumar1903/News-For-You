using NewsFeedApplication.DAL;
using NewsFeedApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.BLL
{
    public class AdminDashBoardBLL
    {
        public static bool AddNewsCategory(string categoryName)
        {
            return AdminDashBoardDAL.AddNewsCategory(categoryName); 
        }

        public static bool AddNewsAgency(string agencyName,string agencyUrl)
        {
            return AdminDashBoardDAL.AddNewsAgency(agencyName, agencyUrl);
        }

        public static bool AddRssFeedForAgencyCategory(CategoryAgencyRssFeedModel agencyRssFeedModel)
        {
            return AdminDashBoardDAL.InsertRssFeedForAgencyCategory(agencyRssFeedModel);
        }

        public static bool DeleteAllNewsTable()
        {
            return AdminDashBoardDAL.DeleteAllNewsTable();
        }

        public static List<NewsClickReportModel> GetNewsClickCountReport(DateTime selectedDateFrom ,DateTime selectedDateTo)
        {
            return AdminDashBoardDAL.GetNewsClickCountReport(selectedDateFrom,selectedDateTo);
        }
    }
}
