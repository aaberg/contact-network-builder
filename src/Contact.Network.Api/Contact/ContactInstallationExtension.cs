using Contact.Network.Domain.Contact;
using Contact.Network.Service;
using Contact.Network.Service.Contact;
using Marten.Events;

namespace Contact.Network.Api.Contact; 

public static class ContactInstallationExtension {

    public static IServiceCollection InstallContactService(this IServiceCollection services) {
        services.AddScoped<IApplicationService<Domain.Contact.Contact>, ContactService>();
        return services;
    }

    public static void AddContactEventTypes(this IEventStoreOptions events) {
        events.AddEventTypes(new []{
            typeof(Events.ContactCreated),
            typeof(Events.ContactRenamed),
            typeof(Events.ContactMarkedAsDeleted),
            typeof(Events.CompanySpecified),
            typeof(Events.CompanyRemoved),
            typeof(Events.JobTitleSpecified),
            typeof(Events.JobTitleRemoved),
            typeof(Events.BirthDaySpecified),
            typeof(Events.BirthDayRemoved),
            typeof(Events.PhoneNumberAdded),
            typeof(Events.EmailAdded),
        });
    }
}