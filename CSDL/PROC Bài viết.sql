use DienDan_Vinfast
go

SELECT*FROM Topics

go

create PROCEDURE get_post_byid (@id int) --Gọi ra cơ sở dữ liệu bảng Posts
as
begin 
SELECT*FROM Posts
WHERE ID_Post = @id
end
go


alter PROCEDURE get_post_byid_User (@id int)
as
begin 
SELECT
			k.ID_Post,
			k.ID_User,
            k.ID_Topic,
            k.Title as Title_Posts,
            k.CreatedDate,
			k.Synopsis,
			u.FullName,
			u.Avatar,
			t.Title as Title_Topic
FROM Posts k
inner join Users u on k.ID_User = u.ID_User
inner join Topics t on k.ID_Topic = t.ID_Topic
WHERE ID_Post = @id
end


create PROCEDURE sp_Posts_delete
(@ID_Post INT)
AS
BEGIN
    -- Xoá chi tiết bài viết
    DELETE FROM PostDetails WHERE ID_Post = @ID_Post;

    -- Xoá bài viết
    DELETE FROM Posts WHERE ID_Post = @ID_Post;
    SELECT '';
END;
GO



alter PROCEDURE sp_Posts_update_List
(@ID_Post              int, 
 @ID_User              int, 
 @ID_Topic             int, 
 @Title                NVARCHAR(255),  
 @Synopsis             NVARCHAR(max),  
 @list_json_PostDetails NVARCHAR(MAX)
)
AS
BEGIN
    UPDATE Posts
    SET
        ID_User = @ID_User,
        ID_Topic = @ID_Topic,
        Title = @Title,
        Synopsis = @Synopsis,
		CreatedDate = getdate()
    WHERE
        ID_Post = @ID_Post;

    -- Xoá các chi tiết bài viết cũ
    DELETE FROM PostDetails WHERE ID_Post = @ID_Post;

    -- Thêm các chi tiết bài viết mới
    IF(@list_json_PostDetails IS NOT NULL)
    BEGIN
        INSERT INTO PostDetails (ID_Post, Content, Image)
        SELECT @ID_Post,
               JSON_VALUE(p.value, '$.content'), 
               JSON_VALUE(p.value, '$.image')
        FROM OPENJSON(@list_json_PostDetails) AS p;
    END;

    SELECT '';
END;
GO


----------------------------------deletes 
alter PROCEDURE sp_post_deletes
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

        -- Thực hiện xóa chi tiết bài viết
        DELETE FROM PostDetails WHERE ID_Post IN (SELECT iD_Post FROM #Results);

        -- Thực hiện xóa bài viết
        DELETE FROM Posts WHERE ID_Post IN (SELECT iD_Post FROM #Results);

        DROP TABLE #Results;
    END;
END;


select*from Posts

--------------------------------------------search
alter PROCEDURE sp_Posts_search 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    BEGIN
        SET NOCOUNT ON;


        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Post DESC) AS RowNumber, 
            k.ID_Post,
            k.ID_User,
            k.ID_Topic,
            k.Title,
            k.CreatedDate
        INTO #Results1
        FROM Posts AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.ID_Topic LIKE N'%' + @Keywords + '%'
					OR k.Title LIKE N'%' + @Keywords + '%'
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
  
END;
GO

----------------------------------SEARCH THEO BÀI VIẾT USER
select*from Comments
select*from Users

alter PROCEDURE sp_Posts_search_User_Asc 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255),
	@OrderBy NVARCHAR(255) 
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    BEGIN
        SET NOCOUNT ON;

         SELECT 
            ROW_NUMBER() OVER (ORDER BY 
                CASE 
                    WHEN @OrderBy = 'Comment' THEN  COUNT(DISTINCT c.ID_Comment)
                    WHEN @OrderBy = 'Likes' THEN COUNT(DISTINCT l.ID_Likes)  
                    ELSE k.ID_Post 
                END ASC
            ) AS RowNumber, 
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName,
            COUNT(DISTINCT c.ID_Comment) as Comment,
            COUNT(DISTINCT l.ID_Likes) as Likes
        INTO #Results1
        FROM Posts AS k
        INNER JOIN Users u ON u.ID_User = k.ID_User
        LEFT JOIN Comments c ON k.ID_Post = c.ID_Post
        LEFT JOIN Likes l ON l.ID_Post = k.ID_Post
        WHERE (
            @Keywords = '' OR
            k.Title LIKE N'%' + @Keywords + '%'
        )
        GROUP BY
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName;                  

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
   
