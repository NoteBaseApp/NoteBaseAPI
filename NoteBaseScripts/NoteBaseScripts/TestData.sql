USE NoteBase

INSERT INTO Tag (Title)
	VALUES ('Fontys'),
		('Vrienden'),
		('Minecraft'),
		('Bach')

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

SELECT N.ID, N.Title, N.Text, C.Title AS Category, P.Name AS 'Person name', T.Title AS Tag 
FROM Note AS N
JOIN Person AS P
	ON P.ID = N.PersonID
JOIN Category AS C
	ON N.CategoryID = C.ID
JOIN NoteTag AS NT
	ON n.ID = NT.NoteID
JOIN Tag AS T
	ON T.ID = NT.TagID

	select * from Note

DELETE FROM NoteTag WHERE NoteID = 4
DELETE FROM Note WHERE ID = 1017