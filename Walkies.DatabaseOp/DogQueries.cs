using System;
using System.Collections.Generic;
using System.Text;
using Walkies.Common;

namespace Walkies.DatabaseOperations
{
    public class DogQueries
    {
        public string GetAll
        {
            get
            {
                return "select * from Dog";
            }
        }

        public string GetById
        {
            get
            {
                return "select * from Dog where DogId = @DogId";
            }
        }

        public string Insert
        {
            get
            {
                return @"insert into Dog (DogId, ShelterId, Name, Description, Breed, AvailableDate, PrimaryImageFile, IsPublic)" +
                    "       values (@DogId, @ShelterId, @Name, @Description, @Breed, @AvailableDate, @PrimaryImageFIle, @IsPublic)";
            }
        }

        public string Update
        {
            get
            {
                return @"update Dog
                    set 
                        ShelterId = @ShelterId,
                        Name = @Name,
                        Description = @Description,
                        Breed = @Breed,
                        AvailableDate = @AvailableDate,
                        PrimaryImageFile = @PrimaryImageFile,
                        IsPublic = @IsPublic
                    where DogId = @DogId";
            }
        }

        public string Delete
        {
            get
            {
                return @"delete from Dog
                    where DogId = @DogId";
            }
        }
    }

    public partial class Queries : IQueries
    {
        public DogQueries Dog = new DogQueries();
    }
}
