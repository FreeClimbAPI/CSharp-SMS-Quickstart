# Start with the ASP.NET Core SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /CSharp-SMS-Quickstart-Build

# Copy project file
COPY SMSQuickstart.csproj SMSQuickstart.csproj

# Install required packages
RUN dotnet add package freeclimb

# Copy the source code into the container
COPY . .

# Publish built application
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /CSharp-SMS-Quickstart

# Set port for application
ENV ASPNETCORE_URLS=http://+:3000

# Copy runtime files from build
COPY --from=build /CSharp-SMS-Quickstart-Build/out .

# Run the application
ENTRYPOINT ["dotnet", "SMSQuickstart.dll"]
