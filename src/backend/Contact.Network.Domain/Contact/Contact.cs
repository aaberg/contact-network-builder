using JetBrains.Annotations;

namespace Contact.Network.Domain.Contact; 

[PublicAPI]
public class Contact : Aggregate {

    /// <summary>
    /// Required for serialization
    /// </summary>
    public Contact() {
        
    }

    public Guid              UserId       { get; set; }
    public string?           FirstName    { get; set; }
    public string?           LastName     { get; set; } = null!;
    public bool              IsDeleted    { get; set; }
    public string?           Company      { get; set; }
    public string?           JobTitle     { get; set; }
    public DateTime?         BirthDay     { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; } = new();
    public List<Email>       Emails       { get; } = new();
    
    public static Contact RegisterNew(Guid id, Guid userId, string? firstName, string lastName) {
        var c = new Contact();
        c.HandleEvent(new Events.ContactCreated(id, userId, firstName, lastName), c.Apply);
        return c;
    }

    public void Rename(string? firstName, string lastName) {
        HandleEvent(new Events.ContactRenamed(Id, firstName, lastName), Apply);
    }
    
    public void MarkAsDeleted() {
        HandleEvent(new Events.ContactMarkedAsDeleted(Id), Apply);
    }

    public void SpecifyCompany(string companyName) {
        HandleEvent(new Events.CompanySpecified(Id, companyName), Apply);
    }
    
    public void RemoveCompany() {
        HandleEvent(new Events.CompanyRemoved(Id), Apply);
    }
    
    public void SpecifyJobTitle(string jobTitle) {
        HandleEvent(new Events.JobTitleSpecified(Id, jobTitle), Apply);
    }
    
    public void RemoveJobTitle() {
        HandleEvent(new Events.JobTitleRemoved(Id), Apply);
    }
    
    public void SpecifyBirthDay(DateTime birthDay) {
        HandleEvent(new Events.BirthDaySpecified(Id, birthDay), Apply);
    }
    
    public void RemoveBirthDay() {
        HandleEvent(new Events.BirthDayRemoved(Id), Apply);
    }
    
    public void AddPhoneNumber(Guid phoneNumberId, string phoneNumber, string label) {
        HandleEvent(new Events.PhoneNumberAdded(Id, phoneNumberId, phoneNumber, label), Apply);
    }
    
    public void RemovePhoneNumber(Guid phoneNumberId) {
        HandleEvent(new Events.PhoneNumberRemoved(Id, phoneNumberId), Apply);
    }
    
    public void AddEmail(Guid emailId, string email, string label) {
        HandleEvent(new Events.EmailAdded(Id, emailId, email, label), Apply);
    }
    
    public void RemoveEmail(Guid emailId) {
        HandleEvent(new Events.EmailRemoved(Id, emailId), Apply);
    }
    
    # region Apply Events

    private void Apply(Events.ContactCreated @event) {
        Id = @event.Id;
        UserId = @event.UserId;
        FirstName = @event.FirstName;
        LastName = @event.LastName;
    }

    private void Apply(Events.ContactRenamed @event) {
        FirstName = @event.FirstName;
        LastName = @event.LastName;
    }

    private void Apply(Events.ContactMarkedAsDeleted @event) => IsDeleted = true;

    private void Apply(Events.CompanySpecified @event) => Company = @event.CompanyName;
    private void Apply(Events.CompanyRemoved @event)   => Company = null;

    private void Apply(Events.JobTitleSpecified @event) => JobTitle = @event.JobTitle;
    private void Apply(Events.JobTitleRemoved @event)   => JobTitle = null;

    private void Apply(Events.BirthDaySpecified @event) => BirthDay = @event.BirthDay;
    private void Apply(Events.BirthDayRemoved @event)   => BirthDay = null;

    private void Apply(Events.PhoneNumberAdded @event) => PhoneNumbers.Add(new PhoneNumber
        { Id = @event.PhoneNumberId, Label = @event.Label, Value = @event.PhoneNumber });

    private void Apply(Events.PhoneNumberRemoved @event) => PhoneNumbers.RemoveAll(x => x.Id == @event.PhoneNumberId);

    private void Apply(Events.EmailAdded @event) => Emails.Add(new Email
        { Id = @event.EmailId, Label = @event.Label, Value = @event.Email });

    private void Apply(Events.EmailRemoved @event) => Emails.RemoveAll(x => x.Id == @event.EmailId);
    
    #endregion

    protected override void EnsureValidState() {
        // All contacts must have some kind of name
        if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName)) {
            throw new DomainException("Contact must have a name");
        }
    }
}

public record PhoneNumber {
    public required Guid    Id    { get; init; }
    public required string  Value { get; init; }
    public          string? Label { get; init; }
}

public record Email {
    public required Guid    Id    { get; init; }
    public required string  Value { get; init; }
    public          string? Label { get; init; }
}