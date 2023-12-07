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
@FullName nvarchar(255),
@Address nvarchar(255),
@Sex nvarchar(20),
@DateOfBirth date,
@PhoneNumber char(10)
)
as
begin
INSERT INTO USERS (FullName, Address,Sex, DateOfBirth, PhoneNumber)
VALUES 
    (@FullName,@Address,@Sex,@DateOfBirth,@PhoneNumber);
end

create proc sp_user_update(
@ID_User int,
@FullName nvarchar(255),
@Address nvarchar(255),
@Sex nvarchar(20),
@DateOfBirth date,
@PhoneNumber char(10)
)
as
begin
update Users set FullName=@FullName, Address=@Address, Sex=@Sex, DateOfBirth=@DateOfBirth, PhoneNumber=@PhoneNumber 
where ID_User = @ID_User
end

create proc sp_user_delete(
@ID_User int)
as
begin
delete from Users
where ID_User = @ID_User
end