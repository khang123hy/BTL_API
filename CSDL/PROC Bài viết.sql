use DienDan_Vinfast
go

SELECT*FROM Topics

go

create PROCEDURE get_post_byid (@id int) --Gọi ra cơ sở dữ liệu bảng Posts
as
begin 
SELECT*FROM Posts WHERE ID_Post = @id
end

create PROCEDURE sp_posts_create(
@ID_User INT,
@ID_Topic INT,
@Title NVARCHAR(150),
@Content nvarchar(max) ,
@Image VARBINARY(MAX)
)
AS
    BEGIN
       insert into Posts(ID_User,ID_Topic,Title,Content,Image)
	   values(@ID_User,@ID_Topic,@Title,@Content,@Image);
    END;
GO


drop PROCEDURE Post_create
select*from Posts

create PROCEDURE Post_update(
@ID_Post INT,
@ID_User INT,
@ID_Topic INT,
@Title NVARCHAR(150),
@Content TEXT ,
@Image VARBINARY(MAX)
)
AS
    BEGIN
		update Posts set  ID_User = @ID_User, ID_Topic = @ID_Topic, Title = @Title, Content = @Content, Image = @Image where ID_Post = @ID_Post; 
    END;
GO
drop 
SELECT*from Posts