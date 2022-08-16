using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Domain.Abstractions;
using Infrastructure.Repositories;
using Application.ServiceInterface;
using Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMetaDataRepository, MetadataRepository>();
builder.Services.AddScoped<IMetadataService, MetadataService>();

builder.Services.AddControllers();

//builder.Services.AddDbContext<DataContext>(options =>
//options.UseInMemoryDatabase("Files")
//);
builder.Services.AddDbContext<DataContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
