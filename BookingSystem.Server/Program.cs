using BookingSystem.Infrastructure.Data;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Mappers;
using BookingSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MongoDbGenericRepository;
using Microsoft.OpenApi.Models;
using System.Net;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BookingSystemServerContextConnection") ?? throw new InvalidOperationException("Connection string 'BookingSystemServerContextConnection' not found.");

//builder.Services.AddDbContext<BookingSystemServerContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BookingSystemServerContext>();

builder.Services.AddAuthorization();

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoDbSettings!.AtlasURI, mongoDbSettings!.DatabaseName);

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



builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();
builder.Services.AddScoped<MongoConnection>();
builder.Services.AddScoped(typeof(MongoDb<>));


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
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.MapFallbackToFile("/index.html");




app.Run();
