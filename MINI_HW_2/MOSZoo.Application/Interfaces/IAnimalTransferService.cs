namespace MOSZoo.Application.Interfaces;

public interface IAnimalTransferService
{
    Task TransferAsync(Guid animalId, Guid toEnclosureId);
}