FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build
WORKDIR /app

COPY src/API/API.csproj src/API/API.csproj
COPY src/Contracts/*.csproj src/Contracts/
COPY src/Data.Dapper/*.csproj src/Data.Dapper/
COPY *.sln .
# ENTRYPOINT tail -f /dev/null & wait
RUN dotnet restore

RUN dotnet dev-certs https -ep /aspnetapp.pfx -p Timetable
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 as final

ENV ASPNETCORE_Kestrel__Certificates__Default__Password Timetable
ENV ASPNETCORE_Kestrel__Certificates__Default__Path aspnetapp.pfx

EXPOSE 44310
EXPOSE 443
EXPOSE 8080

WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /aspnetapp.pfx .
ENTRYPOINT dotnet API.dll