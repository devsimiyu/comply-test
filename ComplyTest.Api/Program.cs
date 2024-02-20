using ComplyTest.Data.Context;
using ComplyTest.Data.Repository;
using ComplyTest.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

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
app.MapControllers();

// Apply pending database migrations
using (var scope = app.Services.CreateScope())
{
    var persistenceContext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();

    if (persistenceContext.Database.GetPendingMigrations().Any())
    {
        persistenceContext.Database.Migrate();
    }
}

// Start the application
app.Run();
