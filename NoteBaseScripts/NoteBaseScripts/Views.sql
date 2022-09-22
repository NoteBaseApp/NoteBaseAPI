USE NoteBase
GO

CREATE VIEW UserTags AS
SELECT DISTINCT  T.ID,T.Title, N.UserMail
FROM Tag AS T
JOIN NoteTag AS NT
	ON T.ID = NT.NoteID
JOIN Note AS N
	ON N.ID = NT.NoteID
/*DROP VIEW UserTags*/

CREATE VIEW UserCategories AS
SELECT DISTINCT  C.ID, C.Title, N.UserMail
FROM Category AS C
JOIN Note AS N
	ON C.ID = N.CategoryID
/*DROP VIEW UserCategories*/

SELECT ID, Title FROM UserTags
WHERE UserMail = 'JoeyJoeyRemmers@gmail.com';

SELECT ID, Title  FROM UserCategories
WHERE UserMail = 'JoeyJoeyRemmers@gmail.com';