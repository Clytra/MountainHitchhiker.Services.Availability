namespace MountainHitchhiker.Services.Availability.Application.DTO;

public class ResourceDto
{
    public Guid Id { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public IEnumerable<ReservationDto> Reservations { get; set; }
}