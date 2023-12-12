use DienDan_Vinfast
go
select*from Users
go
create proc sp_user_getid(@id int)
as
begin
select*from Users where ID_User = @id
end

go
create proc sp_user_create(
    @FullName NVARCHAR(255),
    @Address NVARCHAR(255),
    @Sex NVARCHAR(20),
    @DateOfBirth DATE,
    @PhoneNumber CHAR(10),
    @AccountName VARCHAR(50),
    @Password VARCHAR(50),
    @Email VARCHAR(50),
    @Role VARCHAR(20))
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    DECLARE @UserID INT;

    -- Insert into Users table
    INSERT INTO Users (FullName, Address, Sex, DateOfBirth, PhoneNumber, Email)
    VALUES (@FullName, @Address, @Sex, @DateOfBirth, @PhoneNumber, @Email);

    SET @UserID = SCOPE_IDENTITY(); -- Get the recently inserted ID_User

    -- Insert into Accounts table using the obtained UserID
    INSERT INTO Accounts (ID_User, AccountName, Password, Role)
    VALUES (@UserID, @AccountName, @Password, @Role);

    -- Commit the transaction if both inserts are successful
    COMMIT TRANSACTION;
END;
go


-----------------UPDATE
create proc sp_user_update(
@ID_User int,
@FullName nvarchar(255),
@Address nvarchar(255),
@Sex nvarchar(20),
@DateOfBirth date,
@PhoneNumber char(10),
@Email varchar(50)
)
as
begin
update Users set FullName=@FullName, Address=@Address, Sex=@Sex, DateOfBirth=@DateOfBirth, PhoneNumber=@PhoneNumber, Email = @Email
where ID_User = @ID_User
end

go
create proc sp_user_delete(
@ID_User int)
as
begin
delete from Users
where ID_User = @ID_User
end


-------------------------------DELETES
drop PROCEDURE sp_user_deletes


CREATE PROCEDURE sp_user_deletes
(
    @list_json_ID_User varchar(255)
)
AS
BEGIN
    IF (@list_json_ID_User IS NOT NULL) 
    BEGIN
        -- Chèn dữ liệu vào bảng tạm 
        SELECT
            JSON_VALUE(p.value, '$.iD_User') AS iD_User

        INTO #Results 
        FROM OPENJSON(@list_json_ID_User) AS p;

        -- Thực hiện xóa tài khoản dựa trên trường note và iduser
        DELETE A 
        FROM Users A
        INNER JOIN #Results R ON A.ID_User = R.iD_User;
		
        DROP TABLE #Results;
    END;
END;