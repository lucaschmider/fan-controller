namespace DataCollector.Repository.Contracts.Calibration.Entities;

/// <summary>
///     Represents a single measurement of a specific fan
/// </summary>
public class MeasurementEntity
{
    /// <summary>
    ///     The id of the measurement
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     The id of the fan that has been recorded
    /// </summary>
    public Guid FanId { get; set; }
    
    /// <summary>
    ///     The duty cycle at the time of measurement
    /// </summary>
    public int DutyCycle { get; set; }
    
    /// <summary>
    ///     The speed (in rpm) that has been measured
    /// </summary>
    public int Speed { get; set; }
    
    /// <summary>
    ///     The timestamp at which the measurement has been recorded
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     A reference to the fan that was recorded
    /// </summary>
    public virtual FanEntity Fan { get; set; }
}