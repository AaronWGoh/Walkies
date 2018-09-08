/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
	CREATE TABLE State(
		StateCode VARCHAR(2) NOT NULL PRIMARY KEY,
		StateName VARCHAR(50) NOT NULL
		)

	CREATE TABLE Shelter(
		ShelterId UNIQUEIDENTIFIER PRIMARY KEY,
		Name VARCHAR(100) NOT NULL,
		Street1 VARCHAR(100) NOT NULL,
		Street2 VARCHAR(100) NULL,
		City VARCHAR(100) NOT NULL,
		StateCode VARCHAR(2) NOT NULL FOREIGN KEY REFERENCES State(StateCode),
		Zip VARCHAR(9) NOT NULL,
		Latitude DECIMAL(18,9) NULL,
		Longitude DECIMAL(18,9) NULL,
		Phone VARCHAR(20) NOT NULL,
		Fax VARCHAR(20) NULL,
		Email VARCHAR(100) NOT NULL
		)

	CREATE TABLE UserType (
		UserTypeCode VARCHAR(10) NOT NULL PRIMARY KEY,
		Name TEXT NOT NULL
		)

	CREATE TABLE UserProfile(
		UserProfileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		Street1 VARCHAR(100) NOT NULL,
		Street2 VARCHAR(100) NULL,
		City VARCHAR(100) NOT NULL,
		StateCode VARCHAR(2) NOT NULL FOREIGN KEY REFERENCES State(StateCode),
		Zip VARCHAR(9) NOT NULL,
		Latitude DECIMAL(18,9) NULL,
		Longitude DECIMAL(18,9) NULL,
		Phone VARCHAR(20) NOT NULL,
		Fax VARCHAR(20) NULL,
		Email VARCHAR(100) NOT NULL,
		ProfileImageUrl VARCHAR(200) NOT null
		)
		
	CREATE TABLE AccountType (
		AccountTypeCode VARCHAR(10) NOT NULL PRIMARY KEY,
		Name TEXT NOT NULL
		)

	CREATE TABLE Account(
		AccountId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		ParentAccountId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES AccountId(AccountId),
		ShelterId UNIQUEIDENTIFIER NOT null
	)


    CREATE TABLE Dog (
        DogId UNIQUEIDENTIFIER PRIMARY KEY,
		ShelterId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Shelter(ShelterId),
		Name VARCHAR(100) NULL,
		Description TEXT NULL,
		Breed VARCHAR(50) NULL,
		AvailableDate DATETIME NULL,
		PrimaryImageFileId UNIQUEIDENTIFIER NULL,
		IsPublic BIT NOT NULL default(0)
    )

	CREATE TABLE MetaFile(
		MetaFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		FileType VARCHAR(10) NOT NULL,
		OriginalFilename VARCHAR(200) NOT NULL,
		StoredFileName VARCHAR(200) NOT NULL,
		Extension VARCHAR(5) NOT NULL,
		StorageUri VARCHAR(200) NOT NULL,
		Description TEXT NOT NULL
	)
    
	CREATE TABLE DogFile(
		DogFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		DogId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Dog(DogId),
		MetaFileId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES MetaFile(MetaFileId),
		SortOrder INT NOT NULL
	)

	CREATE TABLE ShelterFile(
		ShelterFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		ShelterID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Shelter(ShelterId),
		MetaFileId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES MetaFile(MetaFileId),
		SortOrder INT NOT NULL
	)
