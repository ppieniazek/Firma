CREATE PROCEDURE LiczWyplaty (@m int, @y int) AS
BEGIN
	WITH GodzinyTotal AS (
  SELECT p_id, SUM(ilosc) AS godziny
FROM Godziny g
WHERE MONTH(data_dnia) = @m
	AND YEAR(data_dnia) = @y
GROUP BY p_id
), ZaliczkiTotal AS (
  SELECT p_id, SUM(kwota) AS zaliczki
FROM Zaliczki z
WHERE MONTH(data_zaliczki) = @m
		AND YEAR(data_zaliczki) = @y
	GROUP BY p_id
), UrlopyTotal AS (
  SELECT p_id, SUM(dni) AS dni
FROM Urlopy u
WHERE MONTH(data_rozpoczecia) = @m
		AND YEAR(data_rozpoczecia) = @y
	GROUP BY p_id
)
SELECT p.p_id, p.imie, p.nazwisko,(t1.godziny * p.stawka - COALESCE(t2.zaliczki, 0) + COALESCE(t3.dni, 0) * 8 * p.stawka * 0.7) AS wypłata
FROM Pracownicy p
LEFT JOIN GodzinyTotal t1 ON
	p.p_id = t1.p_id
LEFT JOIN ZaliczkiTotal t2 ON
	p.p_id = t2.p_id
LEFT JOIN UrlopyTotal t3 ON
	p.p_id = t3.p_id;
END;
INSERT INTO Pracownicy (imie,nazwisko,adres,kod_pocztowy,stanowisko,data_urodzenia,data_przyjecia,stawka) VALUES
     (N'Marcin',N'Strycharz',N'Gwizdaj 455',N'37-200',N'Kierownik','1982-06-16','2013-08-10',35),
	 (N'Andrzej',N'Pieniazek',N'Urzejowice 89',N'37-200',N'Brygadzista','1971-02-16','2016-10-17',31),
	 (N'Piotrek',N'Nowak',N'Mikulice 62',N'37-200',N'Brygadzista','1992-10-05','2017-02-10',29);
INSERT INTO Brygady (p_id) VALUES
	 (1),
	 (2),
	 (3);
