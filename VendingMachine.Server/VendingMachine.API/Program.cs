using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using VendingMachine.API.Middlewares;
using VendingMachine.Application;
using VendingMachine.Contracts.Models;
using VendingMachine.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(Envelope), StatusCodes.Status200OK));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath); 
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true; 
});

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

var imageFolder = Path.Combine(
    Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
    "Images"
);

if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageFolder),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

app.UseSession();

app.UseSingleUserMiddleware();

app.UseExceptionMiddleware();

app.MapControllers();

app.Run();