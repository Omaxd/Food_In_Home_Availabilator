using Model.Interfaces;
using System;

namespace Model
{
    public class User : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsValid
        {
            get
            {
                if (Id != default || Name != default)
                    return true;

                return false;
            }
        }

        public User() { }

        public User(User other)
        {
            if (other == null)
                return;

            this.Id = other.Id;
            this.Name = other.Name;
            this.Login = other.Login;
            this.Password = other.Password;
            this.IsDeleted = other.IsDeleted;
        }
    }
}
