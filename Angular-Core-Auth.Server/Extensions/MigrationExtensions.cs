using Angular_Core_Auth.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Angular_Core_Auth.Server.Extensions
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database. Will create the database if it does not already exist.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
