using Contact.Network.Domain.TenantContext;
using Marten;

namespace Contact.Network.Service.Tenant; 

public class TenantService : ApplicationService<Domain.TenantContext.Tenant> {
    
    public TenantService(IDocumentSession documentSession) : base(documentSession) { }

    public override async Task<Domain.TenantContext.Tenant> Handle(object command) {
        var tenant = command switch {
            Commands.RegisterPrivateTenant cmd => HandleCreate(() => Domain.TenantContext.Tenant.RegisterNewPrivateTenant(cmd.Id, cmd.Name)),
            Commands.RegisterOrganizationTenant cmd => HandleCreate(() => Domain.TenantContext.Tenant.RegisterNewOrganisationTenant(cmd.Id, cmd.Name)),
            Commands.RenameTenant cmd => await HandleUpdate(cmd.Id, tenant => tenant.Rename(cmd.Name)),
            _ => throw new NotSupportedException()
        };

        await DocumentSession.SaveChangesAsync();

        return tenant;
    }

    public override async Task<Domain.TenantContext.Tenant?> Load(Guid id) {
        return await DocumentSession.Events.AggregateStreamAsync<Domain.TenantContext.Tenant>(id);
    }
}