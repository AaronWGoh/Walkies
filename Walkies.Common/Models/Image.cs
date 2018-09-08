using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Walkies.Admin.Models
{
    public class Image
    {
        public String FileType { get; set; }
        public Guid MetaFileId { get; set; }
        public String OriginalFileName { get; set; }
        public String StoredFileName { get; set; }
        public String Extension { get; set; }
        public String StorageUri { get; set; }
        public String Description { get; set; }

        public Guid ShelterFileId { get; set; }
        public Guid ShelterId { get; set; }

        public Guid DogFileId { get; set; }
        public Guid DogId { get; set; }

        public int SortOrder { get; set; }
    }
}
