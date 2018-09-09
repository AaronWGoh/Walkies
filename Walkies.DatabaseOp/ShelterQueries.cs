using System;
using System.Collections.Generic;
using System.Text;
using Walkies.Common;

namespace Walkies.DatabaseOperations
{
    public class ShelterQueries
    {
        public string GetAll
        {
            get
            {
                return "select * from Shelter";
            }
        }

        public string GetById
        {
            get
            {
                return "select * from Shelter where ShelterId = @ShelterId";
            }
        }

        public string Insert
        {
            get
            {
                return @"insert into Shelter (ShelterId, Name, Street1, Street2, City, StateCode, Zip, Latitude, Longitude, Phone, Fax, Email)" +
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
                        Phone = @Phone,
                        Fax = @Fax,
                        Email = @Email
                    where ShelterId = @ShelterId";
            }
        }

        public string Delete
        {
            get
            {
                return @"delete from Shelter
                    where ShelterId = @ShelterId";
            }
        }
    }

    public partial class Queries : IQueries
    {
        public ShelterQueries Shelter = new ShelterQueries();
    }
}
