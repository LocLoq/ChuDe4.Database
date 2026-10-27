-- Chạy sau Database.sql. Có thể chạy lại mà không thêm trùng dữ liệu mẫu.
USE [ChuDe5_HuongDan_RetaurantManagement];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @restaurantId int = (SELECT MIN(ID) FROM dbo.Restaurant);
IF @restaurantId IS NULL
    THROW 50001, N'Hãy chạy Database.sql trước Seed.sql.', 1;

INSERT dbo.Hall (Name, RestaurantID)
SELECT v.Name, @restaurantId
FROM (VALUES (N'Sảnh sân vườn'), (N'Sảnh tầng 2')) AS v(Name)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Hall h WHERE h.Name = v.Name AND h.RestaurantID = @restaurantId);

INSERT dbo.Category (Name, Type)
SELECT v.Name, v.Type
FROM (VALUES
    (N'Món nướng', 1), (N'Món xào', 1), (N'Canh và lẩu', 1),
    (N'Cơm và mì', 1), (N'Cà phê', 0), (N'Nước ép', 0)
) AS v(Name, Type)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Category c WHERE c.Name = v.Name AND c.Type = v.Type);

INSERT dbo.Food (Name, CategoryID, Price, Notes)
SELECT v.Name, c.ID, v.Price, v.Notes
FROM (VALUES
    (N'Món nướng', N'Gà nướng mật ong', 89000, N'Khẩu phần 1 người'),
    (N'Món nướng', N'Sườn nướng', 105000, N'Ăn kèm rau'),
    (N'Món nướng', N'Bò nướng lá lốt', 99000, N'10 cuốn'),
    (N'Món xào', N'Rau muống xào tỏi', 42000, NULL),
    (N'Món xào', N'Mì xào bò', 69000, NULL),
    (N'Món xào', N'Nấm xào bơ tỏi', 55000, NULL),
    (N'Canh và lẩu', N'Canh chua cá', 79000, NULL),
    (N'Canh và lẩu', N'Lẩu gà lá é', 239000, N'Khẩu phần 2-3 người'),
    (N'Canh và lẩu', N'Lẩu nấm', 199000, N'Khẩu phần 2-3 người'),
    (N'Cơm và mì', N'Cơm chiên hải sản', 75000, NULL),
    (N'Cơm và mì', N'Cơm gà', 59000, NULL),
    (N'Cơm và mì', N'Bún bò', 65000, NULL),
    (N'Cà phê', N'Cà phê đen', 29000, NULL),
    (N'Cà phê', N'Cà phê sữa', 35000, NULL),
    (N'Cà phê', N'Bạc xỉu', 39000, NULL),
    (N'Nước ép', N'Nước ép cam', 45000, NULL),
    (N'Nước ép', N'Nước ép dưa hấu', 39000, NULL),
    (N'Nước ép', N'Nước ép táo', 49000, NULL)
) AS v(CategoryName, Name, Price, Notes)
JOIN dbo.Category c ON c.Name = v.CategoryName AND c.Type = CASE WHEN v.CategoryName IN (N'Cà phê', N'Nước ép') THEN 0 ELSE 1 END
WHERE NOT EXISTS (SELECT 1 FROM dbo.Food f WHERE f.Name = v.Name AND f.CategoryID = c.ID);

INSERT dbo.[Role] (RoleName)
SELECT v.RoleName
FROM (VALUES (N'Thu ngân'), (N'Phục vụ')) AS v(RoleName)
WHERE NOT EXISTS (SELECT 1 FROM dbo.[Role] r WHERE r.RoleName = v.RoleName);

INSERT dbo.Account (AccountName, [Password], FullName, Email, Phone)
SELECT v.AccountName, N'123456', v.FullName, v.Email, v.Phone
FROM (VALUES
    (N'test_quanly', N'Nguyễn Minh Quản', N'quanly@example.com', N'0901000001'),
    (N'test_thungan', N'Trần Thị Thu', N'thungan@example.com', N'0901000002'),
    (N'test_phucvu', N'Lê Văn Phúc', N'phucvu@example.com', N'0901000003'),
    (N'test_ngung', N'Phạm Thị Hương', N'ngung@example.com', N'0901000004')
) AS v(AccountName, FullName, Email, Phone)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Account a WHERE a.AccountName = v.AccountName);

