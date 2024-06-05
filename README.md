#  NoteBaseAPI

## Description
The backend of my notes organizer application.

## Requirements
- Have Docker installed.
- 
## Project Startup

### Setup database
- First run the NoteBaseScripts/NoteBaseScript/Mainscript.sql script to create the database.
- Then run NoteBaseScripts/NoteBaseScript/Views.sql to create the necessary views. At the moment, you need to select and run all view code parts separately.

### Setup Docker container
Setup the Docker container with the following command, remember to change parameters where necessary:
```cmd
$ docker run --name NoteBaseAPI --hostname NoteBaseAPI -e "DATABASE_URL = Data Source={IP},{port};Initial Catalog=NoteBase;User id={UserId};Password={Password};Connect Timeout=300;" -p {port}:80 -d joeyremmers/notebaseAPI
```
