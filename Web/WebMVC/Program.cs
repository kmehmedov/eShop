using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Services.Common;
using System.IdentityModel.Tokens.Jwt;
using WebMVC;
using WebMVC.Services;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddHttpForwarder();
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Authentication
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();

var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl") ?? throw new InvalidOperationException("Invalid configuration");
var callBackUrl = builder.Configuration.GetValue<string>("CallBackUrl") ?? throw new InvalidOperationException("Invalid configuration");

// Add Authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityUrl;
    options.SignedOutRedirectUri = callBackUrl;
    options.ClientId = "mvc.client";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.RequireHttpsMetadata = false;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("webappgateway");
    options.Scope.Add("order");
    options.Scope.Add("shoppingcart");
    options.Scope.Add("notification.signalr");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>();
builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddHttpClient<IOrderService, OrderService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("defaultError", "{controller=Error}/{action=Error}");
var destination = app.Configuration.GetValue<string>("SignalRHubUrl") ?? throw new InvalidOperationException($"Invalid configuration");
app.MapForwarder("/hub/notificationhub/{**any}", destination, new ForwarderRequestConfig(), new MvcAuthHttpTransformer());
app.Run();
