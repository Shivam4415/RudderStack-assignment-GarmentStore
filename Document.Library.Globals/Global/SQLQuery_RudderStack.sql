
Create Schema rudderstack


select * from [RudderStack].[USERS]

INSERT INTO [RudderStack].[USERS](Name, Email, Phone, Company, Password, DateCreated, Lastactive) 
VALUES('Shivam', 'shivam441@gmail.com', '9007194710', 'Mapline', 'ShivamGupta441', GETUTCDATE(), GetUtcdate()) 


CREATE TABLE [RudderStack].[USERS]
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NAME NVARCHAR(50),
	EMAIL NVARCHAR(150) NOT NULL UNIQUE, 
	PHONE NVARCHAR(50),
	COMPANY NVARCHAR(50),
	PASSWORD NVARCHAR(100),
	ISVERIFIED BIT DEFAULT(0),
	ISONLINE BIT DEFAULT(0),
	DATECREATED DATETIME,
	LASTACTIVE DATETIME
)

CREATE PROCEDURE [RudderStack].[GetUserProfileByEmail]
@email NVARCHAR(50)
AS
BEGIN
	SELECT 
		ID,
		NAME,
		EMAIL,
		PASSWORD,
		ISVERIFIED AS HasVerifiedEmail,
		ISONLINE,
		DATECREATED,
		LASTACTIVE
	FROM
		[RudderStack].[USERS]
	WHERE 
		EMAIL = @email
END
GO



CREATE TABLE [RudderStack].[UserSessions]
(
	ID UNIQUEIDENTIFIER NOT NULL,
	USERID INT NOT NULL FOREIGN KEY REFERENCES [RudderStack].[Users](ID),
	LASTACCESS DATETIME NOT NULL,
	ISPERSISTENT BIT NOT NULL DEFAULT(0),
	ACCESSTOKEN NVARCHAR(MAX) NULL,
	ACCESSTOKENEXPIRY DATETIME NULL,
	LASTCHECKACCESSTOKEN DATETIME NULL
)



 
CREATE PROCEDURE [RudderStack].[RemoveUserSession]
@sessionId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [RudderStack].[USERSESSIONS] WHERE ID = @sessionId
END
GO



CREATE PROCEDURE [RudderStack].[SetUserSession]
@userId INT,
@sessionId UNIQUEIDENTIFIER,
@lastAccess DATETIME,
@isPersistent BIT,
@accessToken NVARCHAR(MAX) = NULL,
@accessTokenExpiry DATETIME = NULL,
@lastCheckAccessToken DATETIME = NULL
AS
BEGIN
	DELETE FROM [RudderStack].[USERSESSIONS] WHERE USERID = @userId

	INSERT INTO 
		[RudderStack].[USERSESSIONS](ID, USERID, LASTACCESS, ISPERSISTENT, ACCESSTOKEN, ACCESSTOKENEXPIRY, LASTCHECKACCESSTOKEN)
	VALUES
		(@sessionId, @userId, @lastAccess, @isPersistent, @accessToken, @accessTokenExpiry, @lastCheckAccessToken)			
END
GO



CREATE PROCEDURE [RudderStack].[GetUserSession]
@sessionId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		US.EMAIL  AS EMAIL,
		URS.ID ,
		URS.USERID ,
		URS.LASTACCESS ,
		URS.ISPERSISTENT ,
		URS.ACCESSTOKEN ,
		URS.ACCESSTOKENEXPIRY ,
		URS.LASTCHECKACCESSTOKEN 
	FROM
		[RudderStack].[USERSESSIONS] URS
	JOIN
		USERS US
	ON
		URS.USERID = US.ID
	WHERE
		URS.ID = @sessionId
	
END
GO



CREATE PROCEDURE [RudderStack].[UpdateUserSession]
@userId INT,
@sessionId UNIQUEIDENTIFIER,
@accessToken NVARCHAR(MAX) = NULL,
@accessTokenExpiry DATETIME = NULL,
@lastCheckAccessToken DATETIME = NULL 
AS
BEGIN
	UPDATE 
		[RudderStack].[USERSESSIONS]
	SET
		ACCESSTOKEN = @accessToken, 
		ACCESSTOKENEXPIRY =@accessTokenExpiry, 
		LASTCHECKACCESSTOKEN = @lastCheckAccessToken
	WHERE
		ID = @sessionId AND USERID = @userId
	
	
