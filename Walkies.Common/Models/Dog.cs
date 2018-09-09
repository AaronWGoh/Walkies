using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Walkies.Common.Models
{
    public class Dog
    {
        public Guid DogId { get; set; }
        public Guid ShelterId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Breed { get; set; }
        public DateTime AvailableDate { get; set; }
        public Guid PrimaryImageFileId { get; set; }
        public bool IsPublic { get; set; }
    }
}
