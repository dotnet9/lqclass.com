#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LQClass/LQClass.csproj", "LQClass/"]
COPY ["LQClass.Model/LQClass.Model.csproj", "LQClass.Model/"]
COPY ["LQClass.DataAccess/LQClass.DataAccess.csproj", "LQClass.DataAccess/"]
COPY ["LQClass.ViewModel/LQClass.ViewModel.csproj", "LQClass.ViewModel/"]
RUN dotnet restore "LQClass/LQClass.csproj"
COPY . .
WORKDIR "/src/LQClass"
RUN dotnet build "LQClass.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LQClass.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LQClass.dll"]