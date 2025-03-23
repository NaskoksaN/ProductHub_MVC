using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext db;

        public DbInitializer(
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            ApplicationDbContext _db)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            db = _db;
        }
        public void Initialize()
        {
            //migration if they are not applied
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            //create role if they are not
            if (!roleManager.RoleExistsAsync(SDRoles.Role_Customer).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(SDRoles.Role_Customer)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SDRoles.Role_Employee)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SDRoles.Role_Company));
                roleManager.CreateAsync(new IdentityRole(SDRoles.Role_Company));


                //create admin
                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "main_admin@mail.com",
                    Email = "main_admin@mail.com",
                    Name = "Main Admin",
                    PhoneNumber = "0888888111",
                    StreetAddress = "street 1",
                    PostalCode = "9000",
                    City = "Varna"
                }).GetAwaiter().GetResult();

                ApplicationUser user = db.ApplicationUsers
                            .FirstOrDefault(u => u.Email == "main_admin@mail.com");
                userManager.AddToRoleAsync(user, SDRoles.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
