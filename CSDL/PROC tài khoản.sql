use DienDan_Vinfast
go

select*from Users
select*from Accounts
go
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

exec sp_login @accountname = admin, @password = 1

drop PROCEDURE sp_login