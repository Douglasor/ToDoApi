FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ToDoApi.Api/ToDoApi.Api.csproj", "src/ToDoApi.Api/"]
COPY ["src/ToDoApi.Application/ToDoApi.Application.csproj", "src/ToDoApi.Application/"]
COPY ["src/ToDoApi.Domain/ToDoApi.Domain.csproj", "src/ToDoApi.Domain/"]
COPY ["src/ToDoApi.Infrastructure/ToDoApi.Infrastructure.csproj", "src/ToDoApi.Infrastructure/"]
RUN dotnet restore "src/ToDoApi.Api/ToDoApi.Api.csproj"

WORKDIR /src/src/ToDoApi.Api
COPY . .
RUN dotnet build "ToDoApi.Api.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ToDoApi.Api.dll"]

