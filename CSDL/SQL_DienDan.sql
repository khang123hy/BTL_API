use DienDan_Vinfast
GO

CREATE TABLE Accounts(
        ID_Account int PRIMARY KEY IDENTITY(1,1),
        ID_User int FOREIGN KEY (ID_User) REFERENCES USERS(ID_User),
        AccountName varchar(50) not null UNIQUE,
        Password varchar(50) not null,
        Email varchar(50) not null UNIQUE,
        Role varchar(20) CHECK(Role = 'ADMIN' or Role = 'USER') --admin = 0; user = 1
)

CREATE TABLE Users(
    ID_User INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) not null,
	Sex Nvarchar(20) not null,
    DateOfBirth date,
    PhoneNumber char(10),
)
-- Tạo bảng Chủ đề (Topics)
CREATE TABLE Topics (
    ID_Topic INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(255) NOT NULL, -- tieu de
    Description TEXT, -- ghi chu
    CreatedDate DATETIME DEFAULT(GETDATE()) --Ngay tao
   
);
-- Tạo bảng Bài viết
CREATE TABLE Posts (
    ID_Post int PRIMARY KEY IDENTITY(1,1),
    ID_User INT,
    ID_Topic INT,
    Title NVARCHAR(150) NOT NULL,
    Content nvarchar(max) not null,
    Image VARBINARY(MAX),
    CreatedDate DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ID_User) REFERENCES Users(ID_User),
    FOREIGN KEY (ID_Topic) REFERENCES Topics(ID_Topic)
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
    ID_Post INT,
    ID_User INT,
	PRIMARY KEY(ID_Post,ID_User),
    FOREIGN KEY (ID_Post) REFERENCES Posts(ID_Post),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
);


-- Tạo bảng Thông báo (Notifications)
CREATE TABLE Notifications (
    ID_Notification INT PRIMARY KEY IDENTITY(1,1),
    ID_User INT,
    Content nvarchar(max) NOT NULL,
    NotificationDate DATETIME default(getdate()),
    FOREIGN KEY (ID_User) REFERENCES USERS(ID_User)
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
INSERT INTO USERS (FullName,Sex, Address, DateOfBirth, PhoneNumber)
VALUES 
    ('Nguyen Van A','Nam', '123 Main Street', '1990-01-01', '1234567890'),
    ('Tran Thi B',N'Nữ', '456 Oak Street', '1985-05-15', '0987654321');

-- Thêm dữ liệu vào bảng ACCOUNTS
INSERT INTO ACCOUNTS (ID_USER, AccountName, PASSWORD, Email, Role)
VALUES 
    (1, 'user', '1', 'user1@email.com', 'USER'), -- User
    (2, 'admin', '1', 'admin@email.com', 'ADMIN'); -- Admin
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
    (2, 2, 'Amazing Beaches Around the World', 'Share your favorite beach destinations.', NULL, GETDATE());

-- Thêm dữ liệu vào bảng Comments
INSERT INTO Comments (ID_Post, ID_User, Content, CreatedDate)
VALUES 
    (1, 2, 'Great post! I love the advancements mentioned.', GETDATE()),
    (2, 1, 'I have been to some amazing beache. Can not wait to share my experiences!', GETDATE());

-- Thêm dữ liệu vào bảng Likes
INSERT INTO Likes (ID_Post, ID_User)
VALUES 
    (1, 1),
    (2, 2);

-- Thêm dữ liệu vào bảng Notifications
INSERT INTO Notifications (ID_User, Content, NotificationDate)
VALUES 
    (1, 'You have a new like on your post!', GETDATE()),
    (2, 'Someone commented on your post.', GETDATE());

SELECT * from Notifications