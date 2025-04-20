using Microsoft.AspNetCore.Mvc;
using MOSZoo.Application.Interfaces;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingController : ControllerBase
{
    private readonly IFeedingScheduleRepository  _repo;
    private readonly IFeedingOrganizationService _service;

    public FeedingController(IFeedingScheduleRepository repo,
        IFeedingOrganizationService service)
    {
        _repo    = repo;
        _service = service;
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> GetSchedule() =>
        Ok(await _repo.GetAllAsync());

    [HttpPost("schedule")]
    public async Task<IActionResult> AddSchedule(AddScheduleRequest r)
    {
        var s = await _service.AddScheduleAsync(r.AnimalId, r.Time, r.FoodType);
        return Created(s.Id.ToString(), s);
    }

    [HttpPost("schedule/{id:guid}/done")]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        await _service.MarkDoneAsync(id);
        return NoContent();
    }

    // DTO
    public record AddScheduleRequest(
        Guid AnimalId,
        DateTimeOffset Time,
        string FoodType);
}