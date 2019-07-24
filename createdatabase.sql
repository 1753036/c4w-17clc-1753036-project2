CREATE DATABASE university;
GO

USE university;

CREATE TABLE class
(
	id CHAR(5)  NOT NULL PRIMARY KEY,
)
GO

CREATE TABLE student
(
	id CHAR(7) PRIMARY KEY,
	fullname NVARCHAR(100) NOT NULL,
	gender CHAR NOT NULL CHECK(
		gender = 'M' OR
		gender = 'F' OR
		gender = 'E'
	),
	social_id CHAR(9) NOT NULL,
	class_id CHAR(5) NOT NULL,
	FOREIGN KEY(class_id) REFERENCES class(id)
);
GO

CREATE TABLE course
(
	id CHAR(6)  NOT NULL PRIMARY KEY,
	fullname NVARCHAR(100) NOT NULL,
	room CHAR(3) NOT NULL
);
GO

CREATE TABLE grade
(
	class_id CHAR(5) NOT NULL,
	course_id CHAR(6) NOT NULL,
	student_id CHAR(7) NOT NULL,
	midterm FLOAT NOT NULL,
	final FLOAT NOT NULL,
	other FLOAT NOT NULL,
	total FLOAT NOT NULL,

	PRIMARY KEY(class_id, course_id, student_id),
	FOREIGN KEY(class_id) REFERENCES class(id),
	FOREIGN KEY(course_id) REFERENCES course(id),
	FOREIGN KEY(student_id) REFERENCES student(id)
)
GO

CREATE TABLE auth
(
	username VARCHAR(50) NOT NULL PRIMARY KEY,
	passwd VARCHAR(50) NOT NULL,
	permission CHAR NOT NULL CHECK (
		permission = 'S' OR
		permission = 'M'
	)
)
GO

--INSERT INTO class
--VALUES	('17HCB'),
--		('18HCB')

--INSERT INTO student
--VALUES	('1753001', 'Nguyen Van A', 'M', '123456789', '17HCB'),
--		('1753002', 'Tran Van B', 'E', '123456789', '17HCB'),
--		('1753003', 'Nguyen Van C', 'F', '123456789', '17HCB'),
--		('1753004', 'Nguyen Van D', 'E', '123456789', '17HCB'),
--		('1753005', 'Nguyen Van E', 'F', '123456789', '17HCB')

--INSERT INTO course
--VALUES	('CTT011', 'Thiết kế giao diện', 'C32'),
--		('CTT012', 'Kiểm chứng phần mềm', 'C32'),
--		('CTT001', 'Lập trình ứng dụng Java', 'C31'),
--		('CTT002', 'Mạng máy tính', 'C31')

--INSERT INTO grade
--VALUES	('17HCB', 'CTT001', '1753001', 10, 10, 10, 10)

--USE MASTER
--DROP DATABASE university;