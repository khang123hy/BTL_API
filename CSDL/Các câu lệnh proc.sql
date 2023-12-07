use DienDan_Vinfast
go


--START-------------------Bảng table Topic

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

--END----------------POST

--------------------------------------------------------------

--------------------------------------------------------------
--END----------------TOPIC