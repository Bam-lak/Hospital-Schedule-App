
namespace Hospital
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PeselNo { get; set; }
        public string Speciality { get; set; }
        public string PwzNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
    }
}
