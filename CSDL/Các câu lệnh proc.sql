use DienDan_Vinfast
go


--START-------------------Bảng table Topic
CREATE PROCEDURE [dbo].[get_topic] (@id int)
AS 
BEGIN 
SELECT*FROM Topics WHERE ID_Topic = @id
END;

exec get_topic @id = 1
go
--END-------------------Bảng table Topic
-------------------------------------------------

--START-------------------Bảng table Posts
CREATE PROCEDURE get_post_byid (@id int) --Gọi ra cơ sở dữ liệu bảng Posts
as
begin 
SELECT*FROM Posts WHERE ID_Post = @id
end
exec get_post_byid @id =1
--END-------------------Bảng table Posts
-------------------------------------------------

--START-------------------Bảng table Comment
GO
create PROCEDURE get_comment_id (@id int)
AS
BEGIN 
SELECT*FROM Comments WHERE ID_Comment = @id
END
exec get_comment_id @id = 2

--END-------------------Bảng table Comment
-------------------------------------------------
GO
create PROCEDURE get_like_id (@id int)
AS
BEGIN 
SELECT*FROM Likes WHERE ID_Like = @id
END
SELECT*FROM USERS


go
create PROCEDURE user_create(
@USER_NAME CHAR(20) ,
@PASSWORD CHAR(20) ,
@Email CHAR(50),
@FullName NVARCHAR(50),
@Role int
)
AS
    BEGIN
       insert into USERS(USER_NAME,PASSWORD,Email,FullName,Role)
	   values(@USER_NAME,@PASSWORD,@Email,@FullName,@Role);
    END;
GO

create PROCEDURE user_update(
@ID_User int,
@USER_NAME CHAR(20),
@PASSWORD CHAR(20) ,
@Email CHAR(50),
@FullName NVARCHAR(50),
@Role int
)
AS
    BEGIN
		update USERS set USER_NAME = @USER_NAME, PASSWORD = @PASSWORD, Email = @Email, FullName = @FullName, Role = @Role where ID_User = @ID_User; 
    END;
GO

--END ------------------USER
------------------------------------------------------------

--------------------------------------------------------------
--START----------------POST
SELECT*FROM Topics

go
create PROCEDURE Post_create(
@ID_User INT,
@ID_Topic INT,
@Title NVARCHAR(150),
@Content TEXT ,
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
--END----------------POST

--------------------------------------------------------------
--START----------------TOPIC
SELECT*FROM Topics

go
create PROCEDURE Topic_create(
    @Title VARCHAR(255) ,
    @Description TEXT
)
AS
    BEGIN
       insert into Topics(Title,Description)
	   values(@Title,@Description);
    END;
GO
drop PROCEDURE Topic_create
select*from Posts

create PROCEDURE Topic_update(
   @ID_Topic INT,
    @Title VARCHAR(255) ,
    @Description TEXT
)
AS
    BEGIN
		update Topics set @Title = @Title, Description = @Description where ID_Topic = @ID_Topic; 
    END;
GO

--------------------------------------------------------------
--END----------------TOPIC