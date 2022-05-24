create database Podrum_pica
use Podrum_pica


create table Osoba(

	Id int primary key identity(1, 1),
	Ime_prezime nvarchar(40),
	Mesto_stanovanja nvarchar(70),
	Imejl nvarchar(40),
	Lozinka nvarchar(30),
	Administrator char

)

create table Mesecni_popis(

	Id int primary key identity(1, 1),
	Popis_obavio int foreign key references Osoba(Id),
	Datum_popisa date,
	Razlika_u_ceni int

)

create table Stara_logovanja(

	Id int primary key identity(1, 1),
	Ulogovani_imejl nvarchar(40),
	Datum_logovanja date,
	Tip_logovanja char

)

create table Pice(

	Id int primary key identity(1, 1),
	Serijski_broj int,
	Naziv nvarchar(50),
	Cena int,
	Zemlja_proizvodnje nvarchar(50),
	Procenat_alkohola int

)

create table Narudzbina(

	Id int primary key identity(1, 1),
	Broj_narudzbine int,
	Pice int foreign key references Pice(Id),
	Kolicina int,
	Datum date

)

create table Prodajno_mesto(

	Id int primary key identity(1, 1),
	Serijski_broj int,
	Naziv nvarchar(30),
	Lokacija nvarchar(50),
	Radno_vreme nvarchar(40)

)

create table Prodaja_pice(

	Id int primary key identity(1, 1),
	Pice int foreign key references Pice(Id),
	Prodajno_mesto int foreign key references Prodajno_mesto(Id),
	Kolicina_pica int

)