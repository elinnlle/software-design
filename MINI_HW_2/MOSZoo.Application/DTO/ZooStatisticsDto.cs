namespace MOSZoo.Application.DTO;

/// <summary>
/// Сводная статистика по зоопарку.
/// </summary>
public record ZooStatisticsDto(int TotalAnimals, int FreeEnclosures, int TotalEnclosures);