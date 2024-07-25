using DataCollector.Repository.Contracts.Calibration;
using DataCollector.Repository.Contracts.Calibration.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataCollector.Repository.Calibration;

internal class CalibrationContext(DbContextOptions<CalibrationContext> options) : DbContext(options)
{
    /// <summary>
    ///     The fans that have been registered
    /// </summary>
    public DbSet<FanEntity> Fans { get; set; }
}