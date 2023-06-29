namespace Contact.Network.Domain.TenantContext; 

public static class Events {
    public record PrivateTenantRegistered(Guid Id, string Name);

    public record OrganizationTenantRegistered(Guid Id, string Name);
    public record TenantRenamed(Guid Id, string Name);
}