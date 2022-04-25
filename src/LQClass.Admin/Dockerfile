FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish "./LQClass.Admin/LQClass.Admin.csproj" -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# install libgdiplus for System.Drawing
RUN apt-get update && \
    apt-get install -y --allow-unauthenticated libgdiplus libc6-dev

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "LQClass.Admin.dll"]
