using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class AgencyModel
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }

    }

    public class NewsFeedModel
    {
        public int NewsFeedId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
    }

    public class CategoryAgencyRssFeedModel
    {
        public int AgencyId { get; set; }
        public int CategoryId { get; set; }
        public string RssFeedUrl { get; set; }
    }

    public class NewsClickReport
    {
        public string Agency { get; set; }
        public string Category {  get; set; }
        public string Title { get; set; }
        public int ClickCount { get; set; }
    }
}
