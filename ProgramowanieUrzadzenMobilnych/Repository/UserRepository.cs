using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : Repository
    {
        /// <summary>
        /// Getting user from database by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int userId)
        {
            User user = database.Users
                .Where(u => u.Id == userId)
                .Where(u => !u.IsDeleted)
                .FirstOrDefault();

            return user;
        }
    }
}
