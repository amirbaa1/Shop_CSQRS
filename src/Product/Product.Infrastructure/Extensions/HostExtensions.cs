using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

using Polly;
using System.Data;

namespace Product.Infrastructure.Extensions;
public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider>? seeder = null) where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            if (context == null)
            {
                throw new InvalidOperationException("DbContext is not registered.");
            }

            try
            {
                logger.LogInformation("Checking for pending migrations for context {DbContextName}", typeof(TContext).Name);

                // Ensure that DbContext is properly opened
                context.Database.OpenConnection();

                // Check if the __EFMigrationsHistory table exists
                bool tableExists;
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'public' AND table_name = '__EFMigrationsHistory'";
                    command.CommandType = CommandType.Text;

                    tableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                }

                if (!tableExists)
                {
                    logger.LogInformation("The __EFMigrationsHistory table does not exist. Applying migrations.");

                    var retry = Policy.Handle<NpgsqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                            onRetry: (exception, retryCount, context) =>
                            {
                                logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                            });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                else
                {
                    logger.LogInformation("__EFMigrationsHistory table exists. Checking for pending migrations.");

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        logger.LogInformation("Pending migrations found. Applying migrations.");

                        var retry = Policy.Handle<NpgsqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

                        retry.Execute(() => InvokeSeeder(seeder, context, services));

                        logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                    }
                    else
                    {
                        logger.LogInformation("No pending migrations for context {DbContextName}", typeof(TContext).Name);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        return host;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider>? seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder?.Invoke(context, services);
    }
}
