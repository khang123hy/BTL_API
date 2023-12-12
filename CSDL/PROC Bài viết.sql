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


create PROCEDURE sp_posts_delete(
   @ID_Post INT
)
AS
    BEGIN
		delete from Posts where ID_Post = @ID_Post;
    END;
GO

----------------------------------deletes 
CREATE PROCEDURE sp_post_deletes
(
    @list_post NVARCHAR(MAX)
)
AS
BEGIN
    IF (@list_post IS NOT NULL) 
    BEGIN
        -- Chèn dữ liệu vào bảng tạm 
        SELECT
            JSON_VALUE(p.value, '$.iD_Post') AS iD_Post
        INTO #Results 
        FROM OPENJSON(@list_post) AS p;

        -- Thực hiện xóa tài khoản dựa trên trường note và iduser
        DELETE A 
        FROM Posts A
        INNER JOIN #Results R ON A.ID_Post = R.iD_Post;
        
        DROP TABLE #Results;
    END;
END;

select*from Posts

