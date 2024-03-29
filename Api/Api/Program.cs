using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Domain.Abstractions;
using Infrastructure.Repositories;
using Application.ServiceInterface;
using Infrastructure.Service;
using Infrastructure.Exceptions;
using Infrastructure.MinIO;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(8001));
// Add services to the container.
builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.AddScoped<IMetaDataRepository, MetadataRepository>();
builder.Services.AddScoped<IMetadataService, MetadataService>();
builder.Services.AddScoped<FileRules>();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);


//builder.Services.AddDbContext<DataContext>(options =>
//options.UseInMemoryDatabase("Files")
//);
builder.Services.AddDbContext<DataContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);     // DateTime bez tego nie dzia�a� z jakiegos powodu

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
