using System.Security.AccessControl;
using Marten;

namespace Contact.Network.Service.Contact; 

public class ContactService : ApplicationService<Domain.Contact.Contact> {
    
    public ContactService(IDocumentSession documentSession) : base(documentSession) { }
    
    public sealed override async Task<Domain.Contact.Contact> Handle(object command) {
        var contact = command switch {
            Commands.CreateContact cmd => HandleCreate(() => Domain.Contact.Contact.RegisterNew(cmd.Id, cmd.UserId, cmd.FirstName, cmd.LastName)),
            Commands.RenameContact cmd => await HandleUpdate(cmd.Id, contact => contact.Rename(cmd.FirstName, cmd.LastName)),
            Commands.MarkContactAsDeleted cmd => await HandleUpdate(cmd.Id, contact => contact.MarkAsDeleted()),
            Commands.SpecifyCompany cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyCompany(cmd.CompanyName)),
            Commands.RemoveCompany cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveCompany()),
            Commands.SpecifyJobTitle cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyJobTitle(cmd.JobTitle)),
            Commands.RemoveJobTitle cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveJobTitle()),
            Commands.SpecifyBirthDay cmd => await HandleUpdate(cmd.Id, contact => contact.SpecifyBirthDay(cmd.BirthDay)),
            Commands.RemoveBirthDay cmd => await HandleUpdate(cmd.Id, contact => contact.RemoveBirthDay()),
            Commands.AddPhoneNumber cmd => await HandleUpdate(cmd.Id, contact => contact.AddPhoneNumber(cmd.PhoneNumberId, cmd.PhoneNumber, cmd.Label)),
            Commands.RemovePhoneNumber cmd => await HandleUpdate(cmd.Id, contact => contact.RemovePhoneNumber(cmd.PhoneNumberId)),
            Commands.AddEmail cmd => await HandleUpdate(cmd.Id, contact => contact.AddEmail(cmd.EmailId, cmd.Email, cmd.Label)),
            _ => throw new NotSupportedException()
        };
        
        await DocumentSession.SaveChangesAsync();

        return contact;
    }

    public sealed override async Task<Domain.Contact.Contact?> Load(Guid id) {
        return await DocumentSession.Events.AggregateStreamAsync<Domain.Contact.Contact>(id);
    }
}