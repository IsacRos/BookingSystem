using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using BookingSystem.Core.Mappers;
using BookingSystem.Core.Services;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(o =>
{
    o.ClaimsIdentity.UserNameClaimType = "string";
}).AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoDbSettings!.AtlasURI, mongoDbSettings!.DatabaseName);


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password = new PasswordOptions
    {
        RequireDigit = false,
        RequiredUniqueChars = 0,
        RequireNonAlphanumeric = false,
        RequiredLength = 0,
        RequireLowercase = false,
        RequireUppercase = false
    };
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});


builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();
builder.Services.AddScoped<IBookingMapper, BookingMapper>();
builder.Services.AddScoped<MongoConnection>();
builder.Services.AddScoped(typeof(IMongoDb<>), typeof(MongoDb<>));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    o.OperationFilter<SecurityRequirementsOperationFilter>();
});


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

app.MapGroup("api/auth")
    .MapIdentityApi<ApplicationUser>();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.MapFallbackToFile("/index.html");




app.Run();
