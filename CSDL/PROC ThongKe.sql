
-- Để lấy tổng số người dùng được tạo trong ngày cho một năm cụ thể (ví dụ: 2023)
EXEC sp_ThongKe_NguoiDungMoi 'Day', @Year = 2024;

--Thống kê người dùng mới
alter PROCEDURE sp_ThongKe_NguoiDungMoi
    @TimeFrame NVARCHAR(20),
    @Year INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME;
    DECLARE @EndDate DATETIME;

    -- Xác định năm cho khoảng thời gian
    SET @Year = COALESCE(@Year, YEAR(GETDATE()));

    -- Xác định khoảng thời gian tùy thuộc vào giá trị của @TimeFrame và @Year
    IF @TimeFrame = 'Day'
    BEGIN
        SET @StartDate = DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(DAY, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(WEEK, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Month'
    BEGIN
        SET @StartDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(MONTH, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Year'
    BEGIN
        SET @StartDate = DATEFROMPARTS(@Year, 1, 1);
        SET @EndDate = DATEFROMPARTS(@Year + 1, 1, 1);
    END

    -- Tính tổng số người dùng trong khoảng thời gian
    SELECT COUNT(*) AS NewUserCount
    FROM Users
    WHERE CreatedDate >= @StartDate AND CreatedDate < @EndDate;

END

select*from Comments
exec sp_BaiViet_BaiVietMoi @TimeFrame = 'Year' ,@Year =2023
--Thống kê bài đăng 
CREATE PROCEDURE sp_BaiViet_BaiVietMoi
    @TimeFrame NVARCHAR(20),
    @Year INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME;
    DECLARE @EndDate DATETIME;

    -- Xác định năm cho khoảng thời gian
    SET @Year = COALESCE(@Year, YEAR(GETDATE()));

    -- Xác định khoảng thời gian tùy thuộc vào giá trị của @TimeFrame và @Year
    IF @TimeFrame = 'Day'
    BEGIN
        SET @StartDate = DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(DAY, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(WEEK, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Month'
    BEGIN
        SET @StartDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(MONTH, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Year'
    BEGIN
        SET @StartDate = DATEFROMPARTS(@Year, 1, 1);
        SET @EndDate = DATEFROMPARTS(@Year + 1, 1, 1);
    END

    -- Tính tổng số bài đăng trong khoảng thời gian
    SELECT COUNT(*) AS NewPostCount
    FROM Posts
    WHERE CreatedDate >= @StartDate AND CreatedDate < @EndDate;

END

exec sp_ThongKe_TongNguoiDung
alter proc sp_ThongKe_TongNguoiDung
as
begin
select count(*) as TongNguoiDung from Users
end

select*from Users
exec sp_Comment_binhluanmoi  @TimeFrame = 'Day', @Year=2022
CREATE PROCEDURE sp_Comment_binhluanmoi
    @TimeFrame NVARCHAR(20),
    @Year INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME;
    DECLARE @EndDate DATETIME;

    -- Xác định năm cho khoảng thời gian
    SET @Year = COALESCE(@Year, YEAR(GETDATE()));

    -- Xác định khoảng thời gian tùy thuộc vào giá trị của @TimeFrame và @Year
    IF @TimeFrame = 'Day'
    BEGIN
        SET @StartDate = DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(DAY, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(WEEK, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Month'
    BEGIN
        SET @StartDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(MONTH, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Year'
    BEGIN
        SET @StartDate = DATEFROMPARTS(@Year, 1, 1);
        SET @EndDate = DATEFROMPARTS(@Year + 1, 1, 1);
    END

    -- Tính tổng số bài đăng trong khoảng thời gian
    SELECT COUNT(*) AS NewCommentCount
    FROM Comments
    WHERE CreatedDate >= @StartDate AND CreatedDate < @EndDate;

END

EXEC sp_TopLikedPosts 'Year' ,@Month = null, @Year=2024;
CREATE PROCEDURE sp_TopLikedPosts
    @TimeFrame NVARCHAR(20),
    @Year INT = NULL,
    @Month INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME;
    DECLARE @EndDate DATETIME;

    -- Xác định năm cho khoảng thời gian
    SET @Year = COALESCE(@Year, YEAR(GETDATE()));

    -- Xác định tháng cho khoảng thời gian (nếu cung cấp)
    SET @Month = COALESCE(@Month, MONTH(GETDATE()));

    -- Xác định khoảng thời gian tùy thuộc vào giá trị của @TimeFrame, @Year và @Month
    IF @TimeFrame = 'Day'
    BEGIN
        SET @StartDate = DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(DAY, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0);
        SET @EndDate = DATEADD(WEEK, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Month'
    BEGIN
        SET @StartDate = DATEFROMPARTS(@Year, @Month, 1);
        SET @EndDate = DATEADD(MONTH, 1, @StartDate);
    END
    ELSE IF @TimeFrame = 'Year'
    BEGIN
        SET @StartDate = DATEFROMPARTS(@Year, 1, 1);
        SET @EndDate = DATEFROMPARTS(@Year + 1, 1, 1);
    END;

    -- Tính lượt thích cho từng bài viết trong khoảng thời gian
    WITH LikedCounts AS (
        SELECT
            p.ID_Post,
            p.Title,
            COUNT(l.ID_Likes) AS LikeCount
        FROM
            Posts p
            LEFT JOIN Likes l ON p.ID_Post = l.ID_Post
        WHERE
            p.CreatedDate >= @StartDate AND p.CreatedDate < @EndDate
        GROUP BY
            p.ID_Post, p.Title
    )

    -- Lấy 5 bài viết có lượt thích nhiều nhất
    SELECT TOP 5
        ID_Post,
        Title,
        LikeCount
    FROM
        LikedCounts
    ORDER BY
        LikeCount DESC;

END

