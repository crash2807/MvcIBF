using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;

namespace MvcIBF.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MvcIBFContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, MvcIBFContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            //wykonaj migracje jeśli ich nie ma
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }
            //stworzenie roli o ile nie istnieją
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Moderator")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
                //dodanie użytkownika Admin
                _userManager.CreateAsync(new ApplicationUser
                {
                    Email = "admin@admin.pl",
                    UserName = "admin@admin.pl",
                    ProfilePictureUrl = "https://hivedinn.s3.amazonaws.com/upload/photos/d-avatar.jpg?cache=0",

                }, "zaq1@WSX").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.pl");
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            return;
           
        }
    }
}
