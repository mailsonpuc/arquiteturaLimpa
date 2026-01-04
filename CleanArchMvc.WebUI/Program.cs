using CleanArchMvc.Infra.IoC;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

//  AQUI registra a Infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Seed initial roles and users
using (var scope = app.Services.CreateScope())
{
    try
    {
        // Apply EF Core migrations automatically
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        var seeder = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        seeder?.SeedRoles();
        seeder?.SeedUsers();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // HSTS (somente produção)
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
