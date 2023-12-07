use DienDan_Vinfast
go

create PROCEDURE get_comment_id (@id int)
AS
BEGIN 
SELECT*FROM Comments WHERE ID_Comment = @id
END
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