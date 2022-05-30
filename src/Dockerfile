FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . ./
RUN dotnet restore audit.csproj --disable-parallel && \
    dotnet publish audit.csproj -c Release -o publish --no-restore --no-self-contained -r linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV ASPNETCORE_URLS http://+
#"https://+;http://+" // some dev cert issue. solve it later
ENV ASPNETCORE_ENVIRONMENT Production
ENV connection_string mongodb://localhost:27017

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "audit.dll"]