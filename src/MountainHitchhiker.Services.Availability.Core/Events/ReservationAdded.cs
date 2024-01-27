using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.ValueObjects;

namespace MountainHitchhiker.Services.Availability.Core.Events;

public class ReservationAdded : IDomainEvent
{
    public Resource Resource { get; }
    public Reservation Reservation { get; }

    public ReservationAdded(Resource resource, Reservation reservation)
        => (Resource, Reservation) = (resource, reservation);
}