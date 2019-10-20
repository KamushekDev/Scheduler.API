FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build
WORKDIR /app

RUN dotnet dev-certs https -ep /aspnetapp.pfx -p Timetable
COPY . ./
# RUN dotnet dev-certs https --trustыыыы
RUN dotnet publish -c Release -o outы

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 as final

ENV ASPNETCORE_URLS https://+:443;http://+:8080
ENV ASPNETCORE_HTTPS_PORT 44310
ENV ASPNETCORE_Kestrel__Certificates__Default__Password Timetable
ENV ASPNETCORE_Kestrel__Certificates__Default__Path aspnetapp.pfx

EXPOSE 44310
EXPOSE 443
EXPOSE 8080

WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /aspnetapp.pfx .
ENTRYPOINT dotnet API.dll