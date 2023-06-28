using JetBrains.Annotations;

namespace Contact.Network.Domain.TenantContext; 

[PublicAPI]
public class Tenant : Aggregate {

    /// <summary>
    /// Required for serialization
    /// </summary>
    public Tenant() {}

    public string?    Name { get; set; }
    public TenantType Type { get; set; } = TenantType.Private;

    public static Tenant RegisterNewPrivateTenant(Guid id, string name) {
        var tenant = new Tenant();
        tenant.HandleEvent(new Events.PrivateTenantRegistered(id, name), tenant.Apply);
        return tenant;
    }

    public static Tenant RegisterNewOrganisationTenant(Guid id, string name) {
        var tenant = new Tenant();
        tenant.HandleEvent(new Events.OrganizationTenantRegistered(id, name), tenant.Apply);
        return tenant;
    }
    
    public void Rename(string newName) {
        HandleEvent(new Events.TenantRenamed(Id, newName), Apply);
    }

    #region Apply Events

    private void Apply(Events.PrivateTenantRegistered @event) {
        Id = @event.Id;
        Name = @event.Name;
        Type = TenantType.Private;
    }

    private void Apply(Events.OrganizationTenantRegistered @event) {
        Id = @event.Id;
        Name = @event.Name;
        Type = TenantType.Organization;
    }

    private void Apply(Events.TenantRenamed @event) {
        Name = @event.Name;
    }
    
    #endregion

    protected override void EnsureValidState() {
        if (string.IsNullOrEmpty(Name)) {
            throw new DomainException("Tenant must have a name");
        }
    }
}