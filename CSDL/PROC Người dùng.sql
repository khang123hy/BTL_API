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

--------------------------------------Tìm kiếm
select*from Users

exec sp_user_search @page_index = 1, @page_size =10,@FullName='', @Email = N'doe'

CREATE PROCEDURE sp_user_search 
    @page_index INT, 
    @page_size INT,
    @Search_All NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    IF (@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY FullName ASC) AS RowNumber, 
            k.ID_User,
            k.FullName,
            k.Email,
            k.Address,
            k.DateOfBirth,
            k.Sex,
            k.PhoneNumber
        INTO #Results1
        FROM Users AS k
        WHERE  (
                    @Search_All = '' 
                    OR k.FullName LIKE N'%' + @Search_All + '%' 
                    OR k.Email LIKE N'%' + @Search_All + '%'
					OR k.PhoneNumber LIKE N'%' + @Search_All + '%'
					OR k.DateOfBirth LIKE N'%' + @Search_All + '%'
					OR k.Sex LIKE N'%' + @Search_All + '%'
					OR k.Address LIKE N'%' + @Search_All + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results1;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results1
        WHERE 
            RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
            OR @page_index = -1;

        DROP TABLE #Results1; 
    END
    ELSE
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY FullName ASC) AS RowNumber, 
            k.ID_User,
            k.FullName,
            k.Email,
            k.Address,
            k.DateOfBirth,
            k.Sex,
            k.PhoneNumber
        INTO #Results2
        FROM Users AS k
        WHERE  (
                    @Search_All = '' 
                    OR k.FullName LIKE N'%' + @Search_All + '%' 
                    OR k.Email LIKE N'%' + @Search_All + '%'
					OR k.PhoneNumber LIKE N'%' + @Search_All + '%'
					OR k.DateOfBirth LIKE N'%' + @Search_All + '%'
					OR k.Sex LIKE N'%' + @Search_All + '%'
					OR k.Address LIKE N'%' + @Search_All + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results2;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
END;
GO
