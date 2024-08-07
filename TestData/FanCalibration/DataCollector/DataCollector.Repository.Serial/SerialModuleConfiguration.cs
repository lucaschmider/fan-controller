namespace DataCollector.Repository.Serial;

/// <summary>
///     The configuration that is required for the serial repository module to work
/// </summary>
public class SerialModuleConfiguration
{
    /// <summary>
    ///     The serial port to connect to
    /// </summary>
    public string SerialPort { get; set; }

    /// <summary>
    ///     The baud rate that shall be used for the connection
    /// </summary>
    public int BaudRate { get; set; }

    /// <summary>
    ///     The character that identifies the end of a line
    /// </summary>
    public char LineSeparator { get; set; }

    /// <summary>
    ///     The pattern that shall be used to parse incoming data.
    ///     The pattern should contain a named group 'dutyCycle' that captures the duty cycle and another named group
    ///     'speed' that shall capture the fan speed.
    /// </summary>
    public string Pattern { get; set; }
}