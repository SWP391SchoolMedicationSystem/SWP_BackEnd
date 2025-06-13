
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SchoolMedicalSystem.sln", "./"]
COPY ["SchoolMedicalSystem/SchoolMedicalSystem.csproj", "SchoolMedicalSystem/"]
COPY ["BussinessLayer/BussinessLayer.csproj", "BussinessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "SchoolMedicalSystem.sln"

COPY . .
WORKDIR "/src"
RUN dotnet build "SchoolMedicalSystem.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchoolMedicalSystem.sln" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolMedicalSystem.dll"]
