using Catalog.API;
using Catalog.Application.IntegrationEvents.EventHandling;
using Catalog.Application.IntegrationEvents.Events;
using Catalog.Application.Models;
using Catalog.Application.Queries;
using Catalog.Domain.Models.CatalogBrands;
using Catalog.Domain.Models.CatalogItems;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Common.Abstractions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServices();
builder.Services.AddControllers();
builder.Services.AddDbContext<CatalogContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);
        sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});
builder.Services.Configure<CatalogSettings>(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
builder.Services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();

builder.Services.AddScoped<IRequestHandler<GetCatalogBrandsQuery, QueryResult<IEnumerable<CatalogBrandDTO>>>, GetCatalogBrandsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCatalogItemByIdQuery, QueryResult<CatalogItemDTO>>, GetCatalogItemByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCatalogItemsQuery, QueryResult<IEnumerable<CatalogItemDTO>>>, GetCatalogItemsQueryHandler>();
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

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<OrderConfirmedIntegrationEvent, OrderConfirmedIntegrationEventHandler>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    await context.Database.MigrateAsync();
    await new CatalogContextSeed().SeedAsync(context);
}

await app.RunAsync();
