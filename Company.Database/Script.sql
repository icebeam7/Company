create database CompanyDb
go

use CompanyDb
go

create table Department(
	Id int identity(1,1) primary key,
	Name varchar(255) not null
)

insert into Department (Name) values 
('Sales'),
('IT'),
('Human Resources')

create table Employee (
	Id int identity(1,1) primary key,
	Name varchar(255) not null,
	Salary decimal(10,2) not null,
	JoinDate smalldatetime not null,
	DepartmentId int not null,
	Constraint FK_EmployeeDepartment Foreign Key(DepartmentId) references Department(Id)
)

insert into Employee(Name, Salary, JoinDate, DepartmentId) values 
('Carla Mamani', 800, '2021-11-01',1),
('Jesus Gil', 1300, '2021-10-08',2),
('Humberto Jaimes', 1200, '2021-09-12',2),
('Luis Beltran', 600, '2021-12-01',3)

--Scaffold-DbContext Name=CompanyDbContext Microsoft.EntityFrameworkCore.SqlServer -Force -OutputDir Models -ContextDir Data -Context CompanyDbContext
go

select * from Employee
go
select * from Department