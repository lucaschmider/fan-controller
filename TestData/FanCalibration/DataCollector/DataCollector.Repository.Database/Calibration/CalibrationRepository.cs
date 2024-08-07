using DataCollector.Repository.Contracts.Calibration;
using DataCollector.Repository.Contracts.Calibration.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataCollector.Repository.Database.Calibration;

/// <summary>
///     Provides read and write functionality by leveraging a postgres database
/// </summary>
/// <param name="context"></param>
internal class CalibrationRepository(CalibrationContext context) : ICalibrationRepository
{
    public Task<FanEntity[]> GetFansAsync() => context.Fans.ToArrayAsync();

    public Task<bool> CheckWhetherFanExistsAsync(string name) =>
        context.Fans.AnyAsync(fan => fan.Name.Equals(name));

    public async Task RegisterFanAsync(FanEntity fan)
    {
        context.Fans.Add(fan);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}