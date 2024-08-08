using DataCollector.Business.Calibration;
using DataCollector.Business.Contracts.Calibration;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollector.Business;

public static class BusinessModule
{
    public static IServiceCollection AddBusiness(this IServiceCollection services, BusinessModuleConfiguration configuration)
    {
        return services
            .AddSingleton(configuration)
            .AddSingleton<ICalibrationService, CalibrationService>()
            .AddHostedService<CalibrationService>(provider =>
            {
                var service = provider.GetService<ICalibrationService>() as CalibrationService;
                return service ?? throw new ArgumentNullException(nameof(service));
            });
    }
}