END;
GO
exec sp_Posts_search_User_Desc @page_index ='1',@page_size='12',@Keywords='',@OrderBy=''
alter PROCEDURE sp_Posts_search_User_Desc 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255),
	@OrderBy NVARCHAR(255) 
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    BEGIN
        SET NOCOUNT ON;

         SELECT 
            ROW_NUMBER() OVER (ORDER BY 
                CASE 
                 WHEN @OrderBy = 'Comment' THEN  COUNT(DISTINCT c.ID_Comment)
                    WHEN @OrderBy = 'Likes' THEN COUNT(DISTINCT l.ID_Likes)  
                    ELSE k.ID_Post 
                END DESC
            ) AS RowNumber, 
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName,
             COUNT(DISTINCT c.ID_Comment) as Comment,
			COUNT(DISTINCT l.ID_Likes) as Likes
        INTO #Results1
        FROM Posts AS k
        INNER JOIN Users u ON u.ID_User = k.ID_User
        LEFT JOIN Comments c ON k.ID_Post = c.ID_Post
        LEFT JOIN Likes l ON l.ID_Post = k.ID_Post
        WHERE (
            @Keywords = '' OR
            k.Title LIKE N'%' + @Keywords + '%'
        )
        GROUP BY
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName;                  

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
END;
GO

-------------------------------------------------Topic
exec sp_Posts_search_by_topic_User_Desc @page_index=1, @page_size=12,@Keywords='',@OrderBy='',@ID_Topic='9'

--Hiển thị theo từ  bé >lớn 
ALTER PROCEDURE sp_Posts_search_by_topic_User_ASC (
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255),
    @ID_Topic NVARCHAR(255),
    @OrderBy NVARCHAR(255))  
AS
BEGIN

    DECLARE @RecordCount BIGINT;

    BEGIN
        SET NOCOUNT ON;


        SELECT 
            ROW_NUMBER() OVER (ORDER BY 
                CASE 
                    WHEN @OrderBy = 'Comment' THEN  COUNT(DISTINCT c.ID_Comment)
                    WHEN @OrderBy = 'Likes' THEN COUNT(DISTINCT l.ID_Likes)  
                    ELSE k.ID_Post 
                END ASC
            ) AS RowNumber, 
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName,
            COUNT(DISTINCT c.ID_Comment) as Comment,
			COUNT(DISTINCT l.ID_Likes) as Likes
        INTO #Results
        FROM Posts AS k
        INNER JOIN Users u ON u.ID_User = k.ID_User
        LEFT JOIN Comments c ON k.ID_Post = c.ID_Post
        LEFT JOIN Likes l ON l.ID_Post = k.ID_Post
        WHERE (
            @Keywords = '' OR
            k.Title LIKE N'%' + @Keywords + '%'
        )
        AND (
            @ID_Topic = '' OR
            k.ID_Topic LIKE N'%' + @ID_Topic + '%'
        )
        GROUP BY
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName;

        SELECT @RecordCount = COUNT(*)
        FROM #Results;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results
        WHERE 
            RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
            OR @page_index = -1;

        DROP TABLE #Results; 
    END
END;

GO

-- Stored procedure để tìm kiếm bài viết theo chủ đề, người dùng, sắp xếp theo thứ tự giảm dần
ALTER PROCEDURE sp_Posts_search_by_topic_User_desc (
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255),
	@ID_Topic NVARCHAR(255),
    @OrderBy NVARCHAR(255))  
AS
BEGIN
    -- Biến để lưu trữ tổng số bản ghi
    DECLARE @RecordCount BIGINT;

    BEGIN
        -- Tắt in thông báo số bản ghi ảnh hưởng
        SET NOCOUNT ON;

        -- Tạo bảng tạm #Results1 để lưu trữ kết quả tìm kiếm
        SELECT 
            ROW_NUMBER() OVER (ORDER BY 
				--Quyết đinh sắp xếp dữ liệu dựa vào @OrderBy
                CASE 
					--Sắp xếp giảm dần theo số bình luận hoặc 
                    WHEN @OrderBy = 'Comment' THEN  COUNT(DISTINCT c.ID_Comment)
                    WHEN @OrderBy = 'Likes' THEN COUNT(DISTINCT l.ID_Likes)  
                    ELSE k.ID_Post 
                END DESC
            ) AS RowNumber, 
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
			k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName,
            COUNT(DISTINCT c.ID_Comment) as Comment,
			COUNT(DISTINCT l.ID_Likes) as Likes
        INTO #Results1
        FROM Posts AS k
        INNER JOIN Users u ON u.ID_User = k.ID_User
        LEFT JOIN Comments c ON k.ID_Post = c.ID_Post
        LEFT JOIN Likes l ON l.ID_Post = k.ID_Post
        WHERE (
            @Keywords = '' OR
            k.Title LIKE N'%' + @Keywords + '%'
        )
        AND (
            @ID_Topic = '' OR
            k.ID_Topic LIKE N'%' + @ID_Topic + '%'
        )
        GROUP BY
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
			k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName;

        -- Lấy tổng số bản ghi từ #Results1
        SELECT @RecordCount = COUNT(*)
        FROM #Results1;

        -- Trả về kết quả phân trang và tổng số bản ghi
        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results1
        WHERE 
            RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
            OR @page_index = -1;

        -- Xóa bảng tạm #Results1 khi không cần thiết
        DROP TABLE #Results1; 
    END
END;
GO


