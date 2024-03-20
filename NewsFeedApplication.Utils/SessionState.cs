using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NewsFeedApplication.Models;

namespace NewsFeedApplication.Utils
{
    public class SessionState
    {

        public static SessionModel SessionInfo 
        {
            get
            {
                return HttpContext.Current.Session["SessionObject"] as SessionModel;
            }
            set
            {
                HttpContext.Current.Session["SessionObject"] = value;
            }
        }
    }
}
