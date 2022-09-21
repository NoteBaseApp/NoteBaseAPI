USE NoteBase

INSERT INTO Tag (Title)
	VALUES ('Fontys'),
		('Vrienden'),
		('Minecraft')

INSERT INTO Category(Title)
	VALUES ('School'),
		('Games')

INSERT INTO [User]([Name], Email, EncryptedPassword)
	VALUES ('Joey Remmers', 'Joey@remmers.net', 'fneonoewuo8f8383736')

INSERT INTO Note(Title, MainBody, CategoryID, UserID)
	VALUES ('nieuwe school', 'ik heb mij ingeschreven bij #Fontys', 1, 1),
		('Game avond', 'zaterdag avond #Minecraft spelen met #Vrienden', 2, 1)
	
INSERT INTO NoteTag(NoteID, TagID)
	VALUES (1,1),
		(2,2),
		(2,3)	

SELECT N.Title, N.MainBody, C.Title AS Category, T.Title AS Tag 
FROM Note AS N
JOIN Category AS C
	ON N.CategoryID = C.ID
JOIN NoteTag AS NT
	ON n.ID = NT.NoteID
JOIN Tag AS T
	ON T.ID = NT.TagID