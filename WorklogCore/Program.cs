using Data;
using Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
Authentication.Initialize(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment.EnvironmentName;


if (env == "Development")
{
    var sqliteConnection = new SqliteConnection("Data Source=:memory:");
    sqliteConnection.Open();

    builder.Services.AddSingleton(sqliteConnection);
    builder.Services.AddDbContext<WorklogContext>(options =>
        options.UseSqlite(sqliteConnection));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("WorklogConnectionstring");
    builder.Services.AddDbContext<WorklogContext>(options =>
        options.UseSqlServer(connectionString));
}

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IWorklogRepository, WorklogRepository>();
builder.Services.AddScoped<IWorklogService, WorklogService>();

var app = builder.Build();

app.UseCors("AllowAll");

if (env == "Development")
{
    using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorklogContext>();
    db.Database.EnsureCreated(); 
}
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
