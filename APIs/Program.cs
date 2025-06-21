using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Data Source=localhost;Initial Catalog=UniverstyMange;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepo<>));
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<LevelService>();
builder.Services.AddScoped<SemesterServices>();
builder.Services.AddScoped<IUnitOfWork, UniteOfWork>();
builder.Services.AddScoped<IDepartmentRepositiry,DepartmetRepository>();
builder.Services.AddScoped<ILevelRepositiry, LevelReopsitoriy>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();


var app = builder.Build();

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
