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

CREATE VIEW PersonNotes AS
SELECT N.ID, N.Title, N.MainBody, N.CategoryID, P.Email
FROM Note AS N
JOIN Person AS P
	ON P.ID = N.PersonID
/*DROP VIEW UserNotes*/

CREATE VIEW PersonCategories AS
SELECT DISTINCT  C.ID, C.Title, P.Email
FROM Category AS C
JOIN Person AS P
	ON C.PersonId = P.ID
/*DROP VIEW UserCategories*/

SELECT ID, Title FROM NoteTags
WHERE PersonId = 1;

SELECT ID, Title, MainBody, CategoryID FROM PersonNotes
WHERE Email = 'JoeyJoeyRemmers@gmail.com';

SELECT ID, Title  FROM PersonCategories
WHERE Email = 'JoeyJoeyRemmers@gmail.com';