INSERT dbo.RoleAccount (AccountName, RoleID, Actived)
SELECT v.AccountName, r.ID, v.Actived
FROM (VALUES
    (N'test_quanly', N'Quản lý', CAST(1 AS bit)),
    (N'test_quanly', N'Thu ngân', CAST(1 AS bit)),
    (N'test_thungan', N'Thu ngân', CAST(1 AS bit)),
    (N'test_phucvu', N'Phục vụ', CAST(1 AS bit)),
    (N'test_ngung', N'Phục vụ', CAST(0 AS bit))
) AS v(AccountName, RoleName, Actived)
JOIN dbo.[Role] r ON r.RoleName = v.RoleName
WHERE NOT EXISTS (SELECT 1 FROM dbo.RoleAccount ra WHERE ra.AccountName = v.AccountName AND ra.RoleID = r.ID);

INSERT dbo.[Table] (TableCode, Name, Status, Seats, HallID)
SELECT v.TableCode, v.Name, v.Status, v.Seats, h.ID
FROM (VALUES
    (N'TEST01', N'Bàn thử 01', 1, 4, N'Sảnh chính'),
    (N'TEST02', N'Bàn thử 02', 1, 2, N'Sảnh chính'),
    (N'TEST03', N'Bàn thử 03', 0, 6, N'Sảnh chính'),
    (N'TEST04', N'Bàn thử 04', 2, 4, N'Sảnh chính'),
    (N'TEST05', N'Bàn thử 05', 0, 4, N'Sảnh sân vườn'),
    (N'TEST06', N'Bàn thử 06', 1, 8, N'Sảnh sân vườn'),
    (N'TEST07', N'Bàn thử 07', 0, 2, N'Sảnh sân vườn'),
    (N'TEST08', N'Bàn thử 08', 2, 6, N'Sảnh sân vườn'),
    (N'TEST09', N'Bàn thử 09', 0, 4, N'Sảnh tầng 2'),
    (N'TEST10', N'Bàn thử 10', 1, 6, N'Sảnh tầng 2'),
    (N'TEST11', N'Bàn thử 11', 0, 8, N'Sảnh tầng 2'),
    (N'TEST12', N'Bàn thử 12', 2, 2, N'Sảnh tầng 2')
) AS v(TableCode, Name, Status, Seats, HallName)
JOIN dbo.Hall h ON h.Name = v.HallName AND h.RestaurantID = @restaurantId
WHERE NOT EXISTS (SELECT 1 FROM dbo.[Table] t WHERE t.TableCode = v.TableCode);

-- 48 hóa đơn đã thanh toán trong 36 ngày gần đây; mỗi hóa đơn có 3 món.
-- Ngày gần nhất luôn là hôm nay để bộ lọc mặc định của Bai2 có kết quả.
DECLARE @i int = 1;
WHILE @i <= 48
BEGIN
    DECLARE @code nvarchar(20) = N'TEST' + RIGHT(N'00' + CONVERT(nvarchar(2), ((@i - 1) % 12) + 1), 2);
    DECLARE @tableId int = (SELECT TOP (1) ID FROM dbo.[Table] WHERE TableCode = @code ORDER BY ID);
    DECLARE @invoiceName nvarchar(100) = N'TEST - Hóa đơn ' + RIGHT(N'000' + CONVERT(nvarchar(3), @i), 3);
    DECLARE @accountName nvarchar(100) = CASE @i % 3 WHEN 0 THEN N'test_quanly' WHEN 1 THEN N'test_thungan' ELSE N'test_phucvu' END;
    DECLARE @invoiceDate datetime = DATEADD(hour, 11 + (@i % 10), DATEADD(day, -((@i - 1) % 36), CONVERT(datetime, CONVERT(date, GETDATE()))));
    DECLARE @discount int = CASE WHEN @i % 4 = 0 THEN 20000 WHEN @i % 4 = 1 THEN 10000 ELSE 0 END;
    DECLARE @tax int = CASE WHEN @i % 5 = 0 THEN 8000 ELSE 0 END;
    DECLARE @food1 int, @food2 int, @food3 int;
    DECLARE @price1 int, @price2 int, @price3 int;
    SELECT @food1 = ID, @price1 = Price FROM dbo.Food WHERE Name = N'Gà nướng mật ong';
    SELECT @food2 = ID, @price2 = Price FROM dbo.Food WHERE Name = CASE @i % 3 WHEN 0 THEN N'Cơm gà' WHEN 1 THEN N'Mì xào bò' ELSE N'Bún bò' END;
    SELECT @food3 = ID, @price3 = Price FROM dbo.Food WHERE Name = CASE @i % 3 WHEN 0 THEN N'Cà phê sữa' WHEN 1 THEN N'Nước ép cam' ELSE N'Nước ép táo' END;
    IF NOT EXISTS (SELECT 1 FROM dbo.Invoice WHERE Name = @invoiceName AND TableID = @tableId)
    BEGIN
        DECLARE @invoiceId int;
        INSERT dbo.Invoice (Name, TableID, Total, Discount, Tax, Status, AccountName, CheckoutDate)
        VALUES (@invoiceName, @tableId, @price1 + ((@i % 3) + 1) * @price2 + 2 * @price3 - @discount + @tax,
                @discount, @tax, 1, @accountName, @invoiceDate);
        SET @invoiceId = SCOPE_IDENTITY();
        INSERT dbo.InvoiceDetails (InvoiceID, FoodID, Amount)
        VALUES (@invoiceId, @food1, 1), (@invoiceId, @food2, (@i % 3) + 1), (@invoiceId, @food3, 2);
    END;
    SET @i += 1;
