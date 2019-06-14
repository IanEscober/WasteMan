FROM mcr.microsoft.com/dotnet/core/runtime-deps:2.1-alpine3.9
WORKDIR /app
EXPOSE 80
COPY ./src/WasteMan.Web.Api/bin/Release/netcoreapp2.1/linux-musl-x64/publish .
ENTRYPOINT ["./WasteMan.Web.Api"]