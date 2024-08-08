namespace DataCollector.Business.Contracts.Calibration;

/// <summary>
///     Contains metadata about a recording
/// </summary>
/// <param name="FanId">The id of the fan that is being recorded</param>
public record MeasurementMetadata(Guid FanId);

/// <summary>
///     Contains information about an ongoing recording
/// </summary>
/// <param name="IsRunning"></param>
/// <param name="FanId"></param>
public record MeasurementStatus(bool IsRunning, Guid? FanId);