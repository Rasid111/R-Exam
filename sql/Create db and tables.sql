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