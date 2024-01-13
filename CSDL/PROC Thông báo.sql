
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
Select*from Users
Select*from Notifications
--Insert Notification
alter proc sp_notification_insert
(
@ID_User_Nhan int,
@Link nvarchar(max),
@ID_User_Tao int,
@ID_Like_or_Comment int,
@Note nvarchar(max),
@Content nvarchar(max)
)
as
begin
insert into Notifications (ID_User_Nhan,ID_User_Tao,Content,Link,ID_Like_or_Comment,Note) values (@ID_User_Nhan,@ID_User_Tao,@Content,@Link,@ID_Like_or_Comment,@Note);
end
go


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

alter PROCEDURE sp_Notification_search 
    @page_index INT, 
    @page_size INT,
    @Keywords NVARCHAR(255)
AS
BEGIN
    DECLARE @RecordCount BIGINT;

    BEGIN
        SET NOCOUNT ON;

        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID_Notification DESC) AS RowNumber, 
            k.ID_Notification,
            k.ID_User_Nhan,
            k.ID_User_Tao,
            k.Content,
            k.NotificationDate
        INTO #Results1
        FROM Notifications AS k
        WHERE  (
                    @Keywords = '' 
                    OR k.ID_Notification LIKE N'%' + @Keywords + '%' 
                    OR k.ID_User_Nhan LIKE N'%' + @Keywords + '%'
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
   
END;
GO


-----------------------user

select*from Notifications
exec sp_search_notification_by_user @Keywords =''

alter PROC sp_search_notification_by_user
    @Keywords NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY ID_Notification DESC) AS RowNumber, 
        k.ID_Notification,
        k.ID_User_Nhan,
        k.ID_User_Tao,
        k.Content,
        k.Link,
        u.Avatar,
		u.FullName,
        k.NotificationDate
    INTO #Results
    FROM Notifications AS k
	inner join Users u on k.ID_User_Tao = u.ID_User
    WHERE (
            @Keywords = '' 
            OR k.ID_User_Nhan LIKE N'%' + @Keywords + '%'
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


create proc sp_delete_notification(@ID_User int, @ID_Post int)
as
begin
	delete from Notifications where ID_Post = @ID_Post and ID_User =@ID_User
end
select*from Notifications