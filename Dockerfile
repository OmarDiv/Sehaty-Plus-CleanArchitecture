#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Sehaty-Plus/Sehaty-Plus.csproj", "Sehaty-Plus/"]
COPY ["Sehaty-Plus.Application/Sehaty-Plus.Application.csproj", "Sehaty-Plus.Application/"]
COPY ["Sehaty-Plus.Domain/Sehaty-Plus.Domain.csproj", "Sehaty-Plus.Domain/"]
COPY ["Sehaty-Plus.Infrastructure/Sehaty-Plus.Infrastructure.csproj", "Sehaty-Plus.Infrastructure/"]
RUN dotnet restore "./Sehaty-Plus/Sehaty-Plus.csproj"
COPY . .
WORKDIR /src/Sehaty-Plus
RUN dotnet build "Sehaty-Plus.csproj" -c Release -o /app/build


#Publish Stage
FROM build AS publish
RUN dotnet publish "Sehaty-Plus.csproj" -c Release -o /app/publish

#Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sehaty-Plus.dll"]
