namespace MountainHitchhiker.Services.Availability.Application.Exceptions;

public class CustomerNotFoundException : AppException
{
    public Guid CustomerId { get; }
    public override string Code => "customer_not_found";
    
    public CustomerNotFoundException(Guid customerId) 
        : base($"Customer with id: {customerId} was not found.")
    {
        CustomerId = customerId;
    }
}