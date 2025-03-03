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

// �]�w Cookie ����
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.SameSite = SameSiteMode.Lax; // ���\ HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // �T�� Secure ����
    });

// �t�m IdentityServer �ﶵ
builder.Services.Configure<IdentityServerOptions>(options =>
{
    options.Authentication.CookieAuthenticationScheme = "Cookies";
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
});

// �]�w CookiePolicyOptions ���\ HTTP
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

var app = builder.Build();

// Middleware ����
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy(); // �T�O Cookie �]�w�ͮ�
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
