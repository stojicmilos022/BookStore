Create table Genre (
Id int Identity (1,1) Primary key,
GenreName nvarchar(50) not null,
Deleted bit Default 0);

--drop table genre
--drop table book
Create table Book (
Id int Identity (1,1),
BookName nvarchar(50) not null,
Price decimal (15,2) not null,
GenreId int not null,
Deleted bit Default 0
Foreign key(GenreId) references Genre(id)
);

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF

insert into Genre(GenreName) values ('Comedy')
insert into Genre(GenreName) values ('SF')
insert into Genre(GenreName) values ('Horror')

INSERT INTO [dbo].[Book] ( [BookName], [Price], [GenreId], [Deleted]) VALUES ( N'Harry potter', CAST(123.00 AS Decimal(15, 2)), 1, 0)
INSERT INTO [dbo].[Book] ( [BookName], [Price], [GenreId], [Deleted]) VALUES ( N'neka knjiga', CAST(1234.00 AS Decimal(15, 2)), 2, 0)

