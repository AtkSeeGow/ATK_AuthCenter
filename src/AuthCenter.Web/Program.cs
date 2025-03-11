using AuthCenter.Service;
using Duende.IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<ApiScopeService>();
builder.Services.AddSingleton<ApiResourceService>();
builder.Services.AddSingleton<IdentityResourceService>();
builder.Services.AddSingleton<UserService>();

var serviceProvider = builder.Services.BuildServiceProvider();
var clientService = serviceProvider.GetRequiredService<ClientService>();
var apiScopeService = serviceProvider.GetRequiredService<ApiScopeService>();
var apiResourceService = serviceProvider.GetRequiredService<ApiResourceService>();
var identityResourceService = serviceProvider.GetRequiredService<IdentityResourceService>();
var userService = serviceProvider.GetRequiredService<UserService>();

builder.Services.AddIdentityServer()
    .AddCorsPolicyService<CorsPolicyProvider>()
    .AddInMemoryClients(clientService.Get())
    .AddInMemoryApiScopes(apiScopeService.Get())
    .AddInMemoryApiResources(apiResourceService.Get())
    .AddInMemoryIdentityResources(identityResourceService.Get())
    .AddDeveloperSigningCredential()
    .AddTestUsers(UserService.Users);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax; // ¤¹³\ HTTP
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // ¸T¥Î Secure ­­¨î
});

builder.Services.Configure<IdentityServerOptions>(options =>
{
    options.Authentication.CookieAuthenticationScheme = "Cookies";
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
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
