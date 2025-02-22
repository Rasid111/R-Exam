create database r_exam;

use r_exam;

create table Questions
(
	Id int primary key identity,
	Title nvarchar(50) unique not null,
	CorrectAnswerTitle nvarchar(50) not null,
);

create table Answers
(
	Id int primary key identity,
	Title nvarchar(50) not null,
	QuestionId int references Questions(id) on delete cascade not null
);

CREATE TABLE MethodTypes (
    Name NVARCHAR(50) NOT NULL PRIMARY KEY
);

CREATE TABLE RequestLogs (
    RequestId NVARCHAR(1000) PRIMARY KEY,
    Url NVARCHAR(MAX) NOT NULL,
    RequestBody NVARCHAR(MAX) NULL,
    RequestHeaders NVARCHAR(MAX) NULL,
    MethodType NVARCHAR(50) REFERENCES MethodTypes(Name) NOT NULL,
    CreationDateTime DATETIME2 NOT NULL,
    ClientIp NVARCHAR(45) NOT NULL,
);

CREATE TABLE ResponseLogs (
    StatusCode INT NOT NULL,
    ResponseBody NVARCHAR(MAX) NULL,
    ResponseHeaders NVARCHAR(MAX) NULL,
    EndDateTime DATETIME2 NULL,
);

select * from Questions;
select * from Answers;
select * from RequestLogs;
select * from ResponseLogs;
select * from MethodTypes;

drop table Answers;
drop table Questions;
drop table RequestLogs;
drop table ResponseLogs;
drop table MethodTypes;

INSERT INTO MethodTypes (Name)
VALUES ('GET'), ('POST'), ('PUT'), ('PATCH'), ('DELETE');