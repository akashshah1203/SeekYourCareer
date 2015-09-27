CREATE TABLE [dbo].[UserDetails]
(
    [UserID] INT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(20) NOT NULL, 
    [Password] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(30) NOT NULL, 
    [DOB] DATE NOT NULL, 
    [ContactNumber] VARCHAR(10) NOT NULL, 
    [EmailID] VARCHAR(30) NOT NULL, 
    [Address] VARCHAR(200) NOT NULL, 
    [SSCPercent] DECIMAL(5,2) NOT NULL, 
    [HSCPercent] DECIMAL(5,2) NOT NULL, 
    [GradComplete] CHAR(1) NOT NULL, 
    [GradPercent] DECIMAL(5,2) NULL, 
    [PGComplete] CHAR(1) NOT NULL, 
    [PGPercent] DECIMAL(5,2) NULL, 
    [HaveWorkExp] CHAR(1) NOT NULL, 
    [WorkExpYears] INT NULL
)

CREATE TABLE [dbo].[RepDetails]
(
    [RepID] INT NOT NULL PRIMARY KEY, 
    [CompanyName] VARCHAR(30) NOT NULL, 
    [Password] VARCHAR(20) NOT NULL, 
    [Address] VARCHAR(200) NOT NULL, 
    [PhoneNumber] VARCHAR(10) NOT NULL, 
    [EmailID] VARCHAR(30) NOT NULL, 
    [Training] CHAR NOT NULL, 
    [Workshop] CHAR NOT NULL, 
    [Job] CHAR NOT NULL, 
    [Internship] CHAR NOT NULL, 
    
    
)

insert into dbo.UserDetails values(100001,'nicktaylor','nicktaylor123','Nick Taylor','10/1/1976','9876543212','nicktaylor@smoking.com','123 California',99.12,98.22,'Y',100.00,'N',null,'Y',10);
insert into dbo.UserDetails values(100002,'bsastha','bsastha123','Barath Sastha','11/6/1987','1234567891','bsastha@viselrpa.com','108 New York',97.11,100.00,'Y',78,'Y',89.12,'Y',5);
insert into dbo.UserDetails values(100003,'akasha','akasha123','Akash Shah','10/10/1993','1357924680','akasha@iitkgp.com','100 Boston',99.11,100.00,'Y',92,'N',null,'N',0);
insert into dbo.UserDetails values(100004,'vijay','vijay123','Vijay Ashwin','03/04/1987','1234567892','vijay@ashwin.com','101 San Francisco',100.00,100.00,'Y',100.00,'Y',100.00,'Y',5);

insert into [dbo].[RepDetails] values(1001,'Google','google123','23 Chicago','8967452313','google@gmail.com','Y','N','Y','N');
insert into [dbo].[RepDetails] values(1002,'Facebook','facebook123','25 Miami','1357935789','facebook@fb.com','N','Y','N','Y');
go

CREATE TABLE [dbo].[JobDetails]
(
    [JobId] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [RepId] INT NOT NULL, 
    [StreamCode] VARCHAR(30) NOT NULL, 
    [JobType] VARCHAR(9) NOT NULL, 
    [MinSSCPercent] DECIMAL(5, 2) NOT NULL, 
    [MinHSCPercent] DECIMAL(5, 2) NOT NULL, 
    [MinGradAvg] DECIMAL(5, 2) NULL, 
    [MinPGAvg] DECIMAL(5, 2) NULL, 
    [SalPerMonth] INT NOT NULL, 
    [Experience] INT NOT NULL, 
    [AppLastDate] DATE NOT NULL, 
    [StaffApprovalStatus] VARCHAR(10) NOT NULL
)

CREATE TABLE [dbo].[JobApplications]
(
    [ApplicantId] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL, 
    [JobId] VARCHAR(10) NOT NULL, 
    [AppDate] DATE NULL, 
    [Correspondance] VARCHAR(200) NULL, 
    [Status] VARCHAR(10) NOT NULL
)

go

insert into dbo.JobDetails values('ITJ101',1001,'Information Technology','FULL TIME',65.00,65.00,80.00,80.00,25000,2,'9/13/2015','Approved');
insert into dbo.JobDetails values('ITJ102',1001,'Information Technology','PART TIME',65.00,65.00,80.00,80.00,10000,2,'9/13/2015','Pending');

insert into dbo.JobApplications values('AJ101',100001,'ITJ101','9/12/2015','123 California','Selected');
insert into dbo.JobApplications values('AJ102',100002,'ITJ101','9/12/2015','108 New York','Pending');

CREATE TABLE [dbo].[WorkshopDetails]
(
    [WorkshopId] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [RepId] INT NOT NULL, 
    [Domain] VARCHAR(30) NOT NULL, 
    [FromDate] DATE NOT NULL, 
    [ToDate] DATE NOT NULL, 
    [NoOfSeats] INT NOT NULL, 
    [MinGradPct] DECIMAL(5, 2) NULL, 
    [MinPGPct] DECIMAL(5, 2) NULL, 
    [WorkshopDesc] VARCHAR(200) NOT NULL, 
    [StaffApproval] VARCHAR(10) NOT NULL, 
    
)

CREATE TABLE [dbo].[WorkshopAppln]
(
    [ApplicantId] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [WorkshopId] VARCHAR(10) NOT NULL, 
    [AppDate] DATE NULL, 
    [Status] VARCHAR(10) NOT NULL, 
)

go

insert into dbo.WorkshopDetails values('AIW101',1002,'Robotics','9/14/2015','10/14/2015',25,65.00,80.00,'Robotics Workshop by Facebook','Approved');
insert into dbo.WorkshopDetails values('AIW102',1002,'Artifical Intelligence','9/14/2015','10/14/2015',25,65.00,80.00,'Neural Networks','Pending');

insert into dbo.WorkshopAppln values('AW101',100001,'AIW101','9/12/2015','Pending');

CREATE TABLE [dbo].[TrainingDetails]
(
    [TrainingID] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [RepId] INT NOT NULL, 
    [Domain] VARCHAR(30) NOT NULL, 
    [Graduation] CHAR NOT NULL, 
    [PG] CHAR NOT NULL, 
    [PastExp] INT NOT NULL, 
    [StartingDate] DATE NOT NULL, 
    [Duration] INT NOT NULL, 
    [NoOfSeat] INT NOT NULL, 
    [TrainingDesc] VARCHAR(100) NULL, 
    [StaffApproval] VARCHAR(10) NOT NULL
)


CREATE TABLE [dbo].[TrainingAppln]
(
    [ApplicantId] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL, 
    [TrainingId] VARCHAR(10) NOT NULL, 
    [AppDate] DATE NULL, 
    [CorrAddress] VARCHAR(200) NULL, 
    [ContactNo] VARCHAR(10) NULL,
    [SelectionStatus] VARCHAR(10) NULL
)
go

insert into dbo.TrainingDetails values('FINT101',1001,'Finance','M','M',12,'9/13/2015',30,25,'Training in practical concepts of Finance','Approved');
insert into dbo.TrainingDetails values('FINT102',1001,'Finance','M','M',12,'9/13/2015',30,25,'Advanced Training in Finance','Pending');

insert into dbo.TrainingAppln values('AT101',100001,'FINT101','9/12/2015','123 California','9876543212','Selected');

