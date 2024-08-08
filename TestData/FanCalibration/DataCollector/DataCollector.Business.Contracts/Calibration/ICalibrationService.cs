namespace DataCollector.Business.Contracts.Calibration;

/// <summary>
///     Provides functionality to manage calibration measurements
/// </summary>
public interface ICalibrationService
{
    /// <summary>
    ///     Begins to record measurements using the provided metadata
    /// </summary>
    /// <param name="newMetadata"></param>
    void StartMeasurement(MeasurementMetadata newMetadata);
    
    /// <summary>
    ///     Ends an ongoing measurement
    /// </summary>
    void EndMeasurement();

    /// <summary>
    ///     Returns the current status about the measurement progress
    /// </summary>
    /// <returns></returns>
    MeasurementStatus GetStatus();
}