FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src

#print current directory
RUN ls

RUN dotnet build "NoteBaseAPI/NoteBaseAPI.csproj" -c Release -o /app/build --no-cache

FROM build AS publish
RUN dotnet publish "NoteBaseAPI/NoteBaseAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoteBaseAPI.dll"]