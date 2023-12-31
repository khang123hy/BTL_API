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



ALTER PROCEDURE sp_posts_create(
@ID_User INT,
@ID_Topic INT,
@Title NVARCHAR(150),
@Content nvarchar(max) ,
@Synopsis nvarchar(max) ,
@Image nvarchar(MAX)
)
AS
    BEGIN
       insert into Posts(ID_User,ID_Topic,Title,Content,Synopsis,Image)
	   values(@ID_User,@ID_Topic,@Title,@Content,@Synopsis,@Image);
    END;
GO


alter PROCEDURE Post_update(
@ID_Post INT,
@ID_User INT,
@ID_Topic INT,
@Title NVARCHAR(150),
@Content TEXT ,
@Synopsis nvarchar(max) ,
@Image nvarchar(MAX)
)
AS
    BEGIN
		update Posts set  ID_User = @ID_User, ID_Topic = @ID_Topic, Title = @Title, Content = @Content,Synopsis=@Synopsis, Image = @Image where ID_Post = @ID_Post; 
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

--------------------------------------------search

alter PROCEDURE sp_Posts_search 
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
    ELSE
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Post DESC) AS RowNumber, 
            k.ID_Post,
            k.ID_User,
            k.ID_Topic,
            k.Title,
			k.Content,
			k.Image,
            k.CreatedDate
        INTO #Results2
        FROM Posts AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Post LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.ID_Topic LIKE N'%' + @Keywords + '%'
					OR k.Title LIKE N'%' + @Keywords + '%'
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

    IF (@page_size <> 0)
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
    ELSE
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
        INTO #Results2
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
        FROM #Results2;
        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
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

    IF (@page_size <> 0)
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
    ELSE
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
        INTO #Results2
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
        FROM #Results2;
        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
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

    IF (@page_size <> 0)
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
        INTO #Results2
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
        FROM #Results2;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
END;

GO

--Hiển thị theo từ lớn > bé 
alter PROCEDURE sp_Posts_search_by_topic_User_desc (
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255),
	@ID_Topic NVARCHAR(255),
    @OrderBy NVARCHAR(255))  
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    IF (@page_size <> 0)
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
        INTO #Results2
        FROM Posts AS k
		inner join Users u on u.ID_User = k.ID_User
		left join Comments c on k.ID_Post = c.ID_Post
		left join Likes l on l.ID_Post = k.ID_Post
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
        FROM #Results2;

        SELECT 
            *, 
            @RecordCount AS RecordCount
        FROM #Results2;                        

        DROP TABLE #Results1; 
    END;
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

alter PROCEDURE sp_Posts_create_list
(@ID_User              int, 
 @ID_Topic          int, 
 @Title         NVARCHAR(255),  
 @Synopsis         NVARCHAR(max),  
 @list_json_PostDetails NVARCHAR(MAX)
)
AS
    BEGIN
		DECLARE @ID_Post INT;

        INSERT INTO Posts
                (ID_User, 
                 ID_Topic, 
                 Title,
				 Synopsis
                )
                VALUES
                (@ID_User, 
                 @ID_Topic, 
                 @Title,
				 @Synopsis
                );

				SET @ID_Post = SCOPE_IDENTITY();
                IF(@list_json_PostDetails IS NOT NULL)
                    BEGIN
                        INSERT INTO PostDetails
						 (ID_Post, 
						  Content,
                          Image             
                        )
                    SELECT @PostID,
						JSON_VALUE(p.value, '$.content'), 
                            JSON_VALUE(p.value, '$.image')
                    FROM OPENJSON(@list_json_PostDetails) AS p;
                END;
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