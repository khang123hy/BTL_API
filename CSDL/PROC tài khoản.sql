use DienDan_Vinfast
go

select*from Users
exec sp_check_AccountName @AccountName ='user', @Email ='b@gmail.com'
alter proc sp_check_AccountName
(@AccountName NVARCHAR(50),
@Email NVARCHAR(50)
)
as
begin
select count(*) as Check_Account from Users where AccountName = @AccountName or Email = @Email
end

-------------------------------------------UPDATE'
select*from Users
exec sp_createLogin @AccountName = 'user1', @Password= '1',@Email='user1',@FullName='hi',@Avatar='a',@Role='1'
alter PROCEDURE sp_createLogin
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255),
    @Email VARCHAR(255),
    @FullName NVARCHAR(255),
    @Avatar NVARCHAR(MAX),
    @Role VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(MAX);
    -- Kiểm tra xem AccountName đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Users WHERE AccountName = @AccountName)
    BEGIN
        SET @ErrorMessage = N'Trùng lặp tên tài khoản. Vui lòng chọn tên khác.';
        -- Lưu thông báo lỗi vào biến cục bộ
        SELECT @ErrorMessage AS ErrorMessage;
        RETURN;
    END
    -- Kiểm tra xem Email đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        SET @ErrorMessage = N'Trùng lặp Email. Vui lòng chọn Email khác.';
        -- Lưu thông báo lỗi vào biến cục bộ
        SELECT @ErrorMessage AS ErrorMessage;
        RETURN;
    END
    -- Thêm mới người dùng nếu không có trùng lặp
    INSERT INTO Users (AccountName, Password, Role, Email, FullName, Avatar)
    VALUES (@AccountName, @Password, @Role, @Email, @FullName, @Avatar);
    -- Lưu thông báo lỗi (nếu có) vào biến cục bộ (trong trường hợp có lỗi sau khi thêm mới)
    SELECT NULL AS ErrorMessage;
END;
GO






CREATE PROCEDURE CheckLogin (
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255))
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Topics 
    WHERE AccountName = @AccountName AND Password = @Password;
END

