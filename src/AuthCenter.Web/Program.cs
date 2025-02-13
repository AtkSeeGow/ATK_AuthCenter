var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddDeveloperSigningCredential()
    .AddTestUsers(IdentityConfig.Users);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseIdentityServer();
app.UseAuthorization();
app.UseStatusCodePages();
app.MapControllers();
app.Run();
