using NewsFeedApplication.Models;
using NewsFeedApplication.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NewsFeedApplication.DAL
{
    public class DashBoardDAL
    {
        public static List<CategoryModel> GetCategoriesList()
        {
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    var categories = context.Categories
                        .Select(c => new
                        {
                            CategoryId = c.CategoryId,
                            CategoryTitle = c.CategoryTitle
                        })
                        .ToList();

                    var categoryList = categories.Select(c => new CategoryModel
                    {
                        CategoryId = c.CategoryId,
                        CategoryTitle = c.CategoryTitle
                    }).ToList();

                    return categoryList;
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
                return new List<CategoryModel>(); 
            }
        }

        public static List<AgencyModel> GetAgenciesList()
        {
            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var agencies = context.Agencies
                        .Select(c => new
                        {
                            AgencyId = c.AgencyId,
                            AgencyName = c.AgencyName
                        })
                        .ToList();

                    var agencyList = agencies.Select(c => new AgencyModel
                    {
                        AgencyId = c.AgencyId,
                        AgencyName = c.AgencyName
                    }).ToList();

                    return agencyList;
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
                return new List<AgencyModel>();
            }
        }

        public static List<AgencyFeed> GetAgencyFeedsByAgencyAndCategory(int agencyId, int categoryId)
        {
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    return context.AgencyFeeds
                        .Where(af => af.AgencyId == agencyId && af.CategoryId == categoryId)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
                return new List<AgencyFeed>();
            }
        }

        public static List<NewsFeedModel> ParseRssFeed(string rssFeedUrl)
        {
            List<NewsFeedModel> newsItems = new List<NewsFeedModel>();
            try
            {
                using (System.Net.WebClient wclient = new System.Net.WebClient())
                {
                    string rssData = wclient.DownloadString(rssFeedUrl);
                    XDocument xml = XDocument.Parse(rssData);

                    var rssFeedData = (from x in xml.Descendants("item")
                                       select new NewsFeedModel
                                       {
                                           Title = (string)x.Element("title"),
                                           Link = (string)x.Element("link"),
                                           Description = (string)x.Element("description"),
                                           PubDate = (string)x.Element("pubDate")
                                       }).ToList();

                    newsItems.AddRange(rssFeedData);
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }

            return newsItems;
        }

        public static void StoreNewsItems(int categoryId, int agencyId)
        {
            var agencyFeeds = GetAgencyFeedsByAgencyAndCategory(agencyId, categoryId);
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    foreach (var agencyFeed in agencyFeeds)
                    {
                        var items = ParseRssFeed(agencyFeed.AgencyFeedUrl);
                        foreach (var item in items)
                        {
                            var existingNews = context.News.FirstOrDefault(n => n.NewsLink == item.Link);
                            if (existingNews == null)
                            {
                                var news = new News
                                {
                                    NewsTitle = item.Title,
                                    NewsDescription = item.Description,
                                    NewsPublishDateTime = DateTime.TryParse(item.PubDate, out DateTime pubDate) ? pubDate : DateTime.Now,
                                    NewsLink = item.Link,
                                    ClickCount = 0,
                                    CategoryId = categoryId,
                                    AgencyId = agencyId
                                };
                                context.News.Add(news);
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
            }
        }

        public static List<NewsFeedModel> GetNewsItemsByCategoryAndAgency(List<int> categoryIds, int agencyId)
        {
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    var newsItems = context.News
                        .Where(n => categoryIds.Contains(n.CategoryId) && n.AgencyId == agencyId)
                        .OrderByDescending(n => n.NewsPublishDateTime)
                        .Select(n => new NewsFeedModel
                        {
                            NewsFeedId = n.NewsId,
                            Title = n.NewsTitle,
                            Link = n.NewsLink,
                            Description = n.NewsDescription,
                            PubDate = n.NewsPublishDateTime.ToString()
                        })
                        .ToList();

                    return newsItems;
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
                return new List<NewsFeedModel>();
            }
        }

        public static List<NewsFeedModel> GetNewsItemsByAgency(int agencyId)
        {
            List<NewsFeedModel> allNewsItems = new List<NewsFeedModel>();

            try
            {
                var categories = GetCategoriesList();
                foreach (var category in categories)
                {
                    StoreNewsItems(category.CategoryId, agencyId);
                }
                using (var context = new NewsFeedDBEntities())
                {
                    allNewsItems = context.News
                        .Where(n => n.AgencyId == agencyId)
                        .OrderByDescending(n => n.NewsPublishDateTime)
                        .Select(n => new NewsFeedModel
                        {
                            NewsFeedId = n.NewsId,
                            Title = n.NewsTitle,
                            Link = n.NewsLink,
                            Description = n.NewsDescription,
                            PubDate = n.NewsPublishDateTime.ToString() 
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
            }

            return allNewsItems;
        }

        public static bool IncrementClickCount(int newsId)
        {
            bool isIncremented = false;
            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var newsItem = context.News.FirstOrDefault(n => n.NewsId == newsId);
                    if(newsItem != null)
                    {
                        newsItem.ClickCount++;
                        context.SaveChanges();
                        isIncremented = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }
            return isIncremented;
        }
    }
}
