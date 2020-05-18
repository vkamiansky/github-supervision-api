FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Fls.Supervision.Api.csproj", "./"]
RUN dotnet restore "./Fls.Supervision.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Fls.Supervision.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fls.Supervision.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fls.Supervision.Api.dll"]
