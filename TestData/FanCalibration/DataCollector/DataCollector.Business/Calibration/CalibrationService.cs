using System.Timers;
using DataCollector.Business.Contracts.Calibration;
using DataCollector.Repository.Contracts.Calibration;
using DataCollector.Repository.Contracts.Calibration.Entities;
using DataCollector.Repository.Contracts.Measurement;
using DataCollector.Repository.Contracts.Measurement.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace DataCollector.Business.Calibration;

internal class CalibrationService(ICalibrationRepository calibrationRepository,
    IMeasurementRepository measurementRepository, 
    ILogger<CalibrationService> logger,
    BusinessModuleConfiguration configuration) : IHostedService, ICalibrationService
{
    private readonly string _listenerTag = $"{nameof(CalibrationService)}-{Guid.NewGuid()}";
    private readonly List<MeasurementEntity> _measurements = new();
    private readonly Timer _saveTimer = new(configuration.SaveInterval);

    private MeasurementMetadata? _metadata;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        measurementRepository.AddListener(_listenerTag, OnMeasurementReceived);
        _saveTimer.AutoReset = true;
        _saveTimer.Start();
        _saveTimer.Elapsed += SaveMeasurements;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _saveTimer.Stop();
        return Task.CompletedTask;
    }

    public void StartMeasurement(MeasurementMetadata newMetadata)
    {
        logger.LogInformation("Updating the metadata of the current measurement.");
        _metadata = newMetadata;
    }

    public void EndMeasurement()
    {
        logger.LogInformation("Removing the metadata to stop recording.");
        _metadata = null;
    }

    public MeasurementStatus GetStatus()
    {
        return new MeasurementStatus(_metadata != null, _metadata?.FanId);
    }

    private void OnMeasurementReceived(HardwareMeasurement measurement)
    {
        var currentMetadata = _metadata;
        if (currentMetadata == null)
        {
            return;
        }
        
        var entity = new MeasurementEntity
        {
            Id = Guid.NewGuid(),
            Speed = measurement.Speed,
            Timestamp = measurement.Timestamp,
            DutyCycle = measurement.DutyCycle,
            FanId = currentMetadata.FanId
        };
        _measurements.Add(entity);
    }
    
    private async void SaveMeasurements(object? sender, ElapsedEventArgs e)
    {
        logger.LogInformation("Trying to save measurements to the database.");
        if (!_measurements.Any())
        {
            logger.LogInformation("There are no new measurements to be saved.");
            return;
        }

        try
        {
            await calibrationRepository.AddMeasurementsAsync(_measurements).ConfigureAwait(false);
            logger.LogInformation("{measurementCount} measurements have been saved to the database.", _measurements.Count);
            _measurements.Clear();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unexpected error occurred while trying to save measurements to the database.");
        }
        
    }
}