UPDATE Pracownicy SET b_id = 1 WHERE p_id = 1;
UPDATE Pracownicy SET b_id = 2 WHERE p_id = 2;
UPDATE Pracownicy SET b_id = 3 WHERE p_id = 3;
INSERT INTO Pracownicy (imie,nazwisko,adres,kod_pocztowy,stanowisko,data_urodzenia,data_przyjecia,b_id,stawka) VALUES
	 (N'Anna',N'Kowalska',N'Rzeszów 15',N'35-100',N'Pracownik','1990-08-20','2015-04-12',1,17),
	 (N'Pawel',N'Lis',N'Warszawska 28',N'00-001',N'Pracownik','1985-06-15','2012-09-08',1,17),
	 (N'Magdalena',N'Wójcik',N'Krakowska 7',N'30-050',N'Pracownik','1993-03-25','2018-11-03',1,17),
	 (N'Kamil',N'Zielinski',N'Poznanska 45',N'60-120',N'Pracownik','1988-12-10','2014-07-18',1,17),
	 (N'Joanna',N'Szymanska',N'Gdanska 82',N'80-001',N'Pracownik','1991-09-01','2016-05-22',1,17),
	 (N'Marcin',N'Dabrowski',N'Lódzka 3',N'90-005',N'Pracownik','1987-11-08','2013-10-14',1,17),
	 (N'Natalia',N'Kaczmarek',N'Wroclawska 12',N'50-200',N'Pracownik','1994-04-18','2019-01-30',1,19),
	 (N'Rafal',N'Jankowski',N'Sosnowiecka 56',N'40-300',N'Pracownik','1986-07-22','2011-06-09',1,19),
	 (N'Ewa',N'Krajewska',N'Katowicka 34',N'42-150',N'Pracownik','1995-01-12','2020-08-05',1,19),
	 (N'Adam',N'Michalak',N'Gdynia 73',N'81-010',N'Pracownik','1989-04-30','2017-11-20',1,19),
	 (N'Karolina',N'Nowicka',N'Plocka 21',N'09-300',N'Pracownik','1996-06-08','2021-03-17',3,19),
	 (N'Tomasz',N'Kubiak',N'Olsztynska 44',N'10-120',N'Pracownik','1990-12-03','2016-08-25',3,19),
	 (N'Dominika',N'Lewandowska',N'Bydgoska 67',N'85-002',N'Pracownik','1992-02-15','2017-10-10',3,19),
	 (N'Artur',N'Witkowski',N'Suwalki 89',N'16-300',N'Pracownik','1988-09-20','2014-05-12',3,20),
	 (N'Aleksandra',N'Olejnik',N'Zielona 32',N'25-001',N'Pracownik','1994-04-05','2019-01-08',3,20),
	 (N'Krzysztof',N'Szewczyk',N'Kielecka 76',N'30-510',N'Pracownik','1987-11-18','2013-06-22',3,20),
	 (N'Monika',N'Pawlak',N'Ostrzeszowska 9',N'55-200',N'Pracownik','1991-07-22','2016-12-19',3,20),
	 (N'Lukasz',N'Górecki',N'Gliwicka 54',N'44-100',N'Pracownik','1986-03-15','2012-09-04',3,20),
	 (N'Patrycja',N'Kurek',N'Zamojska 11',N'20-150',N'Pracownik','1995-01-28','2020-06-11',3,20),
	 (N'Damian',N'Cieslak',N'Siedlecka 87',N'08-010',N'Pracownik','1989-05-10','2015-11-26',3,20),
	 (N'Anna',N'Józwiak',N'Piotrkowska 65',N'90-001',N'Pracownik','1996-08-18','2021-04-23',2,18),
	 (N'Bartosz',N'Olszewski',N'Koszalinska 32',N'70-200',N'Pracownik','1990-12-12','2016-07-15',2,18),
	 (N'Agnieszka',N'Sikora',N'Czestochowska 98',N'42-300',N'Pracownik','1992-03-25','2017-11-03',2,18),
	 (N'Piotr',N'Jablonski',N'Rybnicka 45',N'44-120',N'Pracownik','1988-10-10','2014-06-18',2,18),
	 (N'Natalia',N'Adamska',N'Pucka 22',N'81-005',N'Pracownik','1991-09-20','2019-04-02',2,18),
	 (N'Lukasz',N'Mazurek',N'Lubelska 7',N'20-005',N'Pracownik','1987-12-15','2013-09-14',2,21),
	 (N'Katarzyna',N'Wesolowska',N'Slupska 12',N'76-300',N'Pracownik','1994-05-18','2019-02-28',2,21),
	 (N'Marcin',N'Szymczak',N'Jeleniogórska 56',N'58-100',N'Pracownik','1986-08-22','2011-07-09',2,21),
	 (N'Ewelina',N'Kaczorowska',N'Bielska 34',N'43-150',N'Pracownik','1995-02-12','2020-09-05',2,21),
	 (N'Jakub',N'Kowalczyk',N'Tarnowska 73',N'33-010',N'Pracownik','1989-04-30','2016-01-20',2,21);
