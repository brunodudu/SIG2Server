using Microsoft.EntityFrameworkCore;
using SIG2Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<ModelsContext>(x => 
    x.UseSqlServer(connectionString));
// builder.Services.AddDbContext<ModelsContext>(opt => 
//     opt.UseInMemoryDatabase("ModelsList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
