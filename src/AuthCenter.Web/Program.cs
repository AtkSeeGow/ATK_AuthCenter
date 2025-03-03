using Duende.IdentityServer.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServer()
    .AddCorsPolicyService<CorsPolicyProvider>()
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddDeveloperSigningCredential()
    .AddTestUsers(IdentityConfig.Users);

builder.Services.AddControllersWithViews();

// 設定 Cookie 驗證
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.SameSite = SameSiteMode.Lax; // 允許 HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // 禁用 Secure 限制
    });

// 配置 IdentityServer 選項
builder.Services.Configure<IdentityServerOptions>(options =>
{
    options.Authentication.CookieAuthenticationScheme = "Cookies";
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
});

// 設定 CookiePolicyOptions 允許 HTTP
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

var app = builder.Build();

// Middleware 順序
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy(); // 確保 Cookie 設定生效
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
