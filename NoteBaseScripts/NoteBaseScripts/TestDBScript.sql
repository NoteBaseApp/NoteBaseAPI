DROP TABLE NoteTag
DROP TABLE Note
DROP TABLE Category
DROP TABLE Person
DROP TABLE Tag

DROP VIEW NoteTags
DROP VIEW PersonNotes
DROP VIEW PersonCategories


/*creating tables */
GO
CREATE TABLE Tag (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Title VARCHAR(50) NOT NULL
);

CREATE TABLE Person (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL
);

CREATE TABLE Category (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Title VARCHAR(50) NOT NULL,
	PersonId INT NOT NULL FOREIGN KEY REFERENCES Person(ID)
	UNIQUE(Title, PersonId)
);

CREATE TABLE Note (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Title VARCHAR(50),
	Text VARCHAR(1000) NOT NULL,
	CategoryID INT FOREIGN KEY REFERENCES Category(ID),
	PersonId INT NOT NULL FOREIGN KEY REFERENCES Person(ID)
);

CREATE TABLE NoteTag (
	NoteID INT NOT NULL FOREIGN KEY REFERENCES Note(ID),
	TagID INT NOT NULL FOREIGN KEY REFERENCES Tag(ID),
	PRIMARY KEY(NoteID, TagID)
);

INSERT INTO Tag (Title)
	VALUES ('Fontys'),
		('Vrienden'),
		('Minecraft'),
		('Bach')

GO

INSERT INTO Person (Email, Name)
	VALUES ('JoeyjoeyRemmers@gmail.com', 'Joey Remmers'),
		('MerijnRemmers@gmail.com', 'Merijn Remmers')

INSERT INTO Category(Title, PersonId)
	VALUES ('School', 1),
		('Games', 1),
		('Muziek', 2)

INSERT INTO Note(Title, Text, CategoryID, PersonId)
	VALUES ('nieuwe school', 'ik heb mij ingeschreven bij #Fontys', 1, 1),
		('Game avond', 'zaterdag avond #Minecraft spelen met #Vrienden', 2, 1),
		('bladmuziek', 'ik maak bladmuziek met #Bach', 3, 2)
	
INSERT INTO NoteTag(NoteID, TagID)
	VALUES (1,1),
		(2,2),
		(2,3),
		(3,4)

GO

CREATE VIEW NoteTags AS
SELECT DISTINCT  T.ID,T.Title, N.PersonId, N.ID AS NoteID
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.TagID
JOIN Note AS N
	ON N.ID = NT.NoteID
/*DROP VIEW NoteTags*/

GO

CREATE VIEW PersonNotes AS
SELECT N.ID, N.Title, N.Text, N.CategoryID, P.Email
FROM Note AS N
JOIN Person AS P
	ON P.ID = N.PersonID
/*DROP VIEW UserNotes*/

GO

CREATE VIEW PersonCategories AS
SELECT DISTINCT  C.ID, C.Title, P.ID AS PersonId
FROM Category AS C
JOIN Person AS P
	ON C.PersonId = P.ID
/*DROP VIEW PersonCategories*/