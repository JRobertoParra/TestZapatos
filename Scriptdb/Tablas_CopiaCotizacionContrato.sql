Create database dbZapateria;
go
Use dbZapateria
go
Create table articles(
id int not null primary key,
name	varchar(100),
description  varchar(300),
price decimal(18,4),
total_in_shelf decimal(18,4),
total_in_vault decimal(18,4),
store_id int
)
go
Create table stores(
store_id int not null primary key,
name	varchar(100),
address varchar(300)
)

--Select * from articles
--Select * from stores

Insert into stores values(1,'Super Store','Addres Store')
Insert into articles values (1,'green shoes','The best quality of shoes in a green color',20.15,25,40,1)