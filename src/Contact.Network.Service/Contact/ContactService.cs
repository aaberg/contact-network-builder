using Marten;

namespace Contact.Network.Service.Contact; 

public class ContactService : IApplicationService<Domain.Contact.Contact> {

    private readonly IDocumentSession _documentSession;
    
    public ContactService(IDocumentSession documentSession) {
        _documentSession = documentSession;
    }
    
    public async Task Handle(object command) {
        var contract = command switch {
            Commands.CreateContact cmd => new Domain.Contact.Contact(cmd.Id, cmd.UserId, cmd.FirstName, cmd.LastName),
            Commands.RenameContact cmd => await HandleUpdate(cmd.Id, contact => contact.Rename(cmd.FirstName, cmd.LastName)),
            Commands.MarkContactAsDeleted cmd => await HandleUpdate(cmd.Id, contact => contact.MarkAsDeleted()),
            Commands.SpecifyCompany cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyCompany(cmd.CompanyName)),
            Commands.RemoveCompany cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveCompany()),
            Commands.SpecifyJobTitle cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyJobTitle(cmd.JobTitle)),
            Commands.RemoveJobTitle cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveJobTitle()),
            Commands.SpecifyBirthDay cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyBirthDay(cmd.BirthDay)),
            Commands.RemoveBirthDay cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveBirthDay()),
            Commands.AddPhoneNumber cmd => await HandleUpdate(cmd.Id, contact => contact.AddPhoneNumber(cmd.PhoneNumberId, cmd.PhoneNumber, cmd.Label)),
            Commands.AddEmail cmd => await HandleUpdate(cmd.Id, contact => contact.AddEmail(cmd.EmailId, cmd.Email, cmd.Label)),
            _ => throw new NotSupportedException()
        };
        
        await _documentSession.SaveChangesAsync();
    }

    private async Task<Domain.Contact.Contact> HandleUpdate(Guid id, Action<Domain.Contact.Contact> update) {
        var contact = await _documentSession.Events.AggregateStreamAsync<Domain.Contact.Contact>(id);
        if (contact == null) throw new Exception($"No contact found with id {contact.Id.ToString()}");

        update(contact);
        var events = contact.DequeueUncommittedEvents();
        var nextVersion = contact.Version + events.Length;
        
        _documentSession.Events.Append(id, nextVersion, events);
        
        return contact;
    }

    public async Task<Domain.Contact.Contact?> Load(Guid id) {
        return await _documentSession.LoadAsync<Domain.Contact.Contact>(id);
    }
}