FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app

COPY src/API/*.csproj src/API/
COPY src/Contracts/*.csproj src/Contracts/
COPY src/Data.Dapper/*.csproj src/Data.Dapper/
COPY src/Parser/*.csproj src/Parser/
COPY src/Domain/*.csproj src/Domain/
COPY src/Parser.Tests/*.csproj src/Parser.Tests/
COPY src/API.Tests/*.csproj src/API.Tests/

# RUN find . -name '*.csproj' | cpio -pdm .
# RUN cp --parents -r -v $(find -name *.csproj) /app

# RUN apt update
# RUN apt install cpio
# RUN find . -type f -name "*.csproj" | cpio -pdm  ./testik

COPY *.sln .
# ENTRYPOINT tail -f /dev/null & wait
RUN dotnet restore

RUN dotnet dev-certs https -ep /aspnetapp.pfx -p Timetable
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final

ENV ASPNETCORE_Kestrel__Certificates__Default__Password Timetable
ENV ASPNETCORE_Kestrel__Certificates__Default__Path aspnetapp.pfx

EXPOSE 44310
EXPOSE 443
EXPOSE 8080

WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /aspnetapp.pfx .
ENTRYPOINT dotnet API.dll