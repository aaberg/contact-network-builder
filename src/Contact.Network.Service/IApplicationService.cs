namespace Contact.Network.Service; 

public interface IApplicationService<T> {
    Task Handle(object command);

    Task<T?> Load(Guid id);
}