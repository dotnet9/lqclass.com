﻿FROM mcr.microsoft.com/dotnet/core/sdk:5.0-alpine AS build
WORKDIR /app

COPY . .
RUN dotnet publish "./LQClass.Admin/LQClass.Admin.csproj" -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# install libgdiplus for System.Drawing
RUN apk add libgdiplus --update-cache --repository http://dl-cdn.alpinelinux.org/alpine/edge/testing/ --allow-untrusted && \
    apk add terminus-font

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "LQClass.Admin.dll"]
