use DienDan_Vinfast
go

create PROCEDURE get_comment_id (@id int)
AS
BEGIN 
SELECT*FROM Comments WHERE ID_Comment = @id
END
go

create PROCEDURE get_comment_byid_post (@id int) --Gọi ra cơ sở dữ liệu bảng Posts
as
begin 
SELECT*FROM Comments
WHERE ID_Post = @id
end
go

CREATE PROC Comment_Create(
@ID_Post int,
@ID_User int,
@Content nvarchar(max))
as
begin
insert into Comments(ID_Post,ID_User,Content) values (@ID_Post,@ID_User,@Content);
end
go


CREATE PROC Comment_Update(
@ID_Comment int,
@ID_Post int,
@ID_User int,
@Content nvarchar(max))
as
begin
update Comments set ID_Post = @ID_Post, ID_User = @ID_User ,Content=@Content where ID_Comment = @ID_Comment
end
go



CREATE PROC Comment_Delete(
@ID_Comment int)
as
begin
delete from Comments where ID_Comment = @ID_Comment
end
go

------------------------------------------deletes
CREATE PROCEDURE sp_comment_deletes
(
    @list_comment NVARCHAR(MAX)
)
AS
BEGIN
    IF (@list_comment IS NOT NULL) 
    BEGIN
        -- Chèn dữ liệu vào bảng tạm 
        SELECT
            JSON_VALUE(p.value, '$.iD_Comment') AS iD_Comment
        INTO #Results 
        FROM OPENJSON(@list_comment) AS p;

        -- Thực hiện xóa tài khoản dựa trên trường note và iduser
        DELETE A 
        FROM Comments A
        INNER JOIN #Results R ON A.ID_Comment = R.iD_Comment;
        
        DROP TABLE #Results;
    END;
END;



----------------------------------------------------------search
select*from Comments

alter PROCEDURE sp_Comment_search 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    IF (@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Comment desc) AS RowNumber, 
            k.ID_Comment,
            k.ID_Post,
            k.ID_User,
			k.Content,
            k.CreatedDate
        INTO #Results1
        FROM Comments AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Comment LIKE N'%' + @Keywords + '%' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%'
					OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.Content LIKE N'%' + @Keywords + '%'
					OR k.CreatedDate LIKE N'%' + @Keywords + '%'

                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results1;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results1
        WHERE 
            RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
            OR @page_index = -1;

        DROP TABLE #Results1; 
    END
    ELSE
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Comment desc) AS RowNumber, 
            k.ID_Comment,
            k.ID_Post,
            k.ID_User,
			k.Content,
            k.CreatedDate
        INTO #Results2
        FROM Comments AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Comment LIKE N'%' + @Keywords + '%' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%'
					OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.Content LIKE N'%' + @Keywords + '%'
					OR k.CreatedDate LIKE N'%' + @Keywords + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results2;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
END;
GO


----------------------------------------------------------search-User
select*from Comments

alter PROCEDURE sp_Comment_search_User
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    IF (@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Comment desc) AS RowNumber, 
            k.ID_Comment,
            k.ID_Post,
            k.ID_User,
			k.Content,
            k.CreatedDate,
			u.FullName,
			u.Avatar
        INTO #Results1
        FROM Comments AS k
		INNER JOIN Users u on k.ID_User=u.ID_User
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results1;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results1
        WHERE 
            RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
            OR @page_index = -1;

        DROP TABLE #Results1; 
    END
    ELSE
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Comment desc) AS RowNumber, 
            k.ID_Comment,
            k.ID_Post,
            k.ID_User,
			k.Content,
            k.CreatedDate,
			u.FullName,
			u.Avatar
        INTO #Results2
             FROM Comments AS k
		INNER JOIN Users u on k.ID_User=u.ID_User
        WHERE  (
                             @Keywords = '' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results2;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
END;
GO

alter PROC Comment_Create_thongbao(
    @ID_Post INT,
    @ID_User INT,
    @ID_User_Nhan INT,
    @Link NVARCHAR(MAX),
    @Title NVARCHAR(MAX),
    @Content NVARCHAR(MAX)
)
AS
BEGIN
    DECLARE @ID_Comment INT;

    -- Thêm bình luận và lấy ID_Comment
    INSERT INTO Comments (ID_Post, ID_User, Content)
    VALUES (@ID_Post, @ID_User, @Content);

    -- Lấy ID_Comment
    SET @ID_Comment = SCOPE_IDENTITY();

    -- Thêm thông báo với ID_Comment vừa lấy được
    INSERT INTO Notifications (ID_User_Tao, ID_User_Nhan, ID_Like_or_Comment, Note, Content, Link)
    VALUES (@ID_User, @ID_User_Nhan, @ID_Comment, 'Comment', @Title, @Link);
END;
GO



create proc sp_comment_Delete_Notification
(@ID_Comment int)
as
begin
	delete from Comments where ID_Comment =@ID_Comment;
	delete from Notifications where ID_Like_or_Comment = @ID_Comment and Note = 'Comment';
end
go
select*from Comments
select*from Notifications