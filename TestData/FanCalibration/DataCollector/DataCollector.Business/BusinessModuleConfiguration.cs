namespace DataCollector.Business;

/// <summary>
///     The configuration that is required for the business module
/// </summary>
public class BusinessModuleConfiguration
{
    /// <summary>
    ///     The interval at which measurements shall be saved to the database
    /// </summary>
    public TimeSpan SaveInterval { get; set; }
}