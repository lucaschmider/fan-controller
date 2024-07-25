using DataCollector.Repository.Calibration;
using DataCollector.Repository.Contracts.Calibration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollector.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContext<CalibrationContext>(options =>
            {
                options.UseNpgsql(connectionString);
            })
            .AddScoped<ICalibrationRepository, CalibrationRepository>();
    }
}