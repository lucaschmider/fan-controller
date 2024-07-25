namespace DataCollector.Controllers.Calibration.Models;

/// <summary>
///     The request that is sent to create a new fan
/// </summary>
/// <param name="Name"></param>
public record CreateFanRequest(string Name);