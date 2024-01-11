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
select*from Topics

alter PROCEDURE Topic_create(
    @Title VARCHAR(255) ,
    @Description nvarchar(max),
    @Image nvarchar(max)
)
AS
    BEGIN
		  SET NOCOUNT ON;
		DECLARE @ErrorMessage NVARCHAR(MAX);
		-- Kiểm tra xem tiêu đề đã tồn tại chưa
		IF EXISTS (SELECT 1 FROM Topics WHERE Title = @Title)
		BEGIN
			SET @ErrorMessage = N'Tên chủ đề đã tồn vui lòng tạo tên chủ đề khác.';
			-- Lưu thông báo lỗi vào biến cục bộ
			SELECT @ErrorMessage AS ErrorMessage;
			RETURN;
		END
		--Thêm dữ liệu vào
		   insert into Topics(Title,Description,Image)
		   values(@Title,@Description,@Image);
		-- Lưu thông báo lỗi (nếu có) vào biến cục bộ (trong trường hợp có lỗi sau khi thêm mới)
		SELECT NULL AS ErrorMessage;
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
    -- Kiểm tra xem tham số đầu vào có khác NULL hay không
    IF (@list_topic IS NOT NULL) 
    BEGIN
        -- Phân tích chuỗi JSON và lưu trữ ID_Topic vào bảng tạm #Results
        SELECT
            JSON_VALUE(p.value, '$.iD_Topic') AS iD_Topic
        INTO #Results 
        FROM OPENJSON(@list_topic) AS p;

        -- Xóa các bản ghi trong bảng Topics có ID_Topic trùng với #Results
        DELETE A 
        FROM Topics A
        INNER JOIN #Results R ON A.ID_Topic = R.iD_Topic;
        
        -- Giải phóng tài nguyên bảng tạm
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