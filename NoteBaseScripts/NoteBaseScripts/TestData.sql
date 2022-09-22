USE NoteBase

INSERT INTO Tag (Title)
	VALUES ('Fontys'),
		('Vrienden'),
		('Minecraft'),
		('Bach')

INSERT INTO Category(Title)
	VALUES ('School'),
		('Games'),
		('Muziek')

INSERT INTO Note(Title, MainBody, CategoryID, UserMail)
	VALUES ('nieuwe school', 'ik heb mij ingeschreven bij #Fontys', 1, 'JoeyJoeyRemmers@gmail.com'),
		('Game avond', 'zaterdag avond #Minecraft spelen met #Vrienden', 2, 'JoeyJoeyRemmers@gmail.com'),
		('bladmuziek', 'ik maak bladmuziek met #Bach', 3, 'MerijnRemmers@gmail.com')
	
INSERT INTO NoteTag(NoteID, TagID)
	VALUES (1,1),
		(2,2),
		(2,3),
		(3,4)	

SELECT N.Title, N.MainBody, C.Title AS Category, N.UserMail, T.Title AS Tag 
FROM Note AS N
JOIN Category AS C
	ON N.CategoryID = C.ID
JOIN NoteTag AS NT
	ON n.ID = NT.NoteID
JOIN Tag AS T
	ON T.ID = NT.TagID