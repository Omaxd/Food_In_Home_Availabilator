using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : Repository<User>
    {
        public User GetById(int userId)
        {
            return GetById(userId, database.Users);
        }

    }
}
