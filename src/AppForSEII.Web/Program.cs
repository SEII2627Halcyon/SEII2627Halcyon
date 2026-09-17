using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using AppForSEII.Web.Components;
using AppForSEII.Web.Components.Account;
using AppForSEII.Web.Data;
using AppForSEII.Web.API;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//string? connection2Database = Environment.GetEnvironmentVariable("DBConnection2Use");

//the variable DBConnection2Use is defined in appsettings.json
string? connection2Database = builder.Configuration.GetValue(typeof(string), "DBConnection2Use") as string;


// If we are using the Production Environment, then the AZURE DB should be used,
// otherwise the localdb or SQLite should be used
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?source=recommendations&view=aspnetcore-7.0
switch (connection2Database) {
    case "SQLite":
        var _connection = new SqliteConnection("Filename=:memory:");
        //connection in case a persistent database is required
        //DbConnection _connection = new SqliteConnection("Data Source=Application.db;Cache=Shared");
        _connection.Open();
        builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(_connection));
        break;

    case "AzureSQL":
        builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                       opt.UseSqlServer(Environment.GetEnvironmentVariable("AzureSQL")));

        break;
    default:
        //the localdb is used
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        break;
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

string? URI2API = builder.Configuration.GetValue(typeof(string), "AppForSEIIAPIClient_URL") as string;

//the environment variable URI2API is defined in appsettings.json
builder.Services.AddScoped<AppForSEIIAPIClient>(sp =>new AppForSEIIAPIClient(URI2API, new HttpClient()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
