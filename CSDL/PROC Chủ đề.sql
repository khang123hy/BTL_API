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
alter PROCEDURE Topic_create(
    @Title VARCHAR(255) ,
    @Description nvarchar(max),
    @Image nvarchar(max)
)
AS
    BEGIN
       insert into Topics(Title,Description,Image)
	   values(@Title,@Description,@Image);
    END;
GO


alter PROCEDURE sp_topic_create(
    @Title nvarchar(255) ,
    @Description nvarchar(max),
	    @Image nvarchar(max)

)
AS
    BEGIN
		insert into Topics(Title,Description,Image) values (@Title,@Description,@Image); 
    END;
GO

exec sp_topic_create @Title = 'Chăm sóc xe hơi', @Description ='Ko có gì'

select*from Topics

alter PROCEDURE sp_topic_update
(
   @ID_Topic INT,
    @Title nvarchar(255) ,
    @Description nvarchar(max),
    @Image nvarchar(max)
)
AS
    BEGIN
		update Topics set Title = @Title, Description = @Description,Image=@Image, CreatedDate =GETDATE()  where ID_Topic = @ID_Topic; 
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

alter PROCEDURE sp_Topic_search 
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
            ROW_NUMBER() OVER (ORDER BY ID_Topic DESC) AS RowNumber, 
            k.ID_Topic,
            k.Title,
            k.Description,
            k.Image,
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
            ROW_NUMBER() OVER (ORDER BY ID_Topic DESC) AS RowNumber, 
            k.ID_Topic,
            k.Title,
            k.Description,
            k.Image,
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
select*from Posts k inner join Users h on h.ID_User = k.ID_User