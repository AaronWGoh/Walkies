using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Walkies.Cross
{
    [Table("Shelter")]
    public class DogProfile
    {
        public string name = "Doggo";
        public int Zipcode = 0;
        public string Address = "";
    }

    public class Profile
    {
        public string username = "";
    }
}
