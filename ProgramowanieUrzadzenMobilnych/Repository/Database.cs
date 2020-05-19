using Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository
{
    public class Database
    {
        public IList<User> Users { get; set; }
        public IList<UserProductAction> UserProductActions { get; set; }
        public IList<Product> Products { get; set; }

        public Database()
        {
            Users = new List<User>();
            UserProductActions = new List<UserProductAction>();
            Products = new List<Product>();

            User firstUser = new User()
            {
                Login = "admin",
                Name = "Szymon",
                Password = "1234"
            };

            Users.Add(firstUser);
        }
    }
}
