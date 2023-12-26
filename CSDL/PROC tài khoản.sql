use DienDan_Vinfast
go



-------------------------------------------UPDATE
create proc sp_createLogin(    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255),
    @Email VARCHAR(255),
	        @Role varchar(20))
			as
			begin
			 insert into Users(AccountName, Password,Role,Email) values (@AccountName,@Password,@Role,@Email)
			 end

go
CREATE PROCEDURE CheckLogin (
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255))
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Topics 
    WHERE AccountName = @AccountName AND Password = @Password;
END

