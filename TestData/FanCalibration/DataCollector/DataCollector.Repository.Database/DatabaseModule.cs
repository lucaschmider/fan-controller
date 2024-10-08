﻿using DataCollector.Repository.Contracts.Calibration;
using DataCollector.Repository.Database.Calibration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollector.Repository.Database;

public static class DatabaseModule
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        _ = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        
        return services
            .AddDbContext<CalibrationContext>(options =>
            {
                options.UseNpgsql(connectionString);
            })
            .AddScoped<ICalibrationRepository, CalibrationRepository>();
    }

    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CalibrationContext>();
        db.Database.Migrate();
    }
}