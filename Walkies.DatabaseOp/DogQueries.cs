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
                return @"insert into Dog (DogId, ShelterId, Name, Description, Street2, City, StateCode, Zip, Latitude, Longitude, Phone, Fax, Email)" +
                    "       values (@ShelterId, @Name, @Street1, @Street2, @City, @StateCode, @Zip, @Latitude, @Longitude, @Phone, @Fax, @Email)";
            }
        }

        public string Update
        {
            get
            {
                return @"update Shelter
                    set 
                        Name = @Name,
                        Street1 = @Street1,
                        Street2 = @Street2,
                        City = @City,
                        StateCode = @StateCode,
                        Zip = @Zip,
                        Latitude = @Latitude,
                        Longitude = @Longitude,
                        Phone  @Phone,
                        Fax = @Fax,
                        Email = @Email
                    where ShelterId = @ShelterId";
            }
        }
    }

    public partial class Queries : IQueries
    {
        public DogQueries Dog = new DogQueries();
    }
}
