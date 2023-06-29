using Contact.Network.Domain.TenantContext;
using Contact.Network.Service;
using Contact.Network.Service.Tenant;
using Marten.Events;

namespace Contact.Network.Api.Contact; 

public static class TenantInstallationExtension {

    public static IServiceCollection InstallTenantService(this IServiceCollection services) {
        services.AddScoped<IApplicationService<Domain.TenantContext.Tenant>, TenantService>();
        return services;
    }

    public static void AddTenantEventTypes(this IEventStoreOptions events) {
        events.AddEventTypes(new []{
            typeof(Events.PrivateTenantRegistered),
            typeof(Events.TenantRenamed)
            
        });
    }
}