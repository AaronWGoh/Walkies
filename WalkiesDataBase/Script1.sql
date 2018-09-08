﻿	CREATE TABLE State(
		StateCode VARCHAR(2) NOT NULL PRIMARY KEY,
		StateName VARCHAR(50) NOT NULL
		)
	GO

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
		GO

	CREATE TABLE UserType (
		UserTypeCode VARCHAR(10) NOT NULL PRIMARY KEY,
		Name TEXT NOT NULL
		)
		GO

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
		GO

	CREATE TABLE AccountType (
		AccountTypeCode VARCHAR(10) NOT NULL PRIMARY KEY,
		Name TEXT NOT NULL
		)
		GO

	CREATE TABLE Account(
		AccountId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		ParentAccountId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES Account(AccountId),
		ShelterId UNIQUEIDENTIFIER NOT null
	)
	GO

	CREATE TABLE AccountUser(
		AccountUserId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		UserTypeCode varchar(10) not null FOREIGN KEY REFERENCES UserType(UserTypeCode),
		FirstName  TEXT not null,
		LastName text not null,
		LoginEmail varchar(100) NOT NULL,
		RecoveryPhone varchar(20) NOT NULL,
		PasswordHash varchar(50) not null,
		CanLogin bit DEFAULT(1),
		IsLockedDateTime datetime NULL,
		ResetToken varchar(50) null,
		ResetTokenExpiration datetime null
	)
	GO

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
	GO

	CREATE TABLE MetaFile(
		MetaFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		FileType VARCHAR(10) NOT NULL,
		OriginalFilename VARCHAR(200) NOT NULL,
		StoredFileName VARCHAR(200) NOT NULL,
		Extension VARCHAR(5) NOT NULL,
		StorageUri VARCHAR(200) NOT NULL,
		Description TEXT NOT NULL
	)
	GO
    
	CREATE TABLE DogFile(
		DogFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		DogId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Dog(DogId),
		MetaFileId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES MetaFile(MetaFileId),
		SortOrder INT NOT NULL
	)
	GO

	CREATE TABLE ShelterFile(
		ShelterFileId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
		ShelterID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Shelter(ShelterId),
		MetaFileId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES MetaFile(MetaFileId),
		SortOrder INT NOT NULL
	)
	GO