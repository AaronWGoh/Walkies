using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Walkies.Common.Models
{
    public class Shelter
    {
        public Guid ShelterId { get; set; }
        public String Name { get; set; }
        public String Street1 { get; set; }
        public String Street2 { get; set; }
        public String City { get; set; }
        public String StateCode { get; set; }
        public String Zip { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        public String Email { get; set; }
    }
}
