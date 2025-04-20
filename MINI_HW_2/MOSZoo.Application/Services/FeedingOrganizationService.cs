using MediatR;
using MOSZoo.Application.Interfaces;
using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Events;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Application.Services;

public class FeedingOrganizationService : IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _repo;
    private readonly IMediator                  _mediator;

    public FeedingOrganizationService(IFeedingScheduleRepository repo, IMediator mediator)
    {
        _repo     = repo;
        _mediator = mediator;
    }

    public async Task<FeedingSchedule> AddScheduleAsync(Guid animalId, DateTimeOffset time, string foodType)
    {
        var schedule = new FeedingSchedule(animalId, time, foodType);
        await _repo.AddAsync(schedule);
        return schedule;
    }

    public async Task MarkDoneAsync(Guid feedingId)
    {
        var schedule = await _repo.GetAsync(feedingId) ?? throw new KeyNotFoundException();
        schedule.MarkDone();
        await _repo.UpdateAsync(schedule);

        await _mediator.Publish(new FeedingTimeEvent(schedule.Id, schedule.AnimalId, schedule.FeedingTime));
    }
}