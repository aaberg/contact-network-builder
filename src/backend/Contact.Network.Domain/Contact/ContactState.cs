using Contact.Network.Domain.TenantContext;

namespace Contact.Network.Domain.Contact; 

public record ContactState(Guid Id) : AggregateState(Id) {
    public Guid              UserId       { get; init; }
    public string            LastName     { get; init; }
    public string?           FirstName    { get; init; }
    public bool              IsDeleted    { get; init; }
    public string?           Company      { get; init; }
    public string?           JobTitle     { get; init; }
    public DateTime?         BirthDay     { get; init; }
    public List<PhoneNumber> PhoneNumbers { get; } = new();
    public List<Email>       Emails       { get; } = new();
    
}