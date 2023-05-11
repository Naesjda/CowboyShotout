using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Data.CRUD;
using CowboyShotout_DataLayer.Interfaces;
using CowboyShotout_DataLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddLogging(loggingBuilder =>
// {
//     loggingBuilder.ClearProviders();
// }).BuildServiceProvider();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();
//     options =>
// {
//     //TODO : Move connection string to keyvault
//     options.UseSqlServer();//builder.Configuration.GetConnectionString("DefaultConnection")
// });

builder.Services.AddScoped<ICRUD, CRUD>();
builder.Services.AddScoped<ICowboyService, CowboyService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();