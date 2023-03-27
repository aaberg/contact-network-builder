namespace Contact.Network.Domain.Contact; 

public class Contact : Aggregate {
    public Contact(Guid id, Guid userId, string? firstName, string lastName) {
        Id = id;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid              Id           { get; set; }
    public Guid              UserId       { get; set; }
    public string?           FirstName    { get; set; }
    public string            LastName     { get; set; }
    public bool              IsDeleted    { get; set; }
    public string?           Company      { get; set; }
    public string?           JobTitle     { get; set; }
    public DateTime?         BirthDay     { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; } = new();
    public List<Email>       Emails       { get; } = new();

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
    
    public void AddEmail(Guid emailId, string email, string label) {
        HandleEvent(new Events.EmailAdded(Id, emailId, email, label), Apply);
    }

    public void Apply(Events.ContactRenamed @event) {
        FirstName = @event.FirstName;
        LastName = @event.LastName;
    }
    
    public void Apply(Events.ContactMarkedAsDeleted @event) => IsDeleted = true;
    
    public void Apply(Events.CompanySpecified @event) => Company = @event.CompanyName;
    public void Apply(Events.CompanyRemoved @event)   => Company = null;

    public void Apply(Events.JobTitleSpecified @event) => JobTitle = @event.JobTitle;
    public void Apply(Events.JobTitleRemoved @event)   => JobTitle = null;

    public void Apply(Events.BirthDaySpecified @event) => BirthDay = @event.BirthDay;
    public void Apply(Events.BirthDayRemoved @event)   => BirthDay = null;

    public void Apply(Events.PhoneNumberAdded @event) => PhoneNumbers.Add(new PhoneNumber
        { Id = @event.PhoneNumberId, Label = @event.Label, Value = @event.PhoneNumber });

    public void Apply(Events.EmailAdded @event) => Emails.Add(new Email
        { Id = @event.EmailId, Label = @event.Label, Value = @event.Email });
}

public record PhoneNumber {
    public required Guid    Id    { get; set; }
    public required string  Value { get; set; }
    public required string? Label { get; set; }
}

public class Email {
    public Guid   Id    { get; set; }
    public string Value { get; set; }
    public string? Label { get; set; }
}