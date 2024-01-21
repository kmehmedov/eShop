using Services.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("order", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "order");
    });

    options.AddPolicy("shoppingCart", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "shoppingcart");
    });
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
