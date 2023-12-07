use DienDan_Vinfast
go

SELECT*FROM Topics

go
create PROCEDURE Topic_create(
    @Title VARCHAR(255) ,
    @Description TEXT
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

drop PROCEDURE sp_topic_update