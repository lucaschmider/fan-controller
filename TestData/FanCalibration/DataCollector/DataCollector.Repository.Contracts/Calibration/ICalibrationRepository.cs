using DataCollector.Repository.Contracts.Calibration.Entities;

namespace DataCollector.Repository.Contracts.Calibration;

/// <summary>
///     Provides the functionality to read and write to a persistent data store
/// </summary>
public interface ICalibrationRepository
{
    /// <summary>
    ///     Returns a list of all fans
    /// </summary>
    /// <returns></returns>
    Task<FanEntity[]> GetFansAsync();

    /// <summary>
    ///     Checks whether a fan with the specified name exists
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> CheckWhetherFanExistsAsync(string name);

    /// <summary>
    ///     Creates a fan registration
    /// </summary>
    /// <param name="fan"></param>
    /// <returns></returns>
    Task RegisterFanAsync(FanEntity fan);

    /// <summary>
    ///     Adds multiple measurements to the database
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task AddMeasurementsAsync(IEnumerable<MeasurementEntity> entities);
}