USE [Firma]
CREATE TABLE Brygady (
	b_id int IDENTITY(1,1) NOT NULL,
	p_id int NULL,
	CONSTRAINT PK__Brygady__4E29C30D48160434 PRIMARY KEY (b_id)
);

CREATE TABLE Godziny (
	g_id int IDENTITY(1,1) primary key,
	p_id int NOT NULL,
	data_dnia date NOT NULL,
	ilosc int NOT NULL
);

CREATE TABLE Pracownicy (
	p_id int IDENTITY(1,1) NOT NULL,
	imie varchar(30) COLLATE Latin1_General_CI_AS NOT NULL,
	nazwisko varchar(30) COLLATE Latin1_General_CI_AS NOT NULL,
	adres varchar(255) COLLATE Latin1_General_CI_AS NOT NULL,
	kod_pocztowy varchar(6) COLLATE Latin1_General_CI_AS NOT NULL,
	stanowisko varchar(30) COLLATE Latin1_General_CI_AS NOT NULL,
	data_urodzenia date NOT NULL,
	data_przyjecia date NOT NULL,
	b_id int NULL,
	stawka int NOT NULL,
	CONSTRAINT PK__Pracowni__82E06B91EE349CD9 PRIMARY KEY (p_id)
);

CREATE TABLE Urlopy (
	u_id int identity(1,1) primary key,
	p_id int NOT NULL,
	data_rozpoczecia date NOT NULL,
	data_zakonczenia date NOT NULL,
	dni AS (datediff(day,[data_rozpoczecia],[data_zakonczenia])+(1))
);

CREATE TABLE Zaliczki (
	z_id int identity(1,1) primary key,
	p_id int NOT NULL,
	data_zaliczki date NOT NULL,
	kwota int NOT NULL
);

create table Login (
username nvarchar(50) primary key not null,
passw nvarchar(100) not null,
rola char(1) not null,
b_id int foreign key references Brygady(b_id)
);

ALTER TABLE Brygady ADD CONSTRAINT FK__Brygady__p_id__4D94879B FOREIGN KEY (p_id) REFERENCES Pracownicy(p_id);

ALTER TABLE Godziny ADD CONSTRAINT FK__Godziny__p_id__5070F446 FOREIGN KEY (p_id) REFERENCES Pracownicy(p_id);

ALTER TABLE Pracownicy ADD CONSTRAINT FK_PracownikBrygady FOREIGN KEY (b_id) REFERENCES Brygady(b_id);

ALTER TABLE Urlopy ADD CONSTRAINT FK__Urlopy__p_id__5441852A FOREIGN KEY (p_id) REFERENCES Pracownicy(p_id);

ALTER TABLE Zaliczki ADD CONSTRAINT FK__Zaliczki__p_id__52593CB8 FOREIGN KEY (p_id) REFERENCES Pracownicy(p_id);