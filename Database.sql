-- Chạy trong SSMS. Script có thể chạy lại và không xóa dữ liệu đã có.
IF DB_ID(N'ChuDe5_HuongDan_RetaurantManagement') IS NULL
    CREATE DATABASE [ChuDe5_HuongDan_RetaurantManagement];
GO
USE [ChuDe5_HuongDan_RetaurantManagement];
GO

IF OBJECT_ID(N'dbo.Restaurant', N'U') IS NULL
CREATE TABLE dbo.Restaurant (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(1000) NOT NULL DEFAULT N'Chưa đặt tên',
    Address nvarchar(3000) NOT NULL DEFAULT N'Chưa có địa chỉ',
    Phone nvarchar(100) NOT NULL DEFAULT N'',
    Website nvarchar(1000) NULL
);
IF OBJECT_ID(N'dbo.Hall', N'U') IS NULL
CREATE TABLE dbo.Hall (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(1000) NOT NULL DEFAULT N'Chưa đặt tên',
    RestaurantID int NOT NULL REFERENCES dbo.Restaurant(ID)
);
IF OBJECT_ID(N'dbo.[Table]', N'U') IS NULL
CREATE TABLE dbo.[Table] (
    ID int IDENTITY(1,1) PRIMARY KEY,
    TableCode nvarchar(200) NOT NULL,
    Name nvarchar(1000) NULL,
    Status int NOT NULL DEFAULT 0 CHECK (Status IN (0,1,2)),
    Seats int NULL CHECK (Seats IS NULL OR Seats > 0),
    HallID int NOT NULL REFERENCES dbo.Hall(ID)
);
-- HuongDan đã dùng tên Category và Type 1 = món ăn, 0 = đồ uống.
IF OBJECT_ID(N'dbo.Category', N'U') IS NULL
CREATE TABLE dbo.Category (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Type int NOT NULL CHECK (Type IN (0,1))
);
IF OBJECT_ID(N'dbo.Food', N'U') IS NULL
CREATE TABLE dbo.Food (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(1000) NOT NULL,
    CategoryID int NOT NULL REFERENCES dbo.Category(ID),
    Price int NOT NULL DEFAULT 0 CHECK (Price >= 0),
    Notes nvarchar(3000) NULL
);
IF OBJECT_ID(N'dbo.Account', N'U') IS NULL
CREATE TABLE dbo.Account (
    AccountName nvarchar(100) PRIMARY KEY,
    [Password] nvarchar(200) NOT NULL,
    FullName nvarchar(1000) NOT NULL,
    Email nvarchar(1000) NULL,
    Phone nvarchar(200) NOT NULL DEFAULT N'',
    DateCreated smalldatetime NOT NULL DEFAULT GETDATE()
);
IF OBJECT_ID(N'dbo.[Role]', N'U') IS NULL
CREATE TABLE dbo.[Role] (
    ID int IDENTITY(1,1) PRIMARY KEY,
    RoleName nvarchar(1000) NOT NULL,
    [Path] nvarchar(3000) NULL,
    Notes nvarchar(3000) NULL
);
IF OBJECT_ID(N'dbo.RoleAccount', N'U') IS NULL
CREATE TABLE dbo.RoleAccount (
    AccountName nvarchar(100) NOT NULL REFERENCES dbo.Account(AccountName),
    RoleID int NOT NULL REFERENCES dbo.[Role](ID),
    Actived bit NOT NULL DEFAULT 1,
    Notes nvarchar(3000) NULL,
    CONSTRAINT PK_RoleAccount PRIMARY KEY (AccountName, RoleID)
);
-- Phụ lục ghi Invoice.AccountID kiểu int nhưng Account dùng AccountName làm khóa.
IF OBJECT_ID(N'dbo.Invoice', N'U') IS NULL
CREATE TABLE dbo.Invoice (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(1000) NOT NULL DEFAULT N'Hóa đơn thanh toán',
    TableID int NOT NULL REFERENCES dbo.[Table](ID),
    Total int NOT NULL DEFAULT 0,
    Discount float NULL DEFAULT 0,
    Tax float NULL DEFAULT 0,
    Status bit NOT NULL DEFAULT 0,
    AccountName nvarchar(100) NOT NULL REFERENCES dbo.Account(AccountName),
    CheckoutDate smalldatetime NOT NULL DEFAULT GETDATE()
);
IF OBJECT_ID(N'dbo.InvoiceDetails', N'U') IS NULL
CREATE TABLE dbo.InvoiceDetails (
    ID int IDENTITY(1,1) PRIMARY KEY,
    InvoiceID int NOT NULL REFERENCES dbo.Invoice(ID),
    FoodID int NOT NULL REFERENCES dbo.Food(ID),
    Amount int NOT NULL DEFAULT 0 CHECK (Amount >= 0)
);
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Restaurant)
    INSERT dbo.Restaurant (Name, Address, Phone) VALUES (N'Nhà hàng mẫu', N'Đà Lạt', N'');
