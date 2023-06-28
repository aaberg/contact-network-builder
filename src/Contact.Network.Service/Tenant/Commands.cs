using Contact.Network.Domain.TenantContext;

namespace Contact.Network.Service.Tenant; 

public static class Commands {
    public record RegisterPrivateTenant(Guid Id, string Name);

    public record RegisterOrganizationTenant(Guid Id, string Name);
    public record RenameTenant(Guid Id, string Name);
    
}