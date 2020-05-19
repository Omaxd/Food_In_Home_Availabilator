using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(Database database)
            :base(database)
        {

        }

        public User GetById(int userId)
        {
            return GetById(userId, database.Users);
        }

        public User GetByLoginAndPassword(string login, string password)
        {
            User user = database.Users
                .Where(u => u.Login == login)
                .Where(u => u.Password == password)
                .Where(u => !u.IsDeleted)
                .FirstOrDefault();

            if (user == null)
                return null;

            // Delete password information
            User returnedUser = new User(user);
            returnedUser.Password = string.Empty;

            return returnedUser;
        }

        public bool CheckIfLoginIsAvailable(string login)
        {
            User user = database.Users
                .Where(u => u.Login == login)
                .Where(u => !u.IsDeleted)
                .FirstOrDefault();

            if (user == null)
                return true;

            return false;
        }
    }
}
