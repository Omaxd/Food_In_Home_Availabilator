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

    }
}
