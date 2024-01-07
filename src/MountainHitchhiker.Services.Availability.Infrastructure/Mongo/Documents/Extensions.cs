using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.ValueObjects;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;

internal static class Extensions
{
    public static Resource AsEntity(this ResourceDocument document)
        => new Resource(document.Id, document.Tags,
            document.Reservations.Select(r => 
                new Reservation(r.TimeStamp.AsDateTime(), r.Priority)),
            document.Version);

    public static ResourceDocument AsDocument(this Resource resource)
        => new ResourceDocument
        {
            Id = resource.Id,
            Version = resource.Version,
            Tags = resource.Tags,
            Reservations = resource.Reservations.Select(r => 
                new ReservationDocument
            {
                TimeStamp = r.DateTime.AsDaysSinceEpoch(),
                Priority = r.Priority
            })
        };

    internal static int AsDaysSinceEpoch(this DateTime dateTime)
        => (dateTime - new DateTime()).Days;

    internal static DateTime AsDateTime(this int daysSinceEpoch)
        => new DateTime().AddDays(daysSinceEpoch);
}