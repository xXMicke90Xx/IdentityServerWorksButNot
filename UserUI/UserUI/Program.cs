using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UserUI.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpContextAccessor();
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("api1");
        options.Scope.Add("offline_access");
        options.GetClaimsFromUserInfoEndpoint = true;

        //options.Scope.Add("verification");
        //options.ClaimActions.MapJsonKey("email_verified", "email_verified");

        //options.SaveTokens = true;




    });



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
