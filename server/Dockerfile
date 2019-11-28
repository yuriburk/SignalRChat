FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SignalRChat/SignalRChat.API.csproj", "SignalRChat/"]
COPY ["SignalRChat.Infra.NoSQL/SignalRChat.Infra.NoSQL.csproj", "SignalRChat.Infra.NoSQL/"]
COPY ["SignalRChat.Domain/SignalRChat.Domain.csproj", "SignalRChat.Domain/"]
COPY ["SignalRChat.Applications/SignalRChat.Applications.csproj", "SignalRChat.Applications/"]
RUN dotnet restore "SignalRChat/SignalRChat.API.csproj"
COPY . .
WORKDIR "/src/SignalRChat"
RUN dotnet build "SignalRChat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRChat.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SignalRChat.API.dll