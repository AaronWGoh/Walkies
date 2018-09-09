using System;
using System.Collections.Generic;
using System.Text;

namespace Walkies.Common.Models
{
    public class DisplayData
    {
        public IEnumerable<Shelter> Shelters { get; set; }
        public IEnumerable<Dog> Dogs { get; set; }
        public String City { get; set; }
        public String StateCode { get; set; }
    }
}
