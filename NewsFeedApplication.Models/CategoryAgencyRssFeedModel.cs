using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Models
{
    public class CategoryAgencyRssFeedModel
    {
        public int AgencyId { get; set; }
        public int CategoryId { get; set; }
        public string RssFeedUrl { get; set; }
    }
}
