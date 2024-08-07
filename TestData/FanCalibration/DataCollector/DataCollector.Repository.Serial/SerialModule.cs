using DataCollector.Repository.Contracts.Measurement;
using DataCollector.Repository.Serial.Measurement;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollector.Repository.Serial;

public static class SerialModule
{
    public static IServiceCollection AddSerial(this IServiceCollection services, SerialModuleConfiguration configuration)
    {
        _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
        
        return services
            .AddSingleton(configuration)
            .AddSingleton<IMeasurementRepository, MeasurementRepository>();
    }
}