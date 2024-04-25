# Start with the ASP.NET Core SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /CSharp-SMS-Quickstart

# Copy project file
COPY SMSQuickstart.csproj SMSQuickstart.csproj

# Install required packages
RUN dotnet add package freeclimb

# Copy the source code into the container
COPY . .

# Run the application
ENTRYPOINT ["dotnet", "run", "--urls=http://0.0.0.0:3000"]
