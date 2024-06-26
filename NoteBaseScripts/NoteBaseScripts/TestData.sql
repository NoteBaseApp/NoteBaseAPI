USE NoteBase

INSERT INTO Tag(ID, Title)
	VALUES ('A8531841-593A-442B-8F1B-D81B8D964C18', 'Fontys'),
		('FEB7257A-52E8-452F-A673-3656E82BA35C',' Vrienden'),
		('DE100497-77A1-41BD-B559-054ABCF12E9E',' Minecraft'),
		('E049D8FA-BE3F-42AE-B21A-4F8A0A2F19CF', 'Bach')

INSERT INTO Person(ID, Email, Name)
	VALUES ('79ECC113-5CCC-4F7D-83CF-49CD121D8751', 'JoeyjoeyRemmers@gmail.com', 'Joey Remmers'),
		('5C7114A3-46E6-41CD-B74B-A3342EEA0D5D', 'MerijnRemmers@gmail.com', 'Merijn Remmers')

INSERT INTO Category(ID, Title, PersonId)
	VALUES ('F672034B-0DDF-4028-A100-486BB8B49FF4', 'School', convert(UNIQUEIDENTIFIER, '79ECC113-5CCC-4F7D-83CF-49CD121D8751')),
		('1BD1781A-8F2F-4D70-8ABB-9C583D844E9C', 'Games',  convert(UNIQUEIDENTIFIER, '79ECC113-5CCC-4F7D-83CF-49CD121D8751')),
		('374C5F43-F5AA-49AB-917A-F0972D019F3D', 'Muziek', convert(UNIQUEIDENTIFIER, '5C7114A3-46E6-41CD-B74B-A3342EEA0D5D'))

INSERT INTO Note(ID, Title, Text, CategoryID, PersonId)
	VALUES ('5521070A-BD58-46EE-8E21-5F6B47E2677C', 'nieuwe school', 'ik heb mij ingeschreven bij #Fontys', convert(UNIQUEIDENTIFIER, 'F672034B-0DDF-4028-A100-486BB8B49FF4'), convert(uniqueidentifier, '79ECC113-5CCC-4F7D-83CF-49CD121D8751')),
		('348F8BC4-1056-454F-B01B-99DE018702AF', 'Game avond', 'zaterdag avond #Minecraft spelen met #Vrienden', convert(UNIQUEIDENTIFIER, '1BD1781A-8F2F-4D70-8ABB-9C583D844E9C'), convert(uniqueidentifier, '79ECC113-5CCC-4F7D-83CF-49CD121D8751')),
		('165F912A-1B22-4CF1-93EE-860A2E996021', 'bladmuziek', 'ik maak bladmuziek met #Bach', convert(UNIQUEIDENTIFIER, '374C5F43-F5AA-49AB-917A-F0972D019F3D'), convert(uniqueidentifier, '5C7114A3-46E6-41CD-B74B-A3342EEA0D5D'))
	
INSERT INTO NoteTag(NoteID, TagID)
	VALUES ('5521070A-BD58-46EE-8E21-5F6B47E2677C', convert(uniqueidentifier, 'A8531841-593A-442B-8F1B-D81B8D964C18')),
		('348F8BC4-1056-454F-B01B-99DE018702AF', convert(uniqueidentifier, 'FEB7257A-52E8-452F-A673-3656E82BA35C')),
		('348F8BC4-1056-454F-B01B-99DE018702AF', convert(uniqueidentifier, 'DE100497-77A1-41BD-B559-054ABCF12E9E')),
		('165F912A-1B22-4CF1-93EE-860A2E996021', convert(uniqueidentifier, 'E049D8FA-BE3F-42AE-B21A-4F8A0A2F19CF'))	

/* INSERT INTO NoteTag(NoteID, TagID)
	VALUES (1,1),
		(2,2),
		(2,3),
		(3,4)*/

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