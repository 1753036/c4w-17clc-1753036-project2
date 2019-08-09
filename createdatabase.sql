CREATE DATABASE university;
GO

USE university;

CREATE TABLE class
(
	id CHAR(6) NOT NULL PRIMARY KEY,
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
	birthday CHAR(8) NOT NULL,
	social_id CHAR(9) NOT NULL UNIQUE,
	class_id CHAR(6) NOT NULL,
	FOREIGN KEY(class_id) REFERENCES class(id)
);
GO

CREATE TABLE course
(
	id CHAR(6) NOT NULL PRIMARY KEY,
	fullname NVARCHAR(100) NOT NULL,
	room CHAR(3) NOT NULL,
);
GO

CREATE TABLE section
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	class_id CHAR(6) NOT NULL,
	course_id CHAR(6) NOT NULL,
	term CHAR NOT NULL,
	acad_year CHAR(4) NOT NULL,
	FOREIGN KEY(class_id) REFERENCES class(id),
	FOREIGN KEY(course_id) REFERENCES course(id)
);
GO

CREATE TABLE grade_report
(
	section_id INT NOT NULL,
	student_id CHAR(7) NOT NULL,
	midterm FLOAT NOT NULL,
	final FLOAT NOT NULL,
	other FLOAT NOT NULL,
	total FLOAT NOT NULL,

	FOREIGN KEY(section_id) REFERENCES section(id),
	FOREIGN KEY(student_id) REFERENCES student(id),
	PRIMARY KEY(section_id, student_id)
)
GO

CREATE TABLE auth
(
	username VARCHAR(50) NOT NULL PRIMARY KEY,
	passwd VARCHAR(32) NOT NULL,
	permission VARCHAR(100) NOT NULL
)
GO

CREATE FUNCTION fn_authorize(@username VARCHAR(50), @hashedPassword VARCHAR(32))
RETURNS INT
BEGIN
	DECLARE @result INT
	SELECT @result = COUNT(@username) 
	FROM auth
	WHERE username = @username
	AND passwd = @hashedPassword
	RETURN @result
END
GO

CREATE PROCEDURE sp_register_section(
	@section_id INT,
	@student_id CHAR(7))
AS
	IF EXISTS (SELECT * FROM section WHERE id = @section_id)
		INSERT INTO grade_report
		VALUES (@section_id, @student_id, 0, 0, 0, 0)
	ELSE
		THROW 50000, 'Section does not exist!', 1
GO

CREATE PROCEDURE sp_cancle_section(
	@section_id INT,
	@student_id CHAR(7))
AS
	DELETE grade_report
	WHERE section_id = @section_id AND student_id = @student_id
GO

CREATE PROCEDURE sp_add_student(
	@student_id VARCHAR(7),
	@fullname NVARCHAR(100),
	@gender CHAR,
	@birthday CHAR(8),
	@social_id CHAR(9),
	@class_id CHAR(6))
AS
	INSERT INTO student
	VALUES (@student_id, @fullname, @gender, @birthday, @social_id, @class_id)
	INSERT INTO auth
	VALUES (@student_id, CONVERT(VARCHAR(32), HASHBYTES('MD5', @birthday), 2), 'student')
GO

INSERT INTO auth
VALUES ('giaovu', CONVERT(VARCHAR(32), HASHBYTES('MD5', 'giaovu'), 2), 'staff')

--INSERT INTO class
--VALUES ('17CLC1')

--INSERT INTO course
--VALUES ('CTT011', 'Thiet Ke Phan Mem', 'C32')

--INSERT INTO section
--VALUES ('17CLC1', 'CTT011', 1, 2019)

--EXEC dbo.sp_add_student '1753036', 'Thai Chi Cuong', 'M', '14041999', '352434747', '17CLC1'
--EXEC dbo.sp_register_section 1, '1753036'
--EXEC dbo.sp_cancle_section 1, '1753036'
--SELECT dbo.fn_authorize('1753036', CONVERT(VARCHAR(32), HASHBYTES('MD5', '14041999'), 2))

--CLEAN TEST DATA
--DELETE grade_report
--DELETE section
--DELETE course
--DELETE student
--DELETE class
--DELETE auth
--USE MASTER
--DROP TABLE university;