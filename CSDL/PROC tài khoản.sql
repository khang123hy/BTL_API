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

-------------------------------------------UPDATE
alter proc sp_createLogin(    @AccountName NVARCHAR(50),
    @Password NVARCHAR(255),
    @Email VARCHAR(255),
    @FullName NVARCHAR(255),
    @Avatar NVARCHAR(max),
	        @Role varchar(20))
			as
			begin
			 insert into Users(AccountName, Password,Role,Email,FullName,Avatar) values (@AccountName,@Password,@Role,@Email,@FullName,@Avatar)
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

