namespace MountainHitchhiker.Services.Availability.Core.ValueObjects;

public struct Reservation : IEquatable<Reservation>
{
    public DateTime DateTime { get; }
    public int Priority { get; }

    public Reservation(DateTime dateTime, int priority)
        => (DateTime, Priority) = (dateTime, priority);
    
    public bool Equals(Reservation other)
        => DateTime.Equals(other.DateTime.Date) && Priority == other.Priority;

    public override bool Equals(object? obj)
        => obj is Reservation other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(DateTime, Priority);
}