
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
