using Duende.IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddDeveloperSigningCredential()
    .AddTestUsers(IdentityConfig.Users);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies");

builder.Services.Configure<IdentityServerOptions>(options =>
{
    options.Authentication.CookieAuthenticationScheme = "Cookies";
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
});

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseIdentityServer();
app.UseAuthorization();
app.UseStatusCodePages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");
});

app.Run();
