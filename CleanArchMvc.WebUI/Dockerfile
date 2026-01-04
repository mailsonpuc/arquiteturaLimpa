FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy solution and restore
COPY ./CleanArchMvc.*/*.csproj ./
COPY ./CleanArchMvc.WebUI/*.csproj ./CleanArchMvc.WebUI/
COPY ./CleanArchMvc.Infra.IoC/*.csproj ./CleanArchMvc.Infra.IoC/
COPY ./CleanArchMvc.Infra.Data/*.csproj ./CleanArchMvc.Infra.Data/
COPY ./CleanArchMvc.Application/*.csproj ./CleanArchMvc.Application/
COPY ./CleanArchMvc.Domain/*.csproj ./CleanArchMvc.Domain/

RUN dotnet restore CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj

# copy everything and build
COPY . .
RUN dotnet publish CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "CleanArchMvc.WebUI.dll"]
