namespace Contact.Network.Service.Contact;

public static class Commands {

    public record CreateContact(Guid Id, Guid UserId, string? FirstName, string LastName);
    public record RenameContact(Guid Id, string? FirstName, string LastName);
    public record MarkContactAsDeleted(Guid Id);
    public record SpecifyCompany(Guid Id, string CompanyName);
    public record RemoveCompany(Guid Id);
    public record SpecifyJobTitle(Guid Id, string JobTitle);
    public record RemoveJobTitle(Guid Id);
    public record SpecifyBirthDay(Guid Id, DateTime BirthDay);
    public record RemoveBirthDay(Guid Id);
    public record AddPhoneNumber(Guid Id, Guid PhoneNumberId, string PhoneNumber, string Label);
    public record AddEmail(Guid Id, Guid EmailId, string Email, string Label);
}