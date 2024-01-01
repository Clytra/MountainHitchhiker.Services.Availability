using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.Events;
using MountainHitchhiker.Services.Availability.Core.ValueObjects;

namespace MountainHitchhiker.Services.Availability.Core.Exceptions;

public class ReservationCanceled : IDomainEvent
{
    public Resource Resource { get; }
    public Reservation Reservation { get; }

    public ReservationCanceled(Resource resource, Reservation reservation)
        => (Resource, Reservation) = (resource, reservation);
}