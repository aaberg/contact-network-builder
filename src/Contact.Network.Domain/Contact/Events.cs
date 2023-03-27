namespace Contact.Network.Domain.Contact; 

public static class Events {
    
    public record ContactCreated(Guid Id, string? FirstName, string LastName);

    public record ContactRenamed(Guid Id, string? FirstName, string LastName);

    public record ContactMarkedAsDeleted(Guid Id);

    public record CompanySpecified(Guid Id, string CompanyName);
    public record CompanyRemoved(Guid Id);

    public record JobTitleSpecified(Guid Id, string JobTitle);
    public record JobTitleRemoved(Guid Id);
    
    public record BirthDaySpecified(Guid Id, DateTime BirthDay);
    public record BirthDayRemoved(Guid Id);

    public record PhoneNumberAdded(Guid Id, Guid PhoneNumberId, string PhoneNumber, string Label);
    
    public record EmailAdded(Guid Id, Guid EmailId, string Email, string Label);
    
    
}