using NewsFeedApplication.DAL;
using NewsFeedApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.BLL
{
    public class UserDetailsBLL
    {
        public static bool IsEmailUnique(string email)
        {
            return UserDetailsDAL.IsEmailUnique(email);
        }

        public static bool RegisterUser(UserModel user)
        {
            return UserDetailsDAL.RegisterUser(user);
        }

        public static bool ValidateUser(string email,string password)
        {
            return UserDetailsDAL.ValidateUser(email,password);
        }
    }
}
