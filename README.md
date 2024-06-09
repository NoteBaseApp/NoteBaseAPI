#  NoteBaseAPI

## Description
The backend of my notes organizer application.

## Requirements
- Have Docker installed.
- have A MSSQL Server.
## Project Startup

### Setup database
- First run the NoteBaseScripts/NoteBaseScript/Mainscript.sql script to create the database.
- Then run NoteBaseScripts/NoteBaseScript/Views.sql to create the necessary views. At the moment, you need to select and run all view code parts separately.

### Setup Docker container
Setup the Docker container with the following command, remember to change parameters where necessary:
```cmd
docker run --name NoteBaseAPI --hostname NoteBaseAPI -e "DATA_SOURCE={IP},{port}" -e "INITIAL_CATALOG=NoteBase" -e "DB_USER_ID={SQL UserId}" -e "DB_PASSWORD={SQL Password}" -p {port}:80 -d joeyremmers/notebase-api
```
