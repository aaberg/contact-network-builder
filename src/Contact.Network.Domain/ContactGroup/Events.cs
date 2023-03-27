namespace Contact.Network.Domain.ContactGroup; 

public static class Events {
    
    public record ContactGroupCreated(Guid ContactGroupId, string Name);
    
    public record ContactGroupRenamed(Guid ContactGroupId, string Name);
    
    public record ContactAdded(Guid ContactGroupId, Guid ContactId);
    
    public record ContactRemoved(Guid ContactGroupId, Guid ContactId);
}