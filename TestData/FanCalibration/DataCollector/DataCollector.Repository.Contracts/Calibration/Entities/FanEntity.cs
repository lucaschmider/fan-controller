namespace DataCollector.Repository.Contracts.Calibration.Entities;

/// <summary>
///     Represents a specific fan
/// </summary>
public class FanEntity
{
    /// <summary>
    ///     The id of the fan
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     The name of the fan
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The measurements that have been recorded for the fan
    /// </summary>
    public virtual IEnumerable<MeasurementEntity> Measurements { get; set; }
}