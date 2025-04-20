using MediatR;
using MOSZoo.Application.Interfaces;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Application.Services;

public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalRepository    _animals;
    private readonly IEnclosureRepository _enclosures;
    private readonly IMediator            _mediator;

    public AnimalTransferService(IAnimalRepository animals, IEnclosureRepository enclosures, IMediator mediator)
    {
        _animals    = animals;
        _enclosures = enclosures;
        _mediator   = mediator;
    }

    public async Task TransferAsync(Guid animalId, Guid toEnclosureId)
    {
        var animal = await _animals.GetAsync(animalId) 
                     ?? throw new KeyNotFoundException("Animal not found");
        var toEnclosure = await _enclosures.GetAsync(toEnclosureId) 
                          ?? throw new KeyNotFoundException("Enclosure not found");

        // убираем из текущего вольера (если был)
        if (animal.EnclosureId != Guid.Empty)
        {
            var fromEnclosure = await _enclosures.GetAsync(animal.EnclosureId);
            fromEnclosure?.RemoveAnimal(animal);
            await _enclosures.UpdateAsync(fromEnclosure!);
        }

        // кладём в новый
        toEnclosure.AddAnimal(animal);
        await _enclosures.UpdateAsync(toEnclosure);

        var @event = animal.MoveTo(toEnclosureId);
        await _animals.UpdateAsync(animal);

        await _mediator.Publish(@event);   // доменное событие
    }
}