END
GO


CREATE PROCEDURE [RudderStack].[SlideUserSessionExpiration]
@sessionId UNIQUEIDENTIFIER,
@newExpirationTime DATETIME,
@lastCheckAccessToken DATETIME = NULL
AS
BEGIN
	UPDATE 
		[RudderStack].[USERSESSIONS]
	SET
		LASTACCESS = @newExpirationTime, 
		LASTCHECKACCESSTOKEN = @lastCheckAccessToken
	WHERE
		ID = @sessionId
		
	IF @@ROWCOUNT = 0
	BEGIN
		return null;  
	END
	 
	
END
GO


--INSERT INTO RudderStack.Product(Name, Quantity, Source) Values('Shirt', 10, 'Img/Shirt/Blue.jpeg')
--INSERT INTO RudderStack.Product(Name, Quantity, Source) Values('Pant', 10, 'Img/Shirt/Blue.jpeg')
--INSERT INTO RudderStack.Product(Name, Quantity, Source) Values('Shoe', 10, 'Img/Shirt/Blue.jpeg')
--INSERT INTO RudderStack.Product(Name, Quantity, Source) Values('Socks', 10, 'Img/Shirt/Blue.jpeg')

--Insert INTO RudderStack.Price(Value) VALUES(1000)
--Insert INTO RudderStack.Price(Value) VALUES(2000)
--Insert INTO RudderStack.Price(Value) VALUES(3000)
--Insert INTO RudderStack.Price(Value) VALUES(4000)
--Insert INTO RudderStack.Price(Value) VALUES(5000)
--Insert INTO RudderStack.Price(Value) VALUES(6000) 

--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(1, 1)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(1, 2)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(1, 3)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(2, 1)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(2, 3)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(3, 1)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(3, 2)
--INSERT INTO RudderStack.ProductSize(ProductId, SizeId) Values(4, 1)

--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(1,1)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(1,2)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(2,1)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(2,2)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(2,3)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(3,1)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(3,2)
--INSERT INTO RudderStack.ProductColor(ProductId, ColorId) Values(4,2)

--INSERT INTO RudderStack.ProductPrice(ProductId, PriceId, DateCreated, IsDeleted) Values(1, 1, GetDate(), 0)
--INSERT INTO RudderStack.ProductPrice(ProductId, PriceId, DateCreated, IsDeleted) Values(2, 2, GetDate(), 0)
--INSERT INTO RudderStack.ProductPrice(ProductId, PriceId, DateCreated, IsDeleted) Values(3, 3, GetDate(), 0)
--INSERT INTO RudderStack.ProductPrice(ProductId, PriceId, DateCreated, IsDeleted, DateDeleted) Values(4, 4, GetDate(), 1, Getdate() + 1)
--INSERT INTO RudderStack.ProductPrice(ProductId, PriceId, DateCreated, IsDeleted) Values(4, 5, GetDate(), 0)

Go

SELECT * from Product
select * from Price
select * from ProductColor
select * from ProductSize
select * from ProductPrice
GO


Create Table [RudderStack].[Product]
(
Id Int Primary Key Identity(1, 1),
Name Nvarchar(50),
Quantity Int,
Source Nvarchar(50)
)

CREATE Table [RudderStack].[ProductSize]
(
ProductId Int NOT NULL Foreign Key References [RudderStack].[Product](Id),
SizeId INT Not NULL	
)

CREATE TABLE [RudderStack].[ProductColor]
(
ProductId Int NOT NULL Foreign Key References [RudderStack].[Product](Id),
ColorId Int Not NULL
)

Create Table [RudderStack].[ProductPrice]
(
Id Int Primary Key Identity(1, 1),
ProductId Int NOT NULL Foreign Key References [RudderStack].[Product](Id),
PriceId Int Not NULL,
DateCreated DATETIME Not NULL,
DateDeleted DateTime Default(NULL),
IsDeleted Bit
) 

Create Table [RudderStack].[Price]
(
Id Int Primary Key Identity(1, 1),
Value Int Not NULL  
) 


CREATE Table [RudderStack].[UserProduct]
(
Id Int Primary Key Identity(1, 1),
UserId Int NOT NULL FOREIGN KEY REFERENCES [RudderStack].[USERS](ID),
ProductId Int Not Null Foreign Key References [RudderStack].[Product](Id),
Quantity Int Not Null,
Price Int Not NULL,
Color INT Not Null,
Size Int Not Null,
IsSaved Bit Default(0)
)


