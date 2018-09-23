FROM microsoft/dotnet:2.1.402-sdk AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet test Test.FreeskiDb.WebApi
RUN dotnet publish FreeskiDb.WebApi -c Release -o output

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /src/FreeskiDb.WebApi/output .

ENTRYPOINT ["dotnet", "/app/FreeskiDb.WebApi.dll"]