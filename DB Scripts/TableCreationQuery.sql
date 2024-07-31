create database DCE_Test;



create table Customer(
	UserId uniqueidentifier DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
	UserName Varchar(30),
	Email Varchar(20),
	FirstName Varchar(20),
	LastName Varchar(20),
	CreatedOn DateTime,
	IsActive bit
);

create table Supplier(
	SupplierId uniqueidentifier DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
	SupplierName Varchar(50),
	CreatedOn DateTime,
	IsActive bit
);

create table Product(
	ProductId uniqueidentifier DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
	ProductName Varchar(50),
	UnitPrice decimal,
	SupplierId uniqueidentifier,
	CreatedOn DateTime,
	IsActive bit,
	CONSTRAINT FK_Product FOREIGN KEY (SupplierId) REFERENCES Supplier(SupplierId) ON DELETE CASCADE ON UPDATE CASCADE
);

create table OrderTable (
	OrderId uniqueidentifier DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
	ProductId uniqueidentifier,
	OrderStatus numeric(1,0),
	OrderType numeric(1,0),
	OrderBy uniqueidentifier,
	OrderOn DateTime,
	ShippedOn DateTime,
	IsActive bit,
	CONSTRAINT FK_Order1 FOREIGN KEY (OrderBy) REFERENCES Customer(UserId) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Order2 FOREIGN KEY (ProductId) REFERENCES Product(ProductId) ON DELETE CASCADE ON UPDATE CASCADE
);

