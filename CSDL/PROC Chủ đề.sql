use DienDan_Vinfast
go

SELECT*FROM Topics

select*from Topics

create proc get_topic
(@id int)
as
	begin
		select*from Topics where ID_Topic = @id;
	end
go
create PROCEDURE Topic_create(
    @Title VARCHAR(255) ,
    @Description nvarchar(max)
)
AS
    BEGIN
       insert into Topics(Title,Description)
	   values(@Title,@Description);
    END;
GO


create PROCEDURE sp_topic_create(
    @Title nvarchar(255) ,
    @Description nvarchar(max)
)
AS
    BEGIN
		insert into Topics(Title,Description) values (@Title,@Description); 
    END;
GO
exec sp_topic_create @Title = 'Hê lô thôi', @Description ='Ko có gì'

select*from Topics
create PROCEDURE sp_topic_update(
   @ID_Topic INT,
    @Title nvarchar(255) ,
    @Description nvarchar(max)
)
AS
    BEGIN
		update Topics set Title = @Title, Description = @Description where ID_Topic = @ID_Topic; 
    END;
GO

create PROCEDURE sp_topic_delete(
   @ID_Topic INT
)
AS
    BEGIN
		delete from Topics where ID_Topic = @ID_Topic;
    END;
GO
select*from Topics


----------------------------------deletes 
CREATE PROCEDURE sp_topic_deletes
(
    @list_topic NVARCHAR(MAX)
)
AS
BEGIN
    IF (@list_topic IS NOT NULL) 
    BEGIN
        -- Chèn dữ liệu vào bảng tạm 
        SELECT
            JSON_VALUE(p.value, '$.iD_Topic') AS iD_Topic
        INTO #Results 
        FROM OPENJSON(@list_topic) AS p;

        -- Thực hiện xóa tài khoản dựa trên trường note và iduser
        DELETE A 
        FROM Topics A
        INNER JOIN #Results R ON A.ID_Topic = R.iD_Topic;
        
        DROP TABLE #Results;
    END;
END;

select*from Topics

---------------------------search
exec sp_Topic_search @page_index = 1, @page_size =10,@Keywords =''
go

CREATE PROCEDURE sp_Topic_search 
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
            ROW_NUMBER() OVER (ORDER BY Title ASC) AS RowNumber, 
            k.ID_Topic,
            k.Title,
            k.Description,
            k.CreatedDate
        INTO #Results1
        FROM Topics AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Topic LIKE N'%' + @Keywords + '%' 
                    OR k.Title LIKE N'%' + @Keywords + '%'
					OR k.Description LIKE N'%' + @Keywords + '%'
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
            ROW_NUMBER() OVER (ORDER BY Title ASC) AS RowNumber, 
            k.ID_Topic,
            k.Title,
            k.Description,
            k.CreatedDate
        INTO #Results2
        FROM Topics AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Topic LIKE N'%' + @Keywords + '%' 
                    OR k.Title LIKE N'%' + @Keywords + '%'
					OR k.Description LIKE N'%' + @Keywords + '%'
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
