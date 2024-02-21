using ComplyTest.Data.Context;
using ComplyTest.Data.Repository;
using ComplyTest.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersitenceContext();
builder.Services.AddCoreRepository();
builder.Services.AddCoreService();
builder.Services.AddControllers(options =>
{
    // Use JSON property names in validation errors
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cors =>
    {
        cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddSingleton(new MemoryCache(new MemoryCacheOptions
{
    SizeLimit = 256
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();
app.MapControllers();

// Start the application
app.Run();

// Make Program class visible
public partial class Program { }
