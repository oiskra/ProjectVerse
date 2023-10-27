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

           /* if (context.UserRoles.Any()) return;

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

            if (context.ComponentTypes.Any()) return;

            var components = new ComponentType[]
            {
                new ComponentType() {Id=new Guid(), Name="About Me"},
                new ComponentType() {Id=new Guid(), Name="Achievements"},
                new ComponentType() {Id=new Guid(), Name="Primary Technology"},
                new ComponentType() {Id=new Guid(), Name="Secondary Technology"},
                new ComponentType() {Id=new Guid(), Name="Known Technologies"},
                new ComponentType() {Id=new Guid(), Name="Interests"},
                new ComponentType() {Id=new Guid(), Name="Education"},
                new ComponentType() {Id=new Guid(), Name="Certificates"}
            };
            context.ComponentTypes.AddRangeAsync(components);
            context.SaveChanges();

            if (context.ComponentThemes.Any()) return;

            var themes = new ComponentTheme[]
            {
                new ComponentTheme() {Id=new Guid(), Name="Default"},
                new ComponentTheme() {Id=new Guid(), Name="Modern"},
                new ComponentTheme() {Id=new Guid(), Name="Simplistic"},
                new ComponentTheme() {Id=new Guid(), Name="Antique"},
                new ComponentTheme() {Id=new Guid(), Name="Cartoony"},
                new ComponentTheme() {Id=new Guid(), Name="Mono"},
                new ComponentTheme() {Id=new Guid(), Name="Retro"},
            };

            context.ComponentThemes.AddRangeAsync(themes);
            context.SaveChanges();
        }
    }
}
