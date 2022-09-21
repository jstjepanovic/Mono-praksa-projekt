USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'ProjectBoard'
)
CREATE DATABASE [ProjectBoard]
GO

USE [ProjectBoard]

IF OBJECT_ID('[UserGroup]', 'U') IS NOT NULL
DROP TABLE UserGroup
GO

IF OBJECT_ID('[Listing]', 'U') IS NOT NULL
DROP TABLE Listing
GO

IF OBJECT_ID('[TableOrder]', 'U') IS NOT NULL
DROP TABLE TableOrder
GO

IF OBJECT_ID('[Review]', 'U') IS NOT NULL
DROP TABLE Review
GO

IF OBJECT_ID('[TableUser]', 'U') IS NOT NULL
DROP TABLE TableUser
GO

IF OBJECT_ID('[BoardGame]', 'U') IS NOT NULL
DROP TABLE BoardGame
GO

IF OBJECT_ID('[Genre]', 'U') IS NOT NULL
DROP TABLE Genre
GO

IF OBJECT_ID('[TableGroup]', 'U') IS NOT NULL
DROP TABLE TableGroup
GO

IF OBJECT_ID('[Category]', 'U') IS NOT NULL
DROP TABLE Category
GO



CREATE TABLE Genre
(
	GenreId uniqueidentifier not null primary key,
	Name nvarchar(50)
)


CREATE TABLE BoardGame
(
	BoardGameId uniqueidentifier not null primary key,
	Name [nvarchar](100)  not null,
	NoPlayersMin [int] not null,
	NoPlayersMax [int] not null,
	Age [int] not null,
	AvgPlayingTime [int],
	Rating [float],
	Weight [float],
	Publisher [nvarchar](200) not null,
	GenreId uniqueidentifier not null constraint GameGenreFk foreign key references Genre(GenreId),
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null
)



CREATE TABLE TableUser
(
	UserId uniqueidentifier not null primary key,
	Username [nvarchar](50) not null constraint UserUnique unique,
	Email [nvarchar](100) not null constraint EmailUnique unique,
	Password [nvarchar](50) not null,
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null,
	constraint InvalidEmail check ((case when Email = '' then 0
									when Email like '% %' then 0
									when Email like ('%["(),:;<>\]%') then 0
									when substring(Email,charindex('@',Email),len(Email)) like ('%[!#$%&*+/=?^`_{|]%') then 0
									when (left(Email,1) like ('[-_.+]') or right(Email,1) like ('[-_.+]')) then 0                                                                                    
									when (Email like '%[%' or Email like '%]%') then 0
									when Email LIKE '%@%@%' then 0
									when Email NOT LIKE '_%@_%._%' then 0
									else 1 end) = 1)
)


CREATE TABLE Review
(
	ReviewId uniqueidentifier not null primary key,
	Rating [float] not null,
	Weight [float] not null,
	ReviewText [nvarchar](500),
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null,
	UserId uniqueidentifier not null constraint ReviewUserFk foreign key references TableUser(UserId) on delete cascade,
	BoardGameId uniqueidentifier not null constraint ReviewBoardGameFk foreign key references BoardGame(BoardGameId) on delete cascade,
	constraint RatingWeightLimits check (Rating between 1 and 10 and Weight between 1 and 5)
)


CREATE TABLE TableOrder
(
	OrderId uniqueidentifier not null primary key,
	DeliveryAddress [nvarchar](200) not null,
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null,
	UserId uniqueidentifier not null constraint OrderUserFk foreign key references TableUser(UserId) on delete cascade,
)


CREATE TABLE Listing
(
	ListingId uniqueidentifier not null primary key,
	Price float not null,
	Condition nvarchar(50) not null,
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null,
	UserId uniqueidentifier not null constraint ListingUserFk foreign key references TableUser(UserId) on delete cascade,
	BoardGameId uniqueidentifier not null constraint ListingBoardGameFk foreign key references BoardGame(BoardGameId) on delete cascade,
	OrderId uniqueidentifier constraint OrderFk foreign key references TableOrder(OrderId)
)

CREATE TABLE Category
(
	CategoryId uniqueidentifier not null primary key,
	Name [nvarchar](100)  not null,
	Description [nvarchar](500) not null,
)

CREATE TABLE TableGroup
(
	GroupId uniqueidentifier not null primary key,
	Name [nvarchar](100)  not null,
	TimeCreated [datetime] not null,
	TimeUpdated [datetime] not null,
	CategoryId uniqueidentifier not null constraint CategoryGroupFk foreign key references Category(CategoryId)
)

CREATE TABLE UserGroup
(
	UserId uniqueidentifier constraint UserFk foreign key references TableUser(UserId),
	GroupId uniqueidentifier constraint GroupFk foreign key references TableGroup(GroupId),
	constraint UserGroupPk primary key(UserId, GroupId)
);