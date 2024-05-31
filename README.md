# individueel Semester 2
Dit semester werk ik aan een aplicatie voor het maken en beheren van notities. Hierbij kan er gebruik gemaakt worden van categorieën en gegenereerde tags. Deze tags kun je gebruiken doormiddle van een # voor het woordt te zetten, bijvoorbeeld #Fontys, de tags worden vervolgens gekopeld aan de notitie. Zodra je zoekt op de tag krijg je hierbij alle notities te zien die er gebruik van maken.

## opbouw
de aplicatie bestaat uit 8 projecten:
- UI:
    - App (asp.net core MVC)
- logica:
    - NotebaseLogicFactory (creëert de logica classes met benodige DALs)
    - NotebaseLogicInterface (interface voor logica, bezit ook de models)
    - NotebaseLogic (logica laag die de business logic bevat)
    - NotebaseLogicTests (unit tests voor logica laag)
- DAL:
    - NotebaseDALInterface (interface voor Data access layer, bezit ook de DTO models)
    - NoteBaseDAL (Data access layer)
    - NotebaseDALTests (intergratie tests voor Data access layer en database)