END;

-- Ba bàn có hóa đơn hiện tại; một số bàn khác chỉ có lịch sử đã thanh toán.
DECLARE @currentTable int = 1;
WHILE @currentTable <= 3
BEGIN
    DECLARE @currentCode nvarchar(20) = CASE @currentTable WHEN 1 THEN N'TEST01' WHEN 2 THEN N'TEST06' ELSE N'TEST10' END;
    DECLARE @currentTableId int = (SELECT TOP (1) ID FROM dbo.[Table] WHERE TableCode = @currentCode ORDER BY ID);
    DECLARE @currentName nvarchar(100) = N'TEST - Hóa đơn hiện tại ' + @currentCode;
    IF NOT EXISTS (SELECT 1 FROM dbo.Invoice WHERE Name = @currentName AND TableID = @currentTableId)
    BEGIN
        DECLARE @currentInvoiceId int;
        DECLARE @currentFoodId int = (SELECT TOP (1) ID FROM dbo.Food WHERE Name = N'Lẩu gà lá é' ORDER BY ID);
        DECLARE @currentDrinkId int = (SELECT TOP (1) ID FROM dbo.Food WHERE Name = N'Nước ép cam' ORDER BY ID);
        DECLARE @currentTotal int = (SELECT Price FROM dbo.Food WHERE ID = @currentFoodId)
                                  + 2 * (SELECT Price FROM dbo.Food WHERE ID = @currentDrinkId);
        INSERT dbo.Invoice (Name, TableID, Total, Discount, Tax, Status, AccountName, CheckoutDate)
        VALUES (@currentName, @currentTableId, @currentTotal, 0, 0, 0, N'test_thungan', GETDATE());
        SET @currentInvoiceId = SCOPE_IDENTITY();
        INSERT dbo.InvoiceDetails (InvoiceID, FoodID, Amount)
        VALUES (@currentInvoiceId, @currentFoodId, 1), (@currentInvoiceId, @currentDrinkId, 2);
    END;
    SET @currentTable += 1;
END;

COMMIT TRANSACTION;
SELECT N'Category' AS Bang, COUNT(*) AS SoDong FROM dbo.Category
UNION ALL SELECT N'Food', COUNT(*) FROM dbo.Food
UNION ALL SELECT N'Account', COUNT(*) FROM dbo.Account
UNION ALL SELECT N'RoleAccount', COUNT(*) FROM dbo.RoleAccount
UNION ALL SELECT N'Table', COUNT(*) FROM dbo.[Table]
UNION ALL SELECT N'Invoice', COUNT(*) FROM dbo.Invoice
UNION ALL SELECT N'InvoiceDetails', COUNT(*) FROM dbo.InvoiceDetails;
GO
