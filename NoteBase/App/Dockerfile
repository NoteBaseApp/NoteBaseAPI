#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/UI.csproj", "App/"]
COPY ["NoteBaseInterface/LogicInterface.csproj", "NoteBaseInterface/"]
COPY ["NoteBaseDALInterface/DALInterface.csproj", "NoteBaseDALInterface/"]
COPY ["NoteBaseLogicFactory/LogicFactory.csproj", "NoteBaseLogicFactory/"]
COPY ["NoteBaseDAL/DAL.csproj", "NoteBaseDAL/"]
COPY ["NoteBaseLogic/Logic.csproj", "NoteBaseLogic/"]
RUN dotnet restore "App/UI.csproj"
COPY . .
WORKDIR "/src/App"
RUN dotnet build "UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UI.dll"]