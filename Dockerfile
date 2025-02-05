FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY ["HotelChain.BL/HotelChain.BL.csproj", "HotelChain.BL/"]
COPY ["HotelChain.DataAccess/HotelChain.DataAccess.csproj", "HotelChain.DataAccess/"]
COPY ["HotelChain.Repository/HotelChain.Repository.csproj", "HotelChain.Repository/"]
COPY ["HotelChain.Service/HotelChain.Service.csproj", "HotelChain.Service/"]
RUN dotnet restore "HotelChain.Service/HotelChain.Service.csproj"
COPY . ../
WORKDIR /HotelChain.Service
RUN dotnet build "HotelChain.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=4114
EXPOSE 4114
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelChain.Service.dll"]