using NewsFeedApplication.Models;
using NewsFeedApplication.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.DAL
{
    public class AdminDashBoardDAL
    {
        public static bool AddNewsCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return false;
            }

            bool isCategoryAdded = false;
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    var newCategory = new Category
                    {
                        CategoryTitle = categoryName,
                    };

                    context.Categories.Add(newCategory);
                    int affectedRows = context.SaveChanges();
                    if (affectedRows > 0)
                    {
                        isCategoryAdded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
            }
            return isCategoryAdded;
        }

        public static bool AddNewsAgency(string agencyName,string agencyUrl)
        {
            bool isAgencyAdded = false;
            if (string.IsNullOrWhiteSpace(agencyName) || string.IsNullOrWhiteSpace(agencyUrl))
            {
                return false;
            }

            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var newAgency = new Agency
                    {
                        AgencyName = agencyName,
                        AgencyLogoPath = agencyUrl
                    };

                    context.Agencies.Add(newAgency);
                    int affectedRows = context.SaveChanges();
                    if(affectedRows > 0)
                    {
                        isAgencyAdded = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }
            return isAgencyAdded;
        }

        public static bool InsertRssFeedForAgencyCategory(CategoryAgencyRssFeedModel agencyRssFeedModel)
        {
            bool isRssFeedAdded = false;
            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var existingFeed = context.AgencyFeeds
                              .FirstOrDefault(af => af.AgencyId == agencyRssFeedModel.AgencyId && af.CategoryId == agencyRssFeedModel.CategoryId);

                    if (existingFeed != null)
                    {
                        existingFeed.AgencyFeedUrl = agencyRssFeedModel.RssFeedUrl;
                    }
                    else
                    {
                        var agencyFeed = new AgencyFeed
                        {
                            AgencyId = agencyRssFeedModel.AgencyId,
                            CategoryId = agencyRssFeedModel.CategoryId,
                            AgencyFeedUrl = agencyRssFeedModel.RssFeedUrl,
                        };

                        context.AgencyFeeds.Add(agencyFeed);
                    }
                    int affectedRows = context.SaveChanges();
                    if (affectedRows > 0)
                    {
                        isRssFeedAdded = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }
            return isRssFeedAdded;
        }

        public static bool DeleteAllNewsTable()
        {
            bool isAllNewsDeleted = false;
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM News");
                    int affectedRows = context.SaveChanges();
                    isAllNewsDeleted = true;
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }
            return isAllNewsDeleted;
        }

        public static List<NewsClickReportModel> GetNewsClickCountReport(DateTime selectedDateFrom ,DateTime selectedDateTo)
        {
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    var report = context.News
                        .Where(n => n.NewsPublishDateTime >= selectedDateFrom && n.NewsPublishDateTime <= selectedDateTo)
                        .OrderByDescending(n => n.ClickCount)
                        .Select(n => new NewsClickReportModel
                        {
                            Agency = n.Agency.AgencyName, //  Agency is a navigation property in News
                            Category = n.Category.CategoryTitle, // Category is a navigation property in News
                            Title = n.NewsTitle,
                            ClickCount = (int)n.ClickCount
                        })
                        .ToList();

                    return report;
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
                return new List<NewsClickReportModel>();
            }
        }

    }
}
