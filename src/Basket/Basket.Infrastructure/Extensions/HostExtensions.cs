using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace Basket.Infrastructure.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider>? seeder = null) where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Checking for pending migrations for context {DbContextName}", typeof(TContext).Name);

                var tableExists = context.Database.ExecuteSqlRaw("SELECT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'public' AND tablename = '__EFMigrationsHistory')");

                if (tableExists == 0)
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
        }

        return host;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider>? seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder?.Invoke(context, services);
    }
}