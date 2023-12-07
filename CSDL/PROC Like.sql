use DienDan_Vinfast
go 
select * From Likes

create proc get_like_posts( @ID_Posts int)
as
begin
select*from Likes where   ID_Post = @ID_Posts
end

create proc get_like_user(@ID_User int)
as
begin
select*from Likes where ID_User = @ID_User
end


create proc sp_like_create(@ID_Posts int,@ID_User int)
as
begin
	insert into Likes(ID_Post,ID_User) values (@ID_Posts,@ID_User)
end 

create proc sp_like_delete(@ID_Posts int,@ID_User int)
as
begin
	delete from  Likes where ID_Post = @ID_Posts and ID_User =@ID_User
end 