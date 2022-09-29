USE NoteBase
GO

CREATE VIEW NoteTags AS
SELECT DISTINCT  T.ID,T.Title, N.UserMail, N.ID AS NoteID
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.TagID
JOIN Note AS N
	ON N.ID = NT.NoteID
/*DROP VIEW NoteTags*/

/* CREATE VIEW NoteTags AS
SELECT DISTINCT  T.ID, T.Title, N.UserMail
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.TagID
JOIN Note AS NT
	ON T.ID = NT.TagID */
/*DROP VIEW NoteTags*/

CREATE VIEW UserCategories AS
SELECT DISTINCT  C.ID, C.Title, N.UserMail
FROM Category AS C
JOIN Note AS N
	ON C.ID = N.CategoryID
/*DROP VIEW UserCategories*/

SELECT ID, Title FROM NoteTags
WHERE UserMail = 'JoeyJoeyRemmers@gmail.com';

SELECT ID, Title FROM NoteTags
WHERE NoteID = 2;

SELECT ID, Title  FROM UserCategories
WHERE UserMail = 'JoeyJoeyRemmers@gmail.com';