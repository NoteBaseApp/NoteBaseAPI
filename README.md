# individueel Semester 2

Mijn project voor het maken en beheren van notities. Hierbij kan er gebruik gemaakt worden van categorieën en gegenereerde tags.

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
    - NotebaseDALTests (intergratie tests voor Data access layer laag en database)
