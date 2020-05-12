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
    }
}
