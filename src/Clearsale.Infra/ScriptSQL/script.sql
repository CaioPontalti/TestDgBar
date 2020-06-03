CREATE DATABASE PubDb

GO

USE PubDb

GO

CREATE TABLE [user]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Email] VARCHAR(40) NOT NULL,
	[Password] VARCHAR(40) NOT NULL
)

GO

CREATE TABLE [products]
(
	[Id] INT PRIMARY KEY NOT NULL,
	[Name] VARCHAR(40) NOT NULL,
	[Value] NUMERIC(18,2) NOT NULL,
	[Value_Discount] NUMERIC(18,2) NULL
)

GO

INSERT INTO [products] VALUES (1, 'Cerveja', 5.00, 3.00 )
INSERT INTO [products] VALUES (2, 'Conhaque', 20.00, NULL )
INSERT INTO [products] VALUES (3, 'Suco', 50.00, NULL )
INSERT INTO [products] VALUES (4, 'Água', 70.00, 70.00 )


CREATE TABLE [order]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Order_Number] INT NOT NULL,
	[Product_Id] INT NOT NULL,
	[Value] NUMERIC (18, 2) NOT NULL,
	[Has_Discount] BIT NULL
)

GO

CREATE TABLE [order_discount]
(
	[order] INT NOT NULL,
	[product] INT NOT NULL,
	[Value] NUMERIC (18, 2) NOT NULL
)

GO

CREATE TABLE [order_free]
(
	[order] INT NOT NULL,
	[product] INT NULL, 
	[amount] INT NULL
)

GO 

CREATE TABLE Pay_Order
(
	Order_Number INT NOT NULL,
	Value_Pay NUMERIC(18,2) NOT NULL
)


