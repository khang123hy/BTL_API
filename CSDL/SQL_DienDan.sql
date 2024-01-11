create database DienDan_Vinfast
go

use DienDan_Vinfast
GO

CREATE TABLE Users(
    ID_User INT PRIMARY KEY IDENTITY(1,1),
	AccountName varchar(50) not null UNIQUE,
    Password varchar(50) not null,
    FullName NVARCHAR(255),
    Address NVARCHAR(255),
	Sex Nvarchar(20),
    DateOfBirth date,
    PhoneNumber char(10),
	Email varchar(50) UNIQUE,
	Role varchar(20) not null CHECK(Role = 'ADMIN' or Role = 'USER'),
	CreatedDate DATETIME DEFAULT(GETDATE()),
	Avatar nvarchar(Max)
);    
-- Tạo bảng Chủ đề (Topics)
create TABLE Topics (
    ID_Topic INT PRIMARY KEY IDENTITY(1,1),
    Title nVARCHAR(255) NOT NULL, -- tieu de
    Description nvarchar(max), -- ghi chu
	Image NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT(GETDATE()) --Ngay tao
);
-- Sửa kiểu dữ liệu của cột Title từ VARCHAR(255) sang NVARCHAR(255)

CREATE TABLE Posts (
    ID_Post int PRIMARY KEY IDENTITY(1,1),
    ID_User INT,
    ID_Topic INT,
    Title NVARCHAR(150) NOT NULL,
    Synopsis nvarchar(max),
    CreatedDate DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ID_User) REFERENCES Users(ID_User) ON DELETE CASCADE,
    FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic) ON DELETE CASCADE
);

-- Thêm ON DELETE CASCADE cho khóa ngoại trên cột ID_Topic trong bảng Posts
ALTER TABLE Posts
DROP CONSTRAINT FK_Topic_Posts; -- Xóa ràng buộc khóa ngoại hiện tại (đổi tên ràng buộc nếu cần)
ALTER TABLE Posts
ADD CONSTRAINT FK_Topic_Posts FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic) ON DELETE CASCADE;


-- Tạo bảng Thích (Likes)
CREATE TABLE Likes (
    ID_Likes INT PRIMARY KEY IDENTITY(1,1),
    ID_Post INT,
    ID_User INT,
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
);
CREATE TABLE PostDetails (
    ID_Detail int PRIMARY KEY IDENTITY(1,1),
    ID_Post INT,
    Content nvarchar(max) NOT NULL,
    Image nvarchar(MAX),
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post) 
);

-- Tạo bảng Bình luận (Comments)
CREATE TABLE Comments (
    ID_Comment INT PRIMARY KEY IDENTITY(1,1),
    ID_Post INT,
    ID_User INT,
    Content nvarchar(max),
    CreatedDate DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
);
-- Tạo bảng Thích (Likes)
CREATE TABLE Likes (
    ID_Likes INT PRIMARY KEY IDENTITY(1,1),
    ID_Post INT,
    ID_User INT,
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
);

-- Tạo bảng Thông báo (Notifications)
CREATE TABLE Notifications (
    ID_Notification INT PRIMARY KEY IDENTITY(1,1),
    ID_User_Tao INT,
    ID_User_Nhan INT,
    ID_Like_or_Comment INT,
    Note nvarchar(max),
    Content nvarchar(max) NOT NULL,
    Link nvarchar(max),
    NotificationDate DATETIME default(getdate()),
    FOREIGN KEY (ID_User_Tao) REFERENCES USERS(ID_User)
);


drop table Notifications
DROP table Likes
drop TABLE Topics
DROP TABLE Posts
DROP TABLE Comments
DROP TABLE USERS
DROP TABLE ACCOUNTS
---------------------------------------------------------------------
