using DataCollector.Controllers.Calibration.Models;
using DataCollector.Repository.Contracts.Calibration.Entities;

namespace DataCollector.Controllers.Calibration.Extensions;

internal static class MappingExtensions
{
    internal static FanPreviewItem Map(this FanEntity entity)
    {
        return new FanPreviewItem(entity.Id, entity.Name);
    }
}