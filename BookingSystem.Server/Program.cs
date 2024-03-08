using BookingSystem.Server.Data;
using BookingSystem.Server.Entities;
using BookingSystem.Server.Mappers;
using BookingSystem.Server.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
//builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();
builder.Services.AddScoped<MongoConnection>();
builder.Services.AddScoped(typeof(MongoDb<>));

//var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<BookingDbContext>(options => 
//    options.UseMongoDB(mongoDbSettings?.AtlasURI ?? "", mongoDbSettings?.DatabaseName ?? ""));


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");




app.Run();
