use DienDan_Vinfast
go

select*from Users
select*from Accounts
select*from Notifications
go

------------------------------------ LOGIN
CREATE PROCEDURE sp_login
    @accountname varchar(50),
    @password varchar(50)
AS
BEGIN
    SELECT*
    FROM
     Accounts t
	  left join 
	  Users n on t.ID_User = n.ID_User
    WHERE
        AccountName = @accountname AND Password = @password;
END


-------------------------------------------UPDATE

create proc sp_account_update(
       @ID_User int,
        @AccountName varchar(50),
        @Password varchar(50),
        @Role varchar(20)
)
as
begin
	update Accounts set AccountName = @AccountName, Password=@Password, Role = @Role where ID_User = @ID_User
end
go

-----------------------------Delete
create proc sp_account_delete(
@ID_User int
)
as
	begin
		delete from Accounts where ID_User = @ID_User;
	end
go
------------------------------DELETES
drop PROCEDURE sp_account_deletes

CREATE PROCEDURE sp_account_deletes
(
    @list_json_ID_User NVARCHAR(MAX)
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
        FROM Accounts A
        INNER JOIN #Results R ON A.ID_User = R.iD_User;
        
        DROP TABLE #Results;
    END;
END;



select*from Accounts
select*from Users

drop PROCEDURE CheckLogin
CREATE PROCEDURE CheckLogin
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.ID_User,
        u.FullName,
        u.Sex,
        u.Address,
        u.DateOfBirth,
        u.PhoneNumber,
        u.Email,
        a.AccountName,
        a.Role,
        a.Password 
    FROM Users u
    INNER JOIN Accounts a ON u.ID_User = a.ID_User
    WHERE a.AccountName = @AccountName AND a.Password = @Password;
END

-------------------------------------------------------------SEARCH
select*from Accounts
CREATE PROCEDURE sp_Account_search 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    IF (@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY Role ASC) AS RowNumber, 
            k.ID_Account,
            k.ID_User,
            k.AccountName,
            k.Password,
			k.Role
        INTO #Results1
        FROM Accounts AS k
        WHERE (
                    @Keywords = '' 
                    OR k.ID_Account LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.AccountName LIKE N'%' + @Keywords + '%'
					OR k.Password LIKE N'%' + @Keywords + '%'
					OR k.Role LIKE N'%' + @Keywords + '%'

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
            ROW_NUMBER() OVER (ORDER BY Role ASC) AS RowNumber, 
            k.ID_Account,
            k.ID_User,
            k.AccountName,
            k.Password,
			k.Role
        INTO #Results2
        FROM Accounts AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Account LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.AccountName LIKE N'%' + @Keywords + '%'
					OR k.Password LIKE N'%' + @Keywords + '%'
					OR k.Role LIKE N'%' + @Keywords + '%'
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

