FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet test Test.FreeskiDb.WebApi
RUN dotnet publish FreeskiDb.WebApi -c Release -o output

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.3-alpine3.8
WORKDIR /app
COPY --from=build /src/FreeskiDb.WebApi/output .

ENTRYPOINT ["dotnet", "/app/FreeskiDb.WebApi.dll"]