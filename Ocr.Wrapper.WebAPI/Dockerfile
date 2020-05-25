#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

LABEL version="0.2"
LABEL author="David Valpey"

RUN apt-get update && apt-get install -y wget gnupg && apt-get autoremove && apt-get clean
RUN echo "deb https://notesalexp.org/tesseract-ocr-dev/buster/ buster main" >> /etc/apt/sources.list
RUN wget -O - https://notesalexp.org/debian/alexp_key.asc | apt-key add -
RUN apt-get update && apt-get install -y apt-transport-https tesseract-ocr tesseract-ocr-nld tesseract-ocr-eng mupdf mupdf-tools && apt-get autoremove && apt-get clean

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Ocr.Wrapper.WebAPI/Ocr.Wrapper.WebAPI.csproj", "Ocr.Wrapper.WebAPI/"]
COPY ["Ocr.Wrapper/Ocr.Wrapper.csproj", "Ocr.Wrapper/"]
RUN dotnet restore "Ocr.Wrapper.WebAPI/Ocr.Wrapper.WebAPI.csproj"
COPY . .
WORKDIR "/src/Ocr.Wrapper.WebAPI"
RUN dotnet build "Ocr.Wrapper.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocr.Wrapper.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocr.Wrapper.WebAPI.dll"]