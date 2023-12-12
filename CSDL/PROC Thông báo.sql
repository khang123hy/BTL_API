
use DienDan_Vinfast
go
-- select Notification
create proc sp_notification_by_id(@id int)
as
begin
	select*from Notifications where ID_Notification = @id
end
go

-- Update Notification
create proc sp_notification_update(
@ID_Notification int,
@ID_User int,
@Content nvarchar(max)
)
as
begin
update Notifications set  ID_User = @ID_User ,Content = @Content where ID_Notification = @ID_Notification;
end

go
--Insert Notification
create proc sp_notification_insert
(
@ID_User int,
@Content nvarchar(max)
)
as
begin
insert into Notifications (ID_User,Content) values (@ID_User,@Content);
end
go
drop proc sp_notification_update


go
create PROCEDURE sp_notification_delete(
   @id INT
)
AS
    BEGIN
		delete from Notifications where ID_Notification = @id;
    END;
GO

----------------------------------deletes 
CREATE PROCEDURE sp_notification_deletes
(
    @list_notification NVARCHAR(MAX)
)
AS
BEGIN
    IF (@list_notification IS NOT NULL) 
    BEGIN
        -- Chèn dữ liệu vào bảng tạm 
        SELECT
            JSON_VALUE(p.value, '$.iD_Notification') AS iD_Notification
        INTO #Results 
        FROM OPENJSON(@list_notification) AS p;

        -- Thực hiện xóa tài khoản dựa trên trường note và iduser
        DELETE A 
        FROM Notifications A
        INNER JOIN #Results R ON A.ID_Notification = R.iD_Notification;
        
        DROP TABLE #Results;
    END;
END;


-------------------------------------------------search
select*from Notifications

CREATE PROCEDURE sp_Notification_search 
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
            ROW_NUMBER() OVER (ORDER BY Content ASC) AS RowNumber, 
            k.ID_Notification,
            k.ID_User,
            k.Content,
            k.NotificationDate
        INTO #Results1
        FROM Notifications AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Notification LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.Content LIKE N'%' + @Keywords + '%'
					OR k.NotificationDate LIKE N'%' + @Keywords + '%'
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
            ROW_NUMBER() OVER (ORDER BY Content ASC) AS RowNumber, 
            k.ID_Notification,
            k.ID_User,
            k.Content,
            k.NotificationDate
        INTO #Results2
        FROM Notifications AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Notification LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User LIKE N'%' + @Keywords + '%'
					OR k.Content LIKE N'%' + @Keywords + '%'
					OR k.NotificationDate LIKE N'%' + @Keywords + '%'
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
