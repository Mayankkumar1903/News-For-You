using NewsFeedApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsFeedApplication.Utils;

namespace NewsFeedApplication.DAL
{
    public class UserDetailsDAL
    {
        public static bool IsEmailUnique(string email)
        {
            bool isUnique = true;
            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var user = context.Users.FirstOrDefault(x => x.Email == email);
                    if (user != null)
                    {
                        isUnique = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);   
            }
            return isUnique;
        }

        public static bool RegisterUser(UserModel user)
        {
            bool isUserSaved = false;
            try
            {
                using (var context = new NewsFeedDBEntities())
                {
                    var newUser = new User()
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                    };
                    context.Users.Add(newUser);
                    isUserSaved = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
            }
            return isUserSaved;
        }

        public static bool ValidateUser(string email,string password)
        {
            bool isValidUser = false;
            try
            {
                using(var context = new NewsFeedDBEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null && user.Password == password)
                    {
                        isValidUser = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }
            return isValidUser;
        }
    }
}
