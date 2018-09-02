FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY FreeskiDb-WebApi .

RUN dotnet restore
RUN dotnet publish -c Release -o output

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /src/output .

ENV TEST_ENV verdi-123

ENTRYPOINT ["dotnet", "/app/FreeskiDb-WebApi.dll"]