----------------------------------
CREATE TABLE Posts (
    ID_Post int PRIMARY KEY IDENTITY(1,1),
    ID_User INT,
    ID_Topic INT,
    Title NVARCHAR(255) NOT NULL,
    Synopsis nvarchar(max),
    CreatedDate DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ID_User) REFERENCES Users(ID_User),
    FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic)
);
CREATE TABLE PostDetails (
    ID_Detail int PRIMARY KEY IDENTITY(1,1),
    ID_Post INT,
    Content nvarchar(max) NOT NULL,
    Image nvarchar(MAX),
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post)
);

-- Stored procedure để tạo mới bài viết và chi tiết bài viết
ALTER PROCEDURE sp_Posts_create_list
(
    @ID_User INT, 
    @ID_Topic INT, 
    @Title NVARCHAR(255),  
    @Synopsis NVARCHAR(MAX),  
    @list_json_PostDetails NVARCHAR(MAX)
)
AS
BEGIN
    -- Khai báo biến cục bộ để lưu trữ ID của bài viết mới
    DECLARE @ID_Post INT;

    -- Chèn bài viết mới vào bảng Posts
    INSERT INTO Posts (ID_User, ID_Topic, Title, Synopsis)
    VALUES (@ID_User, @ID_Topic, @Title, @Synopsis);

    -- Lấy ID của bài viết vừa được thêm mới
    SET @ID_Post = SCOPE_IDENTITY();

    -- Kiểm tra xem chuỗi JSON chi tiết bài viết có giá trị không
    IF (@list_json_PostDetails IS NOT NULL)
    BEGIN
        -- Chèn chi tiết bài viết từ chuỗi JSON vào bảng PostDetails
        INSERT INTO PostDetails (ID_Post, Content, Image)
        SELECT 
            @ID_Post, -- ID của bài viết mới
            JSON_VALUE(p.value, '$.content'), -- Giá trị 'content' từ JSON
            JSON_VALUE(p.value, '$.image') -- Giá trị 'image' từ JSON
        FROM OPENJSON(@list_json_PostDetails) AS p;
    END;

    -- Trả về một chuỗi rỗng sau khi thực hiện thủ tục
    SELECT '';
END;
GO


---------------------------------Hiển thị chi tiết bài viết
select*from PostDetails

exec sp_search_PostDetails_by_posts @Keywords ='7'


alter PROC sp_search_PostDetails_by_posts
    @Keywords NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY ID_Detail ASC) AS RowNumber, 
        k.ID_Detail,
        k.ID_Post,
        k.Content,
        k.Image
    INTO #Results
    FROM PostDetails AS k
    WHERE (
            @Keywords = '' 
            OR k.ID_Post = @Keywords
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



exec sp_Posts_search_list @page_index = '1' ,@page_size ='10',@Keywords=''
ALTER PROCEDURE sp_Posts_search_list 
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
            ROW_NUMBER() OVER (ORDER BY p.ID_Post DESC) AS RowNumber, 
            p.ID_Post,
            p.ID_User,
            p.ID_Topic,
            p.Title,
			p.Synopsis,
            p.CreatedDate,
            (
                SELECT pd.Content , pd.Image 
                FROM PostDetails AS pd
                WHERE p.ID_Post = pd.ID_Post
                FOR JSON PATH
            ) AS list_json_posts
        INTO #Results1
        FROM Posts AS p
        WHERE  (
                    @Keywords = '' 
                    OR CAST(p.ID_Post AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%' 
                    OR CAST(p.ID_User AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%'
                    OR CAST(p.ID_Topic AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%'
                    OR p.Title LIKE N'%' + @Keywords + '%'
                    OR CONVERT(NVARCHAR(255), p.CreatedDate, 121) LIKE N'%' + @Keywords + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results1;

        SELECT 
            ID_Post,
            ID_User,
            ID_Topic,
            Title,
			Synopsis,
            CreatedDate,
            list_json_posts,
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
            ROW_NUMBER() OVER (ORDER BY p.ID_Post DESC) AS RowNumber, 
            p.ID_Post,
            p.ID_User,
            p.ID_Topic,
            p.Title,
			p.Synopsis,
            p.CreatedDate,
            (
                SELECT pd.Content , pd.Image 
                FROM PostDetails AS pd
                WHERE p.ID_Post = pd.ID_Post
                FOR JSON PATH
            ) AS list_json_posts
        INTO #Results2
        FROM Posts AS p
        WHERE  (
                    @Keywords = '' 
                    OR CAST(p.ID_Post AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%' 
                    OR CAST(p.ID_User AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%'
                    OR CAST(p.ID_Topic AS NVARCHAR(255)) LIKE N'%' + @Keywords + '%'
                    OR p.Title LIKE N'%' + @Keywords + '%'
                    OR CONVERT(NVARCHAR(255), p.CreatedDate, 121) LIKE N'%' + @Keywords + '%'
                );                   

        SELECT @RecordCount = COUNT(*)
        FROM #Results2;

        SELECT 
            ID_Post,
            ID_User,
            ID_Topic,
            Title,
			Synopsis,
            CreatedDate,
            list_json_posts,
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results2; 
    END;
END;
GO
