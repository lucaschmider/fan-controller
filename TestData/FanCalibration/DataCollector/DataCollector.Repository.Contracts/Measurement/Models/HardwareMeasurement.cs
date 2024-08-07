namespace DataCollector.Repository.Contracts.Measurement.Models;

/// <summary>
///     Represents a specific measurement that has been recorded
/// </summary>
/// <param name="Timestamp">The time at which the measurement has been capruted</param>
/// <param name="DutyCycle">The duty cycle (as a percentage, from 0 to 100) at the time</param>
/// <param name="Speed">The speed in rpm</param>
public record HardwareMeasurement(DateTime Timestamp, int DutyCycle, int Speed);