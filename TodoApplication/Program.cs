using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text;
using TodoApplication.IServices;
using TodoApplication.Jobs;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;
using TodoApplication.Repository;
using TodoApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddDbContext<TodoEntityModel>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("TodoDB")
    ));
builder.Services.AddScoped<IRepository, Repository<TodoEntityModel>>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
builder.Services.AddSingleton(scheduler);
builder.Services.AddHostedService<CustomQuartzHostedService>();

builder.Services.AddSingleton<IJobFactory, CustomQuartzJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<NotificationJob>();
//builder.Services.AddScoped<NotificationJob>();
builder.Services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notification Job", "0/10 * * * * ?"));

var app = builder.Build();

//app.UseCors(options => options.WithOrigins("http://localhost:3000")
app.UseCors(options => options.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
