use DienDan_Vinfast
go 
select * From Likes

create proc get_like_posts( @ID_Posts int)
as
begin
select*from Likes where   ID_Post = @ID_Posts
end
select*from Likes
alter proc get_like_user(@ID_User int,
@ID_Post int
)
as
begin
select*from Likes where ID_User = @ID_User and ID_Post = @ID_Post
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


----------------------search
select*from Likes

exec sp_search_like_posts
create PROC sp_search_like_posts
    @Keywords NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY ID_Post DESC) AS RowNumber, 
        k.ID_Post,
        k.ID_User,
        u.FullName,
        u.Avatar
    INTO #Results
    FROM Likes AS k
	inner join Users u on k.ID_User = u.ID_User
    WHERE (
            @Keywords = '' 
            OR k.ID_Post LIKE N'%' + @Keywords + '%'
          );

    DECLARE @RecordCount BIGINT;
    SELECT @RecordCount = COUNT(*) FROM #Results;

    SELECT 
        *,
        @RecordCount AS RecordCount
    FROM #Results;
    DROP TABLE #Results;
END;
GO


select*from Likes
create PROC Like_Create_thongbao(
    @ID_Post INT,
    @ID_User INT,
    @ID_User_Nhan INT,
    @Link NVARCHAR(MAX),
    @Title NVARCHAR(MAX)
)
AS
BEGIN
    DECLARE @ID_Likes INT;

    -- Thêm bình luận và lấy ID_Comment
    INSERT INTO Likes(ID_Post, ID_User)
    VALUES (@ID_Post, @ID_User);

    -- Lấy ID_Comment
    SET @ID_Likes = SCOPE_IDENTITY();

    -- Thêm thông báo với ID_Comment vừa lấy được
    INSERT INTO Notifications (ID_User_Tao, ID_User_Nhan, ID_Like_or_Comment, Note, Content, Link)
    VALUES (@ID_User, @ID_User_Nhan, @ID_Likes, 'Likes', @Title, @Link);
END;
GO


create proc sp_like_Delete_Notification
(@ID_Likes int)
as
begin
	delete from Likes where ID_Likes =@ID_Likes;
	delete from Notifications where ID_Like_or_Comment = @ID_Likes and Note = 'Likes';
end
go

delete
select*from Notifications
select*from Likes