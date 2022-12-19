USE NoteBase
GO

CREATE VIEW NoteTags AS
SELECT DISTINCT  T.ID,T.Title, N.PersonId, N.ID AS NoteID
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.TagID
JOIN Note AS N
	ON N.ID = NT.NoteID
/*DROP VIEW NoteTags*/

CREATE VIEW TagNotes AS
SELECT N.ID, N.Title, N.Text, N.CategoryID, T.ID AS 'TagID'
FROM Note AS N
JOIN NoteTag AS NT
	ON N.ID = NT.NoteID
JOIN Tag AS T
	ON T.ID = NT.TagID
/*DROP VIEW UserNotes*/

SELECT ID, Title, Text, CategoryId FROM TagNotes WHERE TagID = 1

CREATE VIEW PersonNotes AS
SELECT N.ID, N.Title, N.Text, N.CategoryID, P.Email
FROM Note AS N
JOIN Person AS P
	ON P.ID = N.PersonID
/*DROP VIEW UserNotes*/

CREATE VIEW PersonCategories AS
SELECT DISTINCT  C.ID, C.Title, P.ID AS PersonId
FROM Category AS C
JOIN Person AS P
	ON C.PersonId = P.ID
/*DROP VIEW PersonCategories*/

CREATE VIEW TagNoConnection AS
SELECT DISTINCT T.ID
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.TagID
/*DROP VIEW PersonCategories*/

SELECT T.ID, T.Title
FROM Tag AS T

SELECT * 
FROM Tag

DELETE FROM Note 
WHERE ID = 1021

SELECT ID, Title FROM NoteTags
WHERE PersonId = 1;

SELECT ID, Title, Text, CategoryID FROM PersonNotes
WHERE Email = 'JoeyJoeyRemmers@gmail.com';

SELECT ID, Title  FROM PersonCategories
WHERE PersonID = 1;

UPDATE Category SET Title = '' WHERE ID = 1