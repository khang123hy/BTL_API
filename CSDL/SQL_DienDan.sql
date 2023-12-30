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
ALTER TABLE Topics
create COLUMN Image nvarchar(max);
-- Thêm cột Image
ALTER TABLE Topics
ADD Image NVARCHAR(MAX);
-- Tạo bảng Bài viết
CREATE TABLE Posts (
    ID_Post int PRIMARY KEY IDENTITY(1,1),
    ID_User INT,
    ID_Topic INT,
    Title NVARCHAR(150) NOT NULL,
    Content nvarchar(max) not null,
	Synopsis nvarchar(max),
    Image nvarchar(MAX),
    CreatedDate DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ID_User) REFERENCES Users(ID_User),
    FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic)
);
ALTER TABLE Posts
ALTER COLUMN  Image nvarchar(MAX);

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
    ID_Post INT,
    ID_User INT,
	PRIMARY KEY(ID_Post,ID_User),
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
);


-- Tạo bảng Thông báo (Notifications)
CREATE TABLE Notifications (
    ID_Notification INT PRIMARY KEY IDENTITY(1,1),
    ID_User_Tao INT,
    ID_User_Nhan INT,
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
--THÊM DỮ LIỆU VÀO CÁC BẢNG
-- Thêm dữ liệu vào bảng USERS
INSERT INTO USERS (FullName,Sex, Address, DateOfBirth, PhoneNumber,Email)
VALUES 
    ('Nguyen Van A','Nam', '123 Main Street', '1990-01-01', '1234567890','user1@email.com'),
    ('Tran Thi B',N'Nữ', '456 Oak Street', '1985-05-15', '0987654321', 'admin@email.com');
-- Chèn dữ liệu cho bảng Users
select * from Users
INSERT INTO Users (FullName, Address, Sex, DateOfBirth, PhoneNumber, Email)
VALUES 
    ('John Doe', '123 Main St', 'Male', '1990-01-01', '1234567890', '7john.doe@example.com'),
    ('Jane Smith', '456 Oak St', 'Female', '1985-05-15', '9876543210', '2jane.smith@example.com'),
    ('Bob Johnson', '789 Pine St', 'Male', '1982-09-20', '5551112222', '2bob.johnson@example.com'),
    ('Alice Brown', '101 Elm St', 'Female', '1995-03-12', '9998887777', '2alice.brown@example.com'),
    ('Charlie Wilson', '202 Cedar St', 'Male', '1988-11-28', '4443332222', '2charlie.wilson@example.com');

-- Chèn dữ liệu cho bảng Accounts
INSERT INTO Accounts (ID_User, AccountName, Password, Role)
VALUES 
    (4, 'john_doe_account', 'password123', 'USER'),
    (5, 'jane_smith_account', 'pass456', 'ADMIN'),
    (6, 'bob_johnson_account', 'secure789', 'USER'),
    (7, 'alice_brown_account', 'pass123', 'USER'),
    (8, 'charlie_wilson_account', 'password456', 'ADMIN');

-- Thêm dữ liệu vào bảng ACCOUNTS
INSERT INTO ACCOUNTS (ID_USER, AccountName, PASSWORD , Role)
VALUES 
    (1, 'user', '1',  'USER'), -- User
    (2, 'admin', '1', 'ADMIN'); -- Admin
	select*from ACCOUNTS
-- Thêm dữ liệu vào bảng Topics
INSERT INTO Topics (Title, Description, CreatedDate)
VALUES 
    ('Technology', 'Discussion about the latest technology trends.', GETDATE()),
    ('Travel', 'Share your travel experiences and recommendations.', GETDATE());

-- Thêm dữ liệu vào bảng Posts
INSERT INTO Posts (ID_User, ID_Topic, Title, Content, Image, CreatedDate)
VALUES 
    (1, 1, 'New Technology Advancements', 'Exciting developments in technology!', NULL, GETDATE()),
    (3, 2, 'Amazing Beaches Around the World', 'Share your favorite beach destinations.', NULL, GETDATE());

-- Thêm dữ liệu vào bảng Comments
INSERT INTO Comments (ID_Post, ID_User, Content, CreatedDate)
VALUES 
	    (2, 1, 'I have been to some amazing beache. Can not wait to share my experiences 2!', GETDATE()),
    (1, 3, 'Great post! I love the advancements mentioned.', GETDATE()),
    (2, 1, 'I have been to some amazing beache. Can not wait to share my experiences!', GETDATE());

-- Thêm dữ liệu vào bảng Likes
INSERT INTO Likes (ID_Post, ID_User)
VALUES 
    (1, 1),
    (2, 3);

-- Thêm dữ liệu vào bảng Notifications
INSERT INTO Notifications (ID_User_Tao,ID_User_Nhan, Content, NotificationDate)
VALUES 
    (1,3, 'You have a new like on your post!', GETDATE()),
    (3, 1,'Someone commented on your post.', GETDATE());

	select*from Notifications