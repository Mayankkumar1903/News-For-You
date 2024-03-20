using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Models
{
    public class NewsClickReportModel
    {
        public string Agency { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int ClickCount { get; set; }
    }
}
