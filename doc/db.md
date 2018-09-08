[State]
*StateCode {"label":"nvarchar(2),not null"}
StateName {"label":"nvarchar(50),not null"}

[Shelter]
*ShelterId {"label":"uuid,not null"}
Name {"label":"nvarchar(100),not null"}
Street1 {"label":"nvarchar(100),not null"}
Street2 {"label":"nvarchar(100),not null"}
City {"label":"nvarchar(100),not null"}
+StateCode {"label":"nvarchar(2),not null"}
Zip {"label":"nvarchar(9),not null"}
Country {"label":"nvarchar(100),not null"}
Latitude {"label":"decimal(18,9),null"}
Longitude {"label":"decimal(18,9),null"}
Phone {"label":"nvarchar(20),not null"}
Fax {"label":"nvarchar(20),null"}
Email {"label":"nvarchar(100),not null"}

Shelter *--1 State

[UserType]
*UserTypeCode {"label":"varchar(10),not null"} # recs=Admin,Client
Name {"label":"text,not null"}

[UserProfile]
*+UserProfileId {"label":"uuid,not null"}
Street1 {"label":"varchar(100),not null"}
Street2 {"label":"varchar(100),not null"}
City {"label":"varchar(50),not null"}
+StateCode {"label":"varchar(2),not null"} # ui:c=autocomplete
ZipCode {"label":"varchar(9),not null"}
CountryCode {"label":"varchar(100),not null"}
Latitude {"label":"geo,null"}
Longitude {"label":"geo,null"}
Phone {"label":"varchar(20),not null"}
Fax {"label":"varchar(20),null"}
Email {"label":"varchar(100),not null"}
ProfileImageUrl {"label":"nvarchar(200),not null"}

UserProfile 1--1 AccountUser

[AccountUser]
*AccountUserId {"label":"uuid,not null"}
+UserTypeCode {"label":"varchar(10),not null"}
FirstName {"label":"text,not null"} # ui:kn1
LastName {"label":"text,not null"} # ui:kn0
LoginEmail {"label":"nvarchar(100),not null"}
RecoveryPhone {"label":"nvarchar(20),not null"}
PasswordHash {"label":"nvarchar(50),not null"}
CanLogin {"label":"bit,default(1)"}
IsLockedDateTime {"label":"datetime,null"}
ResetToken {"label":"nvarchar(50),null"}
ResetTokenExpiration {"label":"datetime,null"}

[AccountType]
*AccountTypeCode {"label":"varchar(10),not null"} # recs=Owner,Manager,Tenant
Name {"label":"text,not null"}

[Account]
*+AccountId {"label":"uuid,not null"}
+ParentAccountId {"label":"uuid,null"}
+CompanyId {"label":"uuid,not null"}

Account 1--1 UserProfile

[OwnerManager]
*OwnerAccountId {"label":"uuid,not null"}
*ManagerAccountId {"label":"uuid,not null"}

OwnerManager *--1 Account
OwnerManager *--1 Account

[Dog]
*DogId {"label":"uuid,not null"}
+ShelterId {"label":"uuid, not null"}
+ParentUnitId {"label":"uuid,null"}
Name {"label":"nvarchar(100),null"}
Description {"label":"text,null"}
Breed {"label":"nvarchar(50), null"}
AvailableDate {"label":"datetime,null"}
+PrimaryImageFileId {"label":"uuid,null"}
AttributeJson {"label":"jsonb,null"}
IsPublic {"label":"boolean,not null,default(1)"}

[MetaFile]
*MetaFileId {label:"uuid,not null"}
FileType {label:"varchar(10),not null"} # Image,Floorplan,Document
OriginalFilename {label:"varchar(200),not null"}
StoredFilename {label:"varchar(200),not null"} # ui:kn
Extension {label:"varchar(5),not null"}
StorageUri {label:"varchar(200),not null"}
Description {label:"text,null"}

[DogFile]
*PropertyFileId {label:"uuid,not null"}
+PropertyId {label:"uuid,not null"} # ui:kn
+MetaFileId {label:"uuid,not null"} # ui:kn
SortOrder {label:"int,not null"}

DogFile *--1 MetaFile

[ShelterFile]
*ShelterFileId {label:"uuid,not null"}
+MetaFileId {label:"uuid,not null"} # ui:kn
SortOrder {label:"int,not null"}

ShelterFileId *--1 MetaFile