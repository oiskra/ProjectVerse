using Microsoft.AspNetCore.Identity;
using projectverseAPI.Models;

namespace projectverseAPI.Data
{
    public class DbSeeder
    {
        private static RoleManager<IdentityRole> _roleManager;

        public DbSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public static void Seed(ProjectVerseContext context)
        {
            context.Database.EnsureCreated();

           /*if (context.UserRoles.Any()) return;

            _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            _roleManager.CreateAsync(new IdentityRole(UserRoles.CollaborationOwner));*/

            if (context.Technologies.Any()) return;

            var technologies = new Technology[]
            {
                new Technology() {Id = new Guid(), Name="React"},
                new Technology() {Id = new Guid(), Name="Angular"},
                new Technology() {Id = new Guid(), Name="Vue"},
                new Technology() {Id = new Guid(), Name="JavaScript"},
                new Technology() {Id = new Guid(), Name="TypeScript"},
                new Technology() {Id = new Guid(), Name="HTML"},
                new Technology() {Id = new Guid(), Name="CSS"},
                new Technology() {Id = new Guid(), Name="SASS"},
                new Technology() {Id = new Guid(), Name="LESS"}
            };

            context.Technologies.AddRangeAsync(technologies);
            context.SaveChanges();
        }
    }
}
