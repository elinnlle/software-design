using Microsoft.AspNetCore.Mvc;
using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Enums;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _repo;

    public EnclosuresController(IEnclosureRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateEnclosureRequest dto)
    {
        var enc = new Enclosure(dto.Name, dto.Type, dto.MaxCapacity);
        await _repo.AddAsync(enc);
        return Created(enc.Id.ToString(), enc);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.RemoveAsync(id);
        return NoContent();
    }

    // DTO
    public record CreateEnclosureRequest(
        string Name,
        EnclosureType Type,
        int MaxCapacity);
}