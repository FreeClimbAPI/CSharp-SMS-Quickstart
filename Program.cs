﻿using DotNetEnv;
DotNetEnv.Env.Load();

if(String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ACCOUNT_ID")) || 
   String.IsNullOrEmpty(Environment.GetEnvironmentVariable("API_KEY")) || 
   String.IsNullOrEmpty(Environment.GetEnvironmentVariable("FREECLIMB_NUMBER"))) {
    Console.WriteLine("ERROR: ENVIRNOMENT VARIABLES ARE NOT SET. PLEASE SET ALL ENVIRNOMMENT VARIABLES AND RETRY.");
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

// Liveness probe endpoint
app.MapGet("/live", () => Results.Ok(new { status = "live" })).WithName("LiveCheck");

// Readiness probe endpoint
app.MapGet("/ready", () => Results.Ok(new { status = "ready" })).WithName("ReadyCheck");

Console.WriteLine("Welcome to FreeClimb!\n");
Console.WriteLine("Your web server is listening at http://127.0.0.1:3000");
Console.WriteLine("View an example PerCL JSON response to FreeClimb at http://127.0.0.1:3000/swagger/index.html\n");
Console.WriteLine("Your NEXT STEP is to use NGROK to proxy HTTP traffic to this local web server.");
Console.WriteLine("\t1. In NGROK, configure the dynamic url to proxy to http://127.0.0.1:3000");
Console.WriteLine("\t2. Using the Dashboard or API, set your FreeClimb Application Voice Url to the dynamic endpoint NGROK generated\n");

app.Run();

