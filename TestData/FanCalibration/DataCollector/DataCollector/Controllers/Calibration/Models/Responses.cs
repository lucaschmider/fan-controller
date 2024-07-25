namespace DataCollector.Controllers.Calibration.Models;

/// <summary>
///     A subset of a fan, that includes all details that are available right after registering the fan.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record FanPreviewItem(Guid Id, string Name); 