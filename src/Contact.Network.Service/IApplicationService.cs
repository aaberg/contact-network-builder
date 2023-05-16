namespace Contact.Network.Service; 

public interface IApplicationService<T> {
    Task<T> Handle(object command);

    Task<T?> Load(Guid id);
}