INSERT INTO Godziny (p_id,data_dnia,ilosc) VALUES
	 (1,'2024-01-01',8),
	 (4,'2024-01-01',8),
	 (5,'2024-01-01',8),
	 (6,'2024-01-01',8),
	 (7,'2024-01-01',8),
	 (8,'2024-01-01',8),
	 (9,'2024-01-01',8),
	 (10,'2024-01-01',8),
	 (11,'2024-01-01',8),
	 (12,'2024-01-01',8),
	 (13,'2024-01-01',8),
	 (1,'2024-01-01',8),
	 (4,'2024-01-01',8),
	 (5,'2024-01-01',8),
	 (6,'2024-01-01',8),
	 (7,'2024-01-01',8),
	 (8,'2024-01-01',8),
	 (9,'2024-01-01',8),
	 (10,'2024-01-01',8),
	 (11,'2024-01-01',8),
	 (12,'2024-01-01',8),
	 (13,'2024-01-01',8),
	 (1,'2024-01-02',8),
	 (4,'2024-01-02',8),
	 (5,'2024-01-02',8),
	 (6,'2024-01-02',8),
	 (7,'2024-01-02',8),
	 (8,'2024-01-02',8),
	 (9,'2024-01-02',8),
	 (10,'2024-01-02',8),
	 (11,'2024-01-02',8),
	 (12,'2024-01-02',8),
	 (13,'2024-01-02',8),
	 (1,'2024-01-03',8),
	 (4,'2024-01-03',8),
	 (6,'2024-01-03',8),
	 (7,'2024-01-03',8),
	 (8,'2024-01-03',8),
	 (9,'2024-01-03',8),
	 (10,'2024-01-03',8),
	 (11,'2024-01-03',8),
	 (12,'2024-01-03',8),
	 (13,'2024-01-03',8),
	 (1,'2024-01-04',8),
	 (4,'2024-01-04',8),
	 (6,'2024-01-04',8),
	 (7,'2024-01-04',8),
	 (8,'2024-01-04',8),
	 (9,'2024-01-04',8),
	 (10,'2024-01-04',8),
	 (11,'2024-01-04',8),
	 (12,'2024-01-04',8),
	 (13,'2024-01-04',8),
	 (1,'2024-01-05',8),
	 (4,'2024-01-05',8),
	 (6,'2024-01-05',8),
	 (7,'2024-01-05',8),
	 (8,'2024-01-05',8),
	 (9,'2024-01-05',8),
	 (10,'2024-01-05',8),
	 (11,'2024-01-05',8),
	 (12,'2024-01-05',8),
	 (13,'2024-01-05',8),
	 (1,'2024-01-08',8),
	 (4,'2024-01-08',8),
	 (5,'2024-01-08',8),
	 (6,'2024-01-08',8),
	 (7,'2024-01-08',8),
	 (8,'2024-01-08',8),
	 (9,'2024-01-08',8),
	 (10,'2024-01-08',8),
	 (11,'2024-01-08',8),
	 (12,'2024-01-08',8),
	 (13,'2024-01-08',8),
	 (1,'2024-01-09',8),
	 (4,'2024-01-09',8),
	 (5,'2024-01-09',8),
	 (6,'2024-01-09',8),
	 (7,'2024-01-09',8),
	 (8,'2024-01-09',8),
	 (9,'2024-01-09',8),
	 (10,'2024-01-09',8),
	 (11,'2024-01-09',8),
	 (12,'2024-01-09',8),
	 (13,'2024-01-09',8),
	 (1,'2024-01-10',8),
	 (4,'2024-01-10',8),
	 (5,'2024-01-10',8),
	 (6,'2024-01-10',8),
	 (7,'2024-01-10',8),
	 (8,'2024-01-10',8),
	 (9,'2024-01-10',8),
	 (10,'2024-01-10',8),
	 (11,'2024-01-10',8),
	 (12,'2024-01-10',8),
	 (13,'2024-01-10',8),
	 (1,'2024-01-11',8),
	 (4,'2024-01-11',8),
	 (5,'2024-01-11',8),
	 (6,'2024-01-11',8),
	 (7,'2024-01-11',8),
	 (8,'2024-01-11',8),
	 (9,'2024-01-11',8),
	 (10,'2024-01-11',8),
	 (11,'2024-01-11',8),
	 (12,'2024-01-11',8),
	 (13,'2024-01-11',8),
	 (1,'2024-01-15',8),
	 (4,'2024-01-15',8),
	 (5,'2024-01-15',8),
	 (6,'2024-01-15',8),
	 (8,'2024-01-15',8),
	 (9,'2024-01-15',8),
	 (10,'2024-01-15',8),
	 (11,'2024-01-15',8),
	 (12,'2024-01-15',8),
	 (13,'2024-01-15',8),
	 (1,'2024-01-16',8),
	 (4,'2024-01-16',8),
	 (5,'2024-01-16',8),
	 (6,'2024-01-16',8),
	 (8,'2024-01-16',8),
	 (9,'2024-01-16',8),
	 (10,'2024-01-16',8),
	 (11,'2024-01-16',8),
	 (12,'2024-01-16',8),
	 (13,'2024-01-16',8),
	 (1,'2024-01-17',8),
	 (4,'2024-01-17',8),
	 (5,'2024-01-17',8),
	 (6,'2024-01-17',8),
	 (8,'2024-01-17',8),
	 (9,'2024-01-17',8),
	 (10,'2024-01-17',8),
	 (11,'2024-01-17',8),
	 (12,'2024-01-17',8),
	 (13,'2024-01-17',8),
	 (1,'2024-01-18',8),
	 (4,'2024-01-18',8),
	 (5,'2024-01-18',8),
	 (6,'2024-01-18',8),
	 (7,'2024-01-18',8),
	 (8,'2024-01-18',8),
	 (9,'2024-01-18',8),
	 (10,'2024-01-18',8),
	 (11,'2024-01-18',8),
	 (12,'2024-01-18',8),
	 (13,'2024-01-18',8),
	 (1,'2024-01-19',8),
	 (4,'2024-01-19',8),
	 (5,'2024-01-19',8),
	 (6,'2024-01-19',8),
	 (7,'2024-01-19',8),
	 (8,'2024-01-19',8),
	 (9,'2024-01-19',8),
	 (10,'2024-01-19',8),
	 (11,'2024-01-19',8),
	 (12,'2024-01-19',8),
	 (13,'2024-01-19',8),
	 (1,'2024-01-22',8),
	 (4,'2024-01-22',8),
	 (5,'2024-01-22',8),
	 (6,'2024-01-22',8),
	 (7,'2024-01-22',8),
	 (8,'2024-01-22',8),
	 (10,'2024-01-22',8),
	 (11,'2024-01-22',8),
	 (12,'2024-01-22',8),
	 (13,'2024-01-22',8),
	 (1,'2024-01-23',8),
	 (4,'2024-01-23',8),
	 (5,'2024-01-23',8),
	 (6,'2024-01-23',8),
	 (7,'2024-01-23',8),
	 (8,'2024-01-23',8),
	 (9,'2024-01-23',8),
	 (10,'2024-01-23',8),
	 (11,'2024-01-23',8),
	 (12,'2024-01-23',8),
	 (13,'2024-01-23',8),
	 (1,'2024-01-24',8),
	 (4,'2024-01-24',8),
	 (5,'2024-01-24',8),
	 (6,'2024-01-24',8),
	 (7,'2024-01-24',8),
	 (8,'2024-01-24',8),
	 (9,'2024-01-24',8),
	 (10,'2024-01-24',8),
	 (11,'2024-01-24',8),
	 (12,'2024-01-24',8),
	 (13,'2024-01-24',8),
	 (1,'2024-01-25',8),
	 (4,'2024-01-25',8),
	 (5,'2024-01-25',8),
	 (6,'2024-01-25',8),
	 (7,'2024-01-25',8),
	 (8,'2024-01-25',8),
	 (9,'2024-01-25',8),
	 (10,'2024-01-25',8),
	 (11,'2024-01-25',8),
	 (12,'2024-01-25',8),
	 (13,'2024-01-25',8),
	 (1,'2024-01-26',8),
	 (4,'2024-01-26',8),
	 (5,'2024-01-26',8),
	 (6,'2024-01-26',8),
	 (7,'2024-01-26',8),
	 (8,'2024-01-26',8),
	 (9,'2024-01-26',8),
	 (10,'2024-01-26',8),
	 (11,'2024-01-26',8),
	 (12,'2024-01-26',8),
	 (13,'2024-01-26',8),
	 (1,'2024-01-29',8),
	 (4,'2024-01-29',8),
	 (5,'2024-01-29',8),
	 (6,'2024-01-29',8),
	 (7,'2024-01-29',8),
	 (8,'2024-01-29',8),
	 (9,'2024-01-29',8),
	 (10,'2024-01-29',8),
	 (11,'2024-01-29',8),
	 (12,'2024-01-29',8),
	 (13,'2024-01-29',8),
	 (1,'2024-01-30',8),
	 (4,'2024-01-30',8),
	 (5,'2024-01-30',8),
	 (6,'2024-01-30',8),
	 (7,'2024-01-30',8),
	 (8,'2024-01-30',8),
	 (9,'2024-01-30',8),
	 (10,'2024-01-30',8),
	 (11,'2024-01-30',8),
	 (12,'2024-01-30',8),
	 (13,'2024-01-30',8),
	 (1,'2024-01-31',8),
	 (4,'2024-01-31',8),
	 (5,'2024-01-31',8),
	 (6,'2024-01-31',8),
	 (7,'2024-01-31',8),
	 (8,'2024-01-31',8),
	 (9,'2024-01-31',8),
	 (10,'2024-01-31',8),
	 (11,'2024-01-31',8),
	 (12,'2024-01-31',8),
	 (13,'2024-01-31',8),
	 (2,'2024-01-01',10),
	 (24,'2024-01-01',10),
	 (26,'2024-01-01',10),
	 (27,'2024-01-01',10),
	 (28,'2024-01-01',10),
	 (29,'2024-01-01',10),
	 (30,'2024-01-01',10),
	 (31,'2024-01-01',10),
	 (32,'2024-01-01',10),
	 (33,'2024-01-01',10),
	 (2,'2024-01-02',10),
	 (24,'2024-01-02',10),
	 (26,'2024-01-02',10),
	 (27,'2024-01-02',10),
	 (28,'2024-01-02',10),
	 (29,'2024-01-02',10),
	 (30,'2024-01-02',10),
	 (31,'2024-01-02',10),
	 (32,'2024-01-02',10),
	 (33,'2024-01-02',10),
	 (2,'2024-01-03',10),
	 (24,'2024-01-03',10),
	 (26,'2024-01-03',10),
	 (27,'2024-01-03',10),
	 (28,'2024-01-03',10),
	 (29,'2024-01-03',10),
	 (30,'2024-01-03',10),
	 (31,'2024-01-03',10),
	 (32,'2024-01-03',10),
	 (33,'2024-01-03',10),
	 (2,'2024-01-04',10),
	 (24,'2024-01-04',10),
	 (25,'2024-01-04',10),
	 (26,'2024-01-04',10),
	 (27,'2024-01-04',10),
	 (28,'2024-01-04',10),
	 (29,'2024-01-04',10),
	 (30,'2024-01-04',10),
	 (31,'2024-01-04',10),
	 (32,'2024-01-04',10),
	 (33,'2024-01-04',10),
	 (2,'2024-01-05',10),
	 (24,'2024-01-05',10),
	 (25,'2024-01-05',10),
	 (26,'2024-01-05',10),
	 (27,'2024-01-05',10),
	 (28,'2024-01-05',10),
	 (29,'2024-01-05',10),
	 (30,'2024-01-05',10),
	 (31,'2024-01-05',10),
	 (32,'2024-01-05',10),
	 (33,'2024-01-05',10),
	 (2,'2024-01-08',10),
	 (24,'2024-01-08',10),
	 (25,'2024-01-08',10),
	 (26,'2024-01-08',10),
	 (27,'2024-01-08',10),
	 (28,'2024-01-08',10),
	 (29,'2024-01-08',10),
	 (30,'2024-01-08',10),
	 (31,'2024-01-08',10),
	 (32,'2024-01-08',10),
	 (33,'2024-01-08',10),
	 (2,'2024-01-09',10),
	 (24,'2024-01-09',10),
	 (25,'2024-01-09',10),
	 (26,'2024-01-09',10),
	 (27,'2024-01-09',10),
	 (28,'2024-01-09',10),
	 (29,'2024-01-09',10),
	 (30,'2024-01-09',10),
	 (31,'2024-01-09',10),
	 (32,'2024-01-09',10),
	 (33,'2024-01-09',10),
	 (2,'2024-01-10',10),
	 (24,'2024-01-10',10),
	 (25,'2024-01-10',10),
	 (26,'2024-01-10',10),
	 (27,'2024-01-10',10),
	 (28,'2024-01-10',10),
	 (29,'2024-01-10',10),
	 (30,'2024-01-10',10),
	 (31,'2024-01-10',10),
	 (32,'2024-01-10',10),
	 (33,'2024-01-10',10),
	 (2,'2024-01-11',10),
	 (24,'2024-01-11',10),
	 (25,'2024-01-11',10),
	 (26,'2024-01-11',10),
	 (27,'2024-01-11',10),
	 (28,'2024-01-11',10),
	 (29,'2024-01-11',10),
	 (30,'2024-01-11',10),
	 (31,'2024-01-11',10),
	 (32,'2024-01-11',10),
	 (33,'2024-01-11',10),
	 (2,'2024-01-15',10),
	 (24,'2024-01-15',10),
	 (25,'2024-01-15',10),
	 (26,'2024-01-15',10),
	 (27,'2024-01-15',10),
	 (28,'2024-01-15',10),
	 (29,'2024-01-15',10),
	 (30,'2024-01-15',10),
	 (31,'2024-01-15',10),
	 (32,'2024-01-15',10),
	 (33,'2024-01-15',10),
	 (2,'2024-01-16',10),
	 (24,'2024-01-16',10),
	 (25,'2024-01-16',10),
	 (26,'2024-01-16',10),
	 (27,'2024-01-16',10),
	 (28,'2024-01-16',10),
	 (29,'2024-01-16',10),
	 (30,'2024-01-16',10),
	 (31,'2024-01-16',10),
	 (32,'2024-01-16',10),
	 (33,'2024-01-16',10),
	 (2,'2024-01-17',10),
	 (24,'2024-01-17',10),
	 (25,'2024-01-17',10),
	 (26,'2024-01-17',10),
	 (27,'2024-01-17',10),
	 (28,'2024-01-17',10),
	 (29,'2024-01-17',10),
	 (30,'2024-01-17',10),
	 (31,'2024-01-17',10),
	 (32,'2024-01-17',10),
	 (33,'2024-01-17',10),
	 (2,'2024-01-18',10),
	 (24,'2024-01-18',10),
	 (25,'2024-01-18',10),
	 (26,'2024-01-18',10),
	 (27,'2024-01-18',10),
	 (28,'2024-01-18',10),
	 (29,'2024-01-18',10),
	 (30,'2024-01-18',10),
	 (31,'2024-01-18',10),
	 (32,'2024-01-18',10),
	 (33,'2024-01-18',10),
	 (2,'2024-01-19',10),
	 (24,'2024-01-19',10),
	 (25,'2024-01-19',10),
	 (26,'2024-01-19',10),
	 (27,'2024-01-19',10),
	 (28,'2024-01-19',10),
	 (29,'2024-01-19',10),
	 (30,'2024-01-19',10),
	 (31,'2024-01-19',10),
	 (32,'2024-01-19',10),
	 (33,'2024-01-19',10),
	 (2,'2024-01-22',10),
	 (24,'2024-01-22',10),
	 (25,'2024-01-22',10),
	 (26,'2024-01-22',10),
	 (27,'2024-01-22',10),
	 (28,'2024-01-22',10),
	 (29,'2024-01-22',10),
	 (30,'2024-01-22',10),
	 (31,'2024-01-22',10),
	 (32,'2024-01-22',10),
	 (33,'2024-01-22',10),
	 (2,'2024-01-23',10),
	 (24,'2024-01-23',10),
	 (25,'2024-01-23',10),
	 (26,'2024-01-23',10),
	 (27,'2024-01-23',10),
	 (28,'2024-01-23',10),
	 (29,'2024-01-23',10),
	 (30,'2024-01-23',10),
	 (31,'2024-01-23',10),
	 (32,'2024-01-23',10),
	 (33,'2024-01-23',10),
	 (2,'2024-01-24',10),
	 (24,'2024-01-24',10),
	 (25,'2024-01-24',10),
	 (26,'2024-01-24',10),
	 (27,'2024-01-24',10),
	 (28,'2024-01-24',10),
	 (29,'2024-01-24',10),
	 (30,'2024-01-24',10),
	 (31,'2024-01-24',10),
	 (32,'2024-01-24',10),
	 (33,'2024-01-24',10),
	 (2,'2024-01-25',10),
	 (24,'2024-01-25',10),
	 (25,'2024-01-25',10),
	 (26,'2024-01-25',10),
	 (27,'2024-01-25',10),
	 (28,'2024-01-25',10),
	 (29,'2024-01-25',10),
	 (30,'2024-01-25',10),
	 (31,'2024-01-25',10),
	 (32,'2024-01-25',10),
	 (33,'2024-01-25',10),
	 (2,'2024-01-26',10),
	 (24,'2024-01-26',10),
	 (25,'2024-01-26',10),
	 (26,'2024-01-26',10),
	 (27,'2024-01-26',10),
	 (28,'2024-01-26',10),
	 (29,'2024-01-26',10),
	 (30,'2024-01-26',10),
	 (31,'2024-01-26',10),
	 (32,'2024-01-26',10),
	 (33,'2024-01-26',10),
	 (2,'2024-01-29',10),
	 (24,'2024-01-29',10),
	 (25,'2024-01-29',10),
	 (26,'2024-01-29',10),
	 (27,'2024-01-29',10),
	 (28,'2024-01-29',10),
	 (29,'2024-01-29',10),
	 (31,'2024-01-29',10),
	 (32,'2024-01-29',10),
	 (33,'2024-01-29',10),
	 (2,'2024-01-30',10),
	 (24,'2024-01-30',10),
	 (25,'2024-01-30',10),
	 (26,'2024-01-30',10),
	 (27,'2024-01-30',10),
	 (28,'2024-01-30',10),
	 (29,'2024-01-30',10),
	 (31,'2024-01-30',10),
	 (32,'2024-01-30',10),
	 (33,'2024-01-30',10),
	 (2,'2024-01-31',10),
	 (24,'2024-01-31',10),
	 (25,'2024-01-31',10),
	 (26,'2024-01-31',10),
	 (27,'2024-01-31',10),
	 (28,'2024-01-31',10),
	 (29,'2024-01-31',10),
	 (30,'2024-01-31',10),
	 (31,'2024-01-31',10),
	 (32,'2024-01-31',10),
	 (33,'2024-01-31',10),
	 (3,'2024-01-01',7),
	 (14,'2024-01-01',7),
	 (15,'2024-01-01',7),
	 (16,'2024-01-01',7),
	 (17,'2024-01-01',7),
	 (18,'2024-01-01',7),
	 (19,'2024-01-01',7),
	 (20,'2024-01-01',7),
	 (21,'2024-01-01',7),
	 (22,'2024-01-01',7),
	 (23,'2024-01-01',7),
	 (3,'2024-01-02',7),
	 (14,'2024-01-02',7),
	 (15,'2024-01-02',7),
	 (16,'2024-01-02',7),
	 (17,'2024-01-02',7),
	 (18,'2024-01-02',7),
	 (19,'2024-01-02',7),
	 (20,'2024-01-02',7),
	 (21,'2024-01-02',7),
	 (22,'2024-01-02',7),
	 (23,'2024-01-02',7),
	 (3,'2024-01-03',7),
	 (14,'2024-01-03',7),
	 (15,'2024-01-03',7),
	 (16,'2024-01-03',7),
	 (17,'2024-01-03',7),
	 (18,'2024-01-03',7),
	 (19,'2024-01-03',7),
	 (20,'2024-01-03',7),
	 (21,'2024-01-03',7),
	 (22,'2024-01-03',7),
	 (23,'2024-01-03',7),
	 (3,'2024-01-04',7),
	 (14,'2024-01-04',7),
	 (15,'2024-01-04',7),
	 (16,'2024-01-04',7),
	 (17,'2024-01-04',7),
	 (18,'2024-01-04',7),
	 (19,'2024-01-04',7),
	 (20,'2024-01-04',7),
	 (21,'2024-01-04',7),
	 (22,'2024-01-04',7),
	 (23,'2024-01-04',7),
	 (3,'2024-01-05',7),
	 (14,'2024-01-05',7),
	 (15,'2024-01-05',7),
	 (16,'2024-01-05',7),
	 (17,'2024-01-05',7),
	 (18,'2024-01-05',7),
	 (19,'2024-01-05',7),
	 (20,'2024-01-05',7),
	 (21,'2024-01-05',7),
	 (22,'2024-01-05',7),
	 (23,'2024-01-05',7),
	 (3,'2024-01-08',7),
	 (14,'2024-01-08',7),
	 (15,'2024-01-08',7),
	 (16,'2024-01-08',7),
	 (17,'2024-01-08',7),
	 (18,'2024-01-08',7),
	 (19,'2024-01-08',7),
	 (20,'2024-01-08',7),
	 (21,'2024-01-08',7),
	 (22,'2024-01-08',7),
	 (23,'2024-01-08',7),
	 (3,'2024-01-09',7),
	 (14,'2024-01-09',7),
	 (15,'2024-01-09',7),
	 (16,'2024-01-09',7),
	 (17,'2024-01-09',7),
	 (18,'2024-01-09',7),
	 (19,'2024-01-09',7),
	 (20,'2024-01-09',7),
	 (21,'2024-01-09',7),
	 (22,'2024-01-09',7),
	 (23,'2024-01-09',7),
	 (14,'2024-01-10',7),
	 (15,'2024-01-10',7),
	 (16,'2024-01-10',7),
	 (17,'2024-01-10',7),
	 (18,'2024-01-10',7),
	 (19,'2024-01-10',7),
	 (20,'2024-01-10',7),
	 (21,'2024-01-10',7),
	 (22,'2024-01-10',7),
	 (23,'2024-01-10',7),
	 (14,'2024-01-11',7),
	 (15,'2024-01-11',7),
	 (16,'2024-01-11',7),
	 (17,'2024-01-11',7),
	 (18,'2024-01-11',7),
	 (19,'2024-01-11',7),
	 (20,'2024-01-11',7),
	 (21,'2024-01-11',7),
	 (22,'2024-01-11',7),
	 (23,'2024-01-11',7),
	 (3,'2024-01-15',7),
	 (14,'2024-01-15',7),
	 (15,'2024-01-15',7),
	 (16,'2024-01-15',7),
	 (17,'2024-01-15',7),
	 (18,'2024-01-15',7),
	 (19,'2024-01-15',7),
	 (20,'2024-01-15',7),
	 (21,'2024-01-15',7),
	 (22,'2024-01-15',7),
	 (23,'2024-01-15',7),
	 (3,'2024-01-16',7),
	 (14,'2024-01-16',7),
	 (15,'2024-01-16',7),
	 (16,'2024-01-16',7),
	 (17,'2024-01-16',7),
	 (18,'2024-01-16',7),
	 (19,'2024-01-16',7),
	 (20,'2024-01-16',7),
	 (21,'2024-01-16',7),
	 (22,'2024-01-16',7),
	 (23,'2024-01-16',7),
	 (3,'2024-01-17',7),
	 (14,'2024-01-17',7),
	 (15,'2024-01-17',7),
	 (16,'2024-01-17',7),
	 (17,'2024-01-17',7),
	 (18,'2024-01-17',7),
	 (19,'2024-01-17',7),
	 (20,'2024-01-17',7),
	 (21,'2024-01-17',7),
	 (22,'2024-01-17',7),
	 (23,'2024-01-17',7),
	 (3,'2024-01-18',7),
	 (14,'2024-01-18',7),
	 (15,'2024-01-18',7),
	 (16,'2024-01-18',7),
	 (17,'2024-01-18',7),
	 (18,'2024-01-18',7),
	 (19,'2024-01-18',7),
	 (20,'2024-01-18',7),
	 (21,'2024-01-18',7),
	 (22,'2024-01-18',7),
	 (23,'2024-01-18',7),
	 (3,'2024-01-19',7),
	 (14,'2024-01-19',7),
	 (15,'2024-01-19',7),
	 (16,'2024-01-19',7),
	 (17,'2024-01-19',7),
	 (18,'2024-01-19',7),
	 (19,'2024-01-19',7),
	 (20,'2024-01-19',7),
	 (21,'2024-01-19',7),
	 (22,'2024-01-19',7),
	 (23,'2024-01-19',7),
	 (3,'2024-01-22',7),
	 (14,'2024-01-22',7),
	 (15,'2024-01-22',7),
	 (17,'2024-01-22',7),
	 (18,'2024-01-22',7),
	 (19,'2024-01-22',7),
	 (20,'2024-01-22',7),
	 (21,'2024-01-22',7),
	 (22,'2024-01-22',7),
	 (23,'2024-01-22',7),
	 (3,'2024-01-23',7),
	 (14,'2024-01-23',7),
	 (15,'2024-01-23',7),
	 (17,'2024-01-23',7),
	 (18,'2024-01-23',7),
	 (19,'2024-01-23',7),
	 (20,'2024-01-23',7),
	 (21,'2024-01-23',7),
	 (22,'2024-01-23',7),
	 (23,'2024-01-23',7),
	 (3,'2024-01-24',7),
	 (14,'2024-01-24',7),
	 (15,'2024-01-24',7),
	 (17,'2024-01-24',7),
	 (18,'2024-01-24',7),
	 (19,'2024-01-24',7),
	 (20,'2024-01-24',7),
	 (21,'2024-01-24',7),
	 (22,'2024-01-24',7),
	 (23,'2024-01-24',7),
	 (3,'2024-01-25',7),
	 (14,'2024-01-25',7),
	 (15,'2024-01-25',7),
	 (16,'2024-01-25',7),
	 (17,'2024-01-25',7),
	 (18,'2024-01-25',7),
	 (19,'2024-01-25',7),
	 (20,'2024-01-25',7),
	 (21,'2024-01-25',7),
	 (22,'2024-01-25',7),
	 (23,'2024-01-25',7),
	 (3,'2024-01-26',7),
	 (14,'2024-01-26',7),
	 (15,'2024-01-26',7),
	 (16,'2024-01-26',7),
	 (17,'2024-01-26',7),
	 (18,'2024-01-26',7),
	 (19,'2024-01-26',7),
	 (20,'2024-01-26',7),
	 (21,'2024-01-26',7),
	 (22,'2024-01-26',7),
	 (23,'2024-01-26',7),
	 (3,'2024-01-29',7),
	 (14,'2024-01-29',7),
	 (15,'2024-01-29',7),
	 (16,'2024-01-29',7),
	 (17,'2024-01-29',7),
	 (18,'2024-01-29',7),
	 (19,'2024-01-29',7),
	 (20,'2024-01-29',7),
	 (21,'2024-01-29',7),
	 (22,'2024-01-29',7),
	 (23,'2024-01-29',7),
	 (3,'2024-01-30',7),
	 (14,'2024-01-30',7),
	 (15,'2024-01-30',7),
	 (16,'2024-01-30',7),
	 (17,'2024-01-30',7),
	 (18,'2024-01-30',7),
	 (19,'2024-01-30',7),
	 (20,'2024-01-30',7),
	 (21,'2024-01-30',7),
	 (22,'2024-01-30',7),
	 (23,'2024-01-30',7),
	 (3,'2024-01-31',7),
	 (14,'2024-01-31',7),
	 (15,'2024-01-31',7),
	 (16,'2024-01-31',7),
	 (17,'2024-01-31',7),
	 (18,'2024-01-31',7),
	 (19,'2024-01-31',7),
	 (20,'2024-01-31',7),
	 (21,'2024-01-31',7),
	 (22,'2024-01-31',7),
	 (23,'2024-01-31',7);
INSERT INTO Urlopy (p_id,data_rozpoczecia,data_zakonczenia) VALUES
	 (5,'2024-01-03','2024-01-06'),
	 (7,'2024-01-15','2024-01-17'),
	 (16,'2024-01-22','2024-01-24'),
	 (30,'2024-01-28','2024-01-30'),
	 (25,'2024-01-01','2024-01-03'),
	 (3,'2024-01-10','2024-01-12'),
	 (9,'2024-01-20','2024-01-22');
INSERT INTO Zaliczki (p_id,data_zaliczki,kwota) VALUES
	 (4,'2024-01-04',300),
	 (5,'2024-01-15',250),
	 (6,'2024-01-26',400),
	 (27,'2024-01-08',150),
	 (18,'2024-01-22',480),
	 (9,'2024-01-10',200),
	 (20,'2024-01-31',350),
	 (11,'2024-01-19',430),
	 (32,'2024-01-05',300),
	 (33,'2024-01-12',180),
	 (14,'2024-01-28',220);
