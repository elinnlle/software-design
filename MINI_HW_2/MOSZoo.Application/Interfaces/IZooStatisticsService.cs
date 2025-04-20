using MOSZoo.Application.DTO;

namespace MOSZoo.Application.Interfaces;

public interface IZooStatisticsService
{
    Task<ZooStatisticsDto> GetAsync();
}