CREATE Procedure [RudderStack].[GetAllProducts]
AS
BEGIN
SELECT 
	P.Id, P.Name, P.Quantity, P.Source AS Source, PS.Value AS Value, PS.Id AS Id
FROM
	[RudderStack].[Product] P
JOIN
	[RudderStack].[ProductPrice] PR
ON
	P.Id = PR.ProductId AND PR.IsDeleted = 0
JOIN
	[RudderStack].[Price] PS
ON
	PS.Id = PR.PriceId
END
GO

---
CREATE Procedure [RudderStack].[GetProductColor]
@products [RudderStack].[IntArrayTableType] ReadOnly
AS
BEGIN
SELECT 
	PC.ProductId AS Id,
	PC.ColorId AS ColorId
FROM 
	[RudderStack].[ProductColor] PC
JOIN
	@products P
ON
	PC.ProductId = P.Id
END 
GO


CREATE Procedure [RudderStack].[GetProductSize]
@products AS [RudderStack].[IntArrayTableType] ReadOnly
AS
BEGIN
SELECT 
	PS.ProductId AS Id,
	PS.SizeId AS SizeId
FROM 
	[RudderStack].[ProductSize] PS
JOIN
	@products P
ON
	PS.ProductId = P.Id
END 
GO



CREATE Procedure [RudderStack].[GetUserProducts]
@userId Int
AS
BEGIN
	SELECT UP.Id AS Id, UP.UserId AS UserId, UP.IsSaved, 
	 P.Name AS Name, P.Id AS Id, UP.Quantity AS Quantity, P.Source AS Source,
	 UP.Color AS Color, 
	 UP.Size AS Size, 
	 UP.Price AS Value
	FROM [RudderStack].[Product] P 
	JOIN [RudderStack].[UserProduct] UP ON UP.ProductId = P.Id
	WHERE UP.UserId = @userId
END
GO


CREATE TYPE [RudderStack].[IntArrayTableType] AS TABLE(
	[Id] [int] NOT NULL
)

CREATE TYPE [RudderStack].[ProductsTableType] AS TABLE(
	Id INT NOT NULL,
	Quantity INT NOT NULL,
	Price INT NOT NULL
)


CREATE PROCEDURE [RudderStack].[SaveUserProducts]
@userId Int,
@products AS [RudderStack].[IntArrayTableType] ReadOnly
AS
BEGIN
	Update UP 
	SET 
	UP.IsSaved = 1
	FROM [RudderStack].[UserProduct] UP
	JOIN
	@products P
	ON P.Id = UP.ProductId
	WHERE UP.UserId = @userId

END
GO


CREATE PROCEDURE [RudderStack].[SaveUserProduct]
@userId Int,
@productId Int,
@productPrice Int,
@productQuantity Int,
@productSize Int,
@productColor Int
AS
BEGIN
	Insert Into [RudderStack].[UserProduct](UserId, ProductId, Quantity, Price, Size, Color)
	SELECT @userId, @productId, @productQuantity, @productPrice, @productSize, @productColor
	SELECT 
		Id AS Id, UserId AS UserId, 
		Quantity as Quantity, ProductId As Id, Price AS Value From  [RudderStack].[UserProduct] WHERE Id = SCOPE_IDENTITY() 
END
GO

CREATE PROCEDURE [RudderStack].[UpdateUserProduct]
@id Int,
@price Int,
@quantity Int
AS
BEGIN
	Update [RudderStack].[UserProduct] SET Price = @price, Quantity = @quantity WHERE Id = @id
END
GO

CREATE PROCEDURE [RudderStack].[DeleteProduct]
@id Int
AS 
BEGIN
	Delete From [RudderStack].[UserProduct] Where Id = @id
END 
GO


select * from [RudderStack].UserProduct

select * from [RudderStack].product

update [RudderStack].Product set Source = 'Img/Pant/Black.jpeg' where Id = 2
update [RudderStack].Product set Source = 'Img/Shoe/Red.jpeg' where Id = 3

update [RudderStack].Product set Source = 'Img/Socks/Blue.jpeg' where Id = 4

select * from [RudderStack].USERSESSIONS

delete from [RudderStack].USERSESSIONS