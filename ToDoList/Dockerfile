FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /build
COPY . .
RUN dotnet restore "ToDoList.csproj"
RUN dotnet publish -c Release "ToDoList.csproj" -o /build/deploy

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS publish
WORKDIR /app
COPY --from=build /build/deploy .
EXPOSE 80
ENTRYPOINT ["dotnet", "ToDoList.dll"]