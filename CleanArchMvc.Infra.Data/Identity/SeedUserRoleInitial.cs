
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedRoles()
    {
        // Create Admin role
        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new IdentityRole("Admin") { NormalizedName = "ADMIN" };
            _roleManager.CreateAsync(role).Wait();
        }

        // Create User role
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            var role = new IdentityRole("User") { NormalizedName = "USER" };
            _roleManager.CreateAsync(role).Wait();
        }
    }

    public void SeedUsers()
    {
        // Admin user
        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedEmail = "ADMIN@LOCALHOST",
                NormalizedUserName = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(admin, "Admin#2025").Result;
            if (result.Succeeded)
                _userManager.AddToRoleAsync(admin, "Admin").Wait();
        }

        // Normal user
        if (_userManager.FindByEmailAsync("user@localhost").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "user@localhost",
                Email = "user@localhost",
                NormalizedEmail = "USER@LOCALHOST",
                NormalizedUserName = "USER@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "User#2025").Result;
            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "User").Wait();
        }
    }
}
