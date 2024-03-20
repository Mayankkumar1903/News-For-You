using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Models
{
    public class SessionModel
    {
        public string currentUserEmail {  get; set; }

        public bool isUserAdmin {  get; set; }
    }
}
