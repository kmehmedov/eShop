#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Notification/Notification.Email/Notification.Email.csproj", "Services/Notification/Notification.Email/"]
COPY ["Services/Services.Common/Services.Common/Services.Common.csproj", "Services/Services.Common/Services.Common/"]
RUN dotnet restore "Services/Notification/Notification.Email/Notification.Email.csproj"
COPY . .
WORKDIR "/src/Services/Notification/Notification.Email"
RUN dotnet build "Notification.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Email.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Email.dll"]