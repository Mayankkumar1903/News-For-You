using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Utils
{
    public class AdminUtility
    {
        public static List<string> AdminEmails { get; private set; }

        static AdminUtility()
        {
            AdminEmails = new List<string>
            {
                "admin@gmail.com",  
                //"admin2@gmail.com",
            };
        }
    }
}
