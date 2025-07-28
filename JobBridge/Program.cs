using JobBridge.Components;
using JobBridge.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models;
using JobBridge.Services;
using JobBridge.Identity;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- START: Identity and Authentication Services ---
// These lines set up the authentication state management for Blazor
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

// Configure Identity authentication schemes (cookies)
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// Configure the database context for Identity (ApplicationDbContext)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add database developer page filter for migrations during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity Core with ApplicationUser and link it to ApplicationDbContext
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>() // Connects Identity to EF Core and your DbContext
    .AddSignInManager() // Enables sign-in functionality
    .AddDefaultTokenProviders(); // For features like password reset tokens

// Placeholder for email sender (Identity requires one, this is a no-op for development)
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
// --- END: Identity and Authentication Services ---


// // --- START: YOUR CUSTOM SERVICE REGISTRATIONS ---
// // Register your custom business logic services
// builder.Services.AddScoped<AuthService>();
// builder.Services.AddScoped<JobService>();
// builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<ApplicationService>();
// // --- END: YOUR CUSTOM SERVICE REGISTRATIONS ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use migrations endpoint in development for easy database updates
    app.UseMigrationsEndPoint();
}
else
{
    // Error handling for production
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable HTTPS redirection (recommended for production)
app.UseHttpsRedirection();

// Serve static files (HTML, CSS, JS, images from wwwroot)
app.UseStaticFiles();

// Anti-forgery token middleware for security
app.UseAntiforgery();

// --- START: Authentication and Authorization Middleware ---
// These MUST be placed after UseRouting() (which is implicit) and before MapRazorComponents
app.UseAuthentication(); // Enables authentication features
app.UseAuthorization();  // Enables authorization features
// --- END: Authentication and Authorization Middleware ---

// Maps Blazor components for interactive server rendering
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Adds additional endpoints for Identity UI (e.g., /Identity/Account/Login)
// This method is provided by the IdentityEndpointsExtensions.cs file
app.MapAdditionalIdentityEndpoints();

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();
