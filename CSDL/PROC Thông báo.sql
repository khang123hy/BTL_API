
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
