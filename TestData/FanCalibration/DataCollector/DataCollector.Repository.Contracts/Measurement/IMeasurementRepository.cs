using DataCollector.Repository.Contracts.Measurement.Models;

namespace DataCollector.Repository.Contracts.Measurement;

/// <summary>
///     Provides functionality to listen for measurements at hardware level
/// </summary>
public interface IMeasurementRepository : IDisposable
{
    /// <summary>
    ///     Adds a listener, identified by <paramref name="tag"/>.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="onMeasurement"></param>
    void AddListener(string tag, Action<HardwareMeasurement> onMeasurement);

    /// <summary>
    ///     Removes the listener identified by <paramref name="tag"/>. If no listener
    ///     is identified by the tag, nothing will happen.
    /// </summary>
    /// <param name="tag"></param>
    void RemoveListener(string tag);

    /// <summary>
    ///     Initializes the underlying hardware.
    /// </summary>
    void Initialize();
}