IF NOT EXISTS (SELECT 1 FROM dbo.Hall)
    INSERT dbo.Hall (Name, RestaurantID) SELECT N'Sảnh chính', MIN(ID) FROM dbo.Restaurant;
IF NOT EXISTS (SELECT 1 FROM dbo.[Table])
    INSERT dbo.[Table] (TableCode, Name, Status, Seats, HallID)
    SELECT N'B01', N'Bàn 01', 0, 4, MIN(ID) FROM dbo.Hall;
IF NOT EXISTS (SELECT 1 FROM dbo.Category WHERE Type = 1)
    INSERT dbo.Category (Name, Type) VALUES (N'Món ăn', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Category WHERE Type = 0)
    INSERT dbo.Category (Name, Type) VALUES (N'Đồ uống', 0);
IF NOT EXISTS (SELECT 1 FROM dbo.Food)
    INSERT dbo.Food (Name, CategoryID, Price)
    SELECT N'Cơm', MIN(ID), 35000 FROM dbo.Category WHERE Type = 1;
IF NOT EXISTS (SELECT 1 FROM dbo.[Role])
    INSERT dbo.[Role] (RoleName) VALUES (N'Nhân viên'), (N'Quản lý');
IF NOT EXISTS (SELECT 1 FROM dbo.Account)
    INSERT dbo.Account (AccountName, [Password], FullName) VALUES (N'demo', N'demo', N'Nhân viên mẫu');
IF NOT EXISTS (SELECT 1 FROM dbo.RoleAccount)
    INSERT dbo.RoleAccount (AccountName, RoleID, Actived)
    SELECT TOP (1) a.AccountName, r.ID, 1 FROM dbo.Account a CROSS JOIN dbo.[Role] r ORDER BY a.AccountName, r.ID;

IF NOT EXISTS (SELECT 1 FROM dbo.Invoice) AND EXISTS (SELECT 1 FROM dbo.Food)
BEGIN
    DECLARE @tableId int = (SELECT MIN(ID) FROM dbo.[Table]);
    DECLARE @foodId int = (SELECT MIN(ID) FROM dbo.Food);
    DECLARE @accountName nvarchar(100) = (SELECT TOP (1) AccountName FROM dbo.Account ORDER BY AccountName);
    DECLARE @price int = (SELECT Price FROM dbo.Food WHERE ID = @foodId);
    DECLARE @discount int = CASE WHEN @price >= 5000 THEN 5000 ELSE 0 END;
    INSERT dbo.Invoice (Name, TableID, Total, Discount, Tax, Status, AccountName, CheckoutDate)
    VALUES (N'Hóa đơn mẫu đã trả', @tableId, @price - @discount, @discount, 0, 1, @accountName, GETDATE());
    INSERT dbo.InvoiceDetails (InvoiceID, FoodID, Amount) VALUES (SCOPE_IDENTITY(), @foodId, 1);
    INSERT dbo.Invoice (Name, TableID, Total, Discount, Tax, Status, AccountName, CheckoutDate)
    VALUES (N'Hóa đơn hiện tại', @tableId, @price, 0, 0, 0, @accountName, GETDATE());
    INSERT dbo.InvoiceDetails (InvoiceID, FoodID, Amount) VALUES (SCOPE_IDENTITY(), @foodId, 1);
END;
GO
