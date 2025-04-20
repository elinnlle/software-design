using Microsoft.AspNetCore.Mvc;
using MOSZoo.Application.Interfaces;
using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Enums;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository      _animals;
    private readonly IAnimalTransferService _transfer;

    public AnimalsController(IAnimalRepository animals,
        IAnimalTransferService transfer)
    {
        _animals  = animals;
        _transfer = transfer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _animals.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) =>
        (await _animals.GetAsync(id)) is { } a ? Ok(a) : NotFound();

    [HttpPost]
    public async Task<IActionResult> Create(CreateAnimalRequest dto)
    {
        var animal = new Animal(dto.Species, dto.Name, dto.DateOfBirth,
            dto.Gender, dto.FavouriteFood);
        await _animals.AddAsync(animal);
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _animals.RemoveAsync(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/move")]
    public async Task<IActionResult> Move(Guid id, MoveAnimalRequest r)
    {
        await _transfer.TransferAsync(id, r.ToEnclosureId);
        return NoContent();
    }

    // ---- DTO local ----
    public record CreateAnimalRequest(
        Species Species,
        string  Name,
        DateTime DateOfBirth,
        Gender  Gender,
        string  FavouriteFood);

    public record MoveAnimalRequest(Guid ToEnclosureId);
}