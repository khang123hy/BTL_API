use DienDan_Vinfast
go

select*from Users
select*from Accounts
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
        a.Password AS MatKhau
    FROM Users u
    INNER JOIN Accounts a ON u.ID_User = a.ID_User
    WHERE a.AccountName = @AccountName AND a.Password = @Password;
END

