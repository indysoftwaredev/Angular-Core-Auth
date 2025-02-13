using Angular_Core_Auth.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Angular_Core_Auth.Server.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

    }
}
