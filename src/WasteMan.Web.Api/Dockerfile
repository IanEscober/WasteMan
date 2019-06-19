ARG baseTag=2.1-alpine3.9
ARG buildTag=2.1-alpine3.9
ARG baseImage=mcr.microsoft.com/dotnet/core/aspnet
ARG buildImage=mcr.microsoft.com/dotnet/core/sdk

FROM ${baseImage}:${baseTag} AS base
WORKDIR /app
EXPOSE 80

FROM ${buildImage}:${buildTag} AS build
WORKDIR /src
COPY ./src/**/*.csproj ./
COPY ./dirBuilder.sh .
RUN ./dirBuilder.sh
RUN dotnet restore ./WasteMan.Web.Api/WasteMan.Web.Api.csproj
COPY ./src .
RUN dotnet build ./WasteMan.Web.Api/WasteMan.Web.Api.csproj --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish ./WasteMan.Web.Api/WasteMan.Web.Api.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WasteMan.Web.Api.dll"]