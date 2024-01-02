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
    FOREIGN KEY (ID_User) REFERENCES Users(ID_User),
    FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic)
);
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
drop table Notifications
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
--THÊM DỮ LIỆU VÀO CÁC BẢNG
-- Thêm dữ liệu vào bảng USERS
select*from Users
-- Thêm dữ liệu cho bảng Users
-- Insert data into the table
delete
INSERT INTO Users (AccountName, Password, FullName, Address, Sex, DateOfBirth, PhoneNumber, Email, Role, Avatar,CreatedDate)
VALUES 
('usera1', 'user', 'Jane Doe', '456 Second St', 'Female', '1992-03-15', '9876543211', 'user2@example.com', 'USER', 'USERS/01e96c3ce1bda1dfdd339a4999fd9248.jpg','2024-10-10'),
('usera2', 'user', 'Bob Johnson', '789 Third St', 'Male', '1988-08-22', '5551234567', 'user3@example.com', 'USER', 'USERS/01e96c3ce1bda1dfdd339a4999fd9248.jpg','2023-10-10'),
('admina1', 'admin', 'Admin Two', '789 Admin St', 'Male', '1980-05-10', '5559876543', 'admin2@example.com', 'ADMIN', 'USERS/01e96c3ce1bda1dfdd339a4999fd9248.jpg','2023-10-10'),
('usera3', 'user', 'Alice Smith', '101 Fourth St', 'Female', '1995-12-05', '1239876540', 'user4@example.com', 'USER', 'USERS/01e96c3ce1bda1dfdd339a4999fd9248.jpg','2023-12-28'),
('admina2', 'admin', 'Admin Three', '111 Admin St', 'Male', '1975-09-18', '9998887777', 'admin3@example.com', 'ADMIN', 'USERS/01e96c3ce1bda1dfdd339a4999fd9248.jpg','2023-2-10');

INSERT INTO Users (AccountName, Password, FullName, Address, Sex, DateOfBirth, PhoneNumber, Email, Role, Avatar)
VALUES 
    ('user1', 'password1', 'Người Dùng 1', 'Địa chỉ 1', 'Nam', '1990-01-01', '1234567890', 'user1@email.com', 'USER', 'avatar1.jpg'),
    ('user2', 'password2', 'Người Dùng 2', 'Địa chỉ 2', 'Nữ', '1995-02-02', '9876543210', 'user2@email.com', 'USER', 'avatar2.jpg'),
    ('admin1', 'adminpass1', 'Quản trị viên 1', 'Địa chỉ Admin 1', 'Nam', '1985-03-03', '1111111111', 'admin1@email.com', 'ADMIN', 'admin_avatar1.jpg'),
    ('admin2', 'adminpass2', 'Quản trị viên 2', 'Địa chỉ Admin 2', 'Nữ', '1980-04-04', '2222222222', 'admin2@email.com', 'ADMIN', 'admin_avatar2.jpg'),
    ('user3', 'password3', 'Người Dùng 3', 'Địa chỉ 3', 'Nam', '1992-05-05', '3333333333', 'user3@email.com', 'USER', 'avatar3.jpg');

-- Thêm dữ liệu cho bảng Topics
INSERT INTO Topics (Title, Description, Image, CreatedDate)
VALUES 
    ('Chủ đề 1', 'Mô tả chủ đề 1', 'topic_image1.jpg', GETDATE()),
    ('Chủ đề 2', 'Mô tả chủ đề 2', 'topic_image2.jpg', GETDATE()),
    ('Chủ đề 3', 'Mô tả chủ đề 3', 'topic_image3.jpg', GETDATE()),
    ('Chủ đề 4', 'Mô tả chủ đề 4', 'topic_image4.jpg', GETDATE()),
    ('Chủ đề 5', 'Mô tả chủ đề 5', 'topic_image5.jpg', GETDATE());

-- Thêm dữ liệu cho bảng Posts
INSERT INTO Posts (ID_User, ID_Topic, Title, Synopsis, CreatedDate)
VALUES 
    (1, 1, 'Bài đăng 1', 'Tóm tắt bài đăng 1', GETDATE()),
    (2, 2, 'Bài đăng 2', 'Tóm tắt bài đăng 2', GETDATE()),
    (3, 3, 'Bài đăng 3', 'Tóm tắt bài đăng 3', GETDATE()),
    (4, 4, 'Bài đăng 4', 'Tóm tắt bài đăng 4', GETDATE()),
    (5, 5, 'Bài đăng 5', 'Tóm tắt bài đăng 5', GETDATE());

-- Thêm dữ liệu cho bảng PostDetails
INSERT INTO PostDetails (ID_Post, Content, Image)
VALUES 
    (1, 'Nội dung bài đăng 1', 'image1.jpg'),
    (2, 'Nội dung bài đăng 2', 'image2.jpg'),
    (3, 'Nội dung bài đăng 3', 'image3.jpg'),
    (4, 'Nội dung bài đăng 4', 'image4.jpg'),
    (5, 'Nội dung bài đăng 5', 'image5.jpg');
select*from Posts
select*from Likes
-- Thêm dữ liệu cho bảng Comments
INSERT INTO Comments (ID_Post, ID_User, Content, CreatedDate)
VALUES 
    (5, 5, 'Bình luận 1 cho bài đăng 1', GETDATE()),
    (5, 6, 'Bình luận 2 cho bài đăng 2', GETDATE()),
    (5, 7, 'Bình luận 3 cho bài đăng 3', GETDATE()),
    (6, 8, 'Bình luận 4 cho bài đăng 4', GETDATE()),
    (7, 9, 'Bình luận 5 cho bài đăng 5', GETDATE());

	select*from Users
	select k.Title ,k.ID_Post, count(d.ID_Post) from Likes d left join Posts k on k.ID_Post=d.ID_Post group by k.Title,k.ID_Post
-- Thêm dữ liệu cho bảng Likes
INSERT INTO Likes (ID_Post, ID_User)
VALUES 
    (16, 6),
    (13, 8),
    (13, 7),
    (13, 6),
    (13, 5);

-- Thêm dữ liệu cho bảng Notifications
INSERT INTO Notifications (ID_User_Tao, ID_User_Nhan, Content, Link, NotificationDate)
VALUES 
    (1, 2, 'Thông báo 1', '/link1', GETDATE()),
    (2, 3, 'Thông báo 2', '/link2', GETDATE()),
    (3, 4, 'Thông báo 3', '/link3', GETDATE()),
    (4, 5, 'Thông báo 4', '/link4', GETDATE()),
    (5, 1, 'Thông báo 5', '/link5', GETDATE());


	select
	k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName,
            COUNT(DISTINCT c.ID_Comment) as Comment,
			COUNT(DISTINCT d.ID_Likes) as Likes
        FROM Posts AS k
        left JOIN Users u ON u.ID_User = k.ID_User
        left JOIN Comments c ON k.ID_Post = c.ID_Post
        left JOIN Likes d ON k.ID_Post = d.ID_Post
        GROUP BY
            k.ID_Post,
            u.ID_User,
            k.ID_Topic,
            k.Title,
            k.Synopsis,
            k.CreatedDate,
            u.Avatar,
            u.FullName;  

select*from Likes

select*from Users