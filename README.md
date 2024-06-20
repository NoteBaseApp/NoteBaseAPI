#  NoteBaseAPI

## Description
The backend of my notes organizer application.

## Requirements
- Have Docker installed.
- have A MSSQL Server.
## Project Startup

### 1. Setup database
1. First run the NoteBaseScripts/NoteBaseScript/Mainscript.sql script to create the database.
2. Then run NoteBaseScripts/NoteBaseScript/Views.sql to create the necessary views. At the moment, you need to select and run all view code parts separately.
3. Create a user on the server with CRUD rights on the NoteBase Database.

### 2. Setup Docker container
Setup the Docker container with the following command, remember to replace all {parameters}. E.g DATA_SOURCE={IP},{port} becomes DATA_SOURCE=172.0.0.1,6969:
```cmd
docker run --name NoteBaseAPI --hostname NoteBaseAPI -e "DATA_SOURCE={IP},{port}" -e "INITIAL_CATALOG=NoteBase" -e "DB_USER_ID={SQL UserId}" -e "DB_PASSWORD={SQL Password}" -e "JWT_ACCESS_TOKEN_SECRET={jwt token}" -e "JWT_ISSUER={link}" -e "JWT_AUDIENCE={link}" -p {port}:80 -d joeyremmers/notebase-api
```
