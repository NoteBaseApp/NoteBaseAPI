FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV CLIENTID=${{ secrets.CLIENTID }}
ENV CLIENTSECRET=${{ secrets.CLIENTSECRET }}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src

#print current directory
RUN ls

RUN dotnet build "NoteBase/App/UI.csproj" -c Release -o /app/build --no-cache

FROM build AS publish
RUN dotnet publish "NoteBase/App/UI.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:EnvironmentName=Production

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UI.dll"]
