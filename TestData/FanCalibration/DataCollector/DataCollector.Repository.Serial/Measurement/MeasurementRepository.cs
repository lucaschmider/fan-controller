using DataCollector.Repository.Contracts.Measurement;
using DataCollector.Repository.Contracts.Measurement.Models;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace DataCollector.Repository.Serial.Measurement;

internal class MeasurementRepository(SerialModuleConfiguration configuration, ILogger<MeasurementRepository> logger) : IMeasurementRepository
{
    private readonly SerialPort _serialPort = new (
        configuration.SerialPort, 
        configuration.BaudRate);
    private readonly Regex _pattern = new(configuration.Pattern, 
        RegexOptions.Compiled, 
        TimeSpan.FromMilliseconds(10));
    
    private readonly Dictionary<string, Action<HardwareMeasurement>> _listeners = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private bool _isInitialized = false;

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _listeners.Clear();
        _serialPort.Close();
        _serialPort.Dispose();
    }

    public void AddListener(string tag, Action<HardwareMeasurement> onMeasurement)
    {
        _listeners.Add(tag, onMeasurement);
    }

    public void RemoveListener(string tag)
    {
        _listeners.Remove(tag);
    }

    public void Initialize()
    {
        if (_isInitialized)
        {
            return;
        }
        _serialPort.Open();
        BeginRead();
        _isInitialized = true;
    }

    private void BeginRead()
    {
        Task.Run(() =>
        {
            var stringBuffer = string.Empty;
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                var availableData = _serialPort.ReadExisting();
                if (string.IsNullOrEmpty(availableData))
                {
                    continue;
                }

                foreach (var character in availableData)
                {
                    if (character == configuration.LineSeparator)
                    {
                        var match = _pattern.Match(stringBuffer);
                        if (match.Success)
                        {
                            var hasDutyCycle = int.TryParse(match.Groups[SerialConstants.DutyCycleGroup].Value, out var dutyCycle);
                            var hasSpeed = int.TryParse(match.Groups[SerialConstants.SpeedGroup].Value, out var fanSpeed);

                            if (!hasSpeed || !hasDutyCycle)
                            { 
                                logger
                                    .LogWarning("Received an invalid input: {stringBuffer}. hasDutyCycle: {hasDutyCycle}, hasSpeed: {hasSpeed}",
                                        stringBuffer, hasDutyCycle, hasSpeed);
                            }
                            else
                            {
                                var measurement = new HardwareMeasurement(DateTime.UtcNow, dutyCycle, fanSpeed);
                                foreach (var listener in _listeners.Values)
                                {
                                    listener.Invoke(measurement);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning("Received an invalid input: {stringBuffer}.", stringBuffer);
                        }

                        stringBuffer = string.Empty;
                    }

                    stringBuffer += character;
                }
            }
        }, _cancellationTokenSource.Token);
    }
}