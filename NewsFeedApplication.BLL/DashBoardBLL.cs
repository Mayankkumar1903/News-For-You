using NewsFeedApplication.DAL;
using NewsFeedApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.BLL
{
    public class DashBoardBLL
    {
        public static List<CategoryModel> GetCategoriesList()
        {
               return DashBoardDAL.GetCategoriesList();
        }

        public static List<AgencyModel> GetAgenciesList()
        {
                return DashBoardDAL.GetAgenciesList();
        }

        public static List<NewsFeedModel> GetNewsItemsByCategoryAndAgency(List<int> categoryIds, int agencyId)
        {
            return DashBoardDAL.GetNewsItemsByCategoryAndAgency(categoryIds, agencyId);
        }

        public static List<NewsFeedModel> GetNewsByAgency(int agencyId)
        {
            return DashBoardDAL.GetNewsItemsByAgency(agencyId);
        }

        public static bool IncrementClickCount(int newsId)
        {
            return DashBoardDAL.IncrementClickCount(newsId);
        }
    }

}
