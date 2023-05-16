using Contact.Network.Domain;
using Marten;

namespace Contact.Network.Service; 

public abstract class ApplicationService<TAggregate> : IApplicationService<TAggregate> where TAggregate : Aggregate {

    protected readonly IDocumentSession DocumentSession;
    
    protected ApplicationService(IDocumentSession documentSession) {
        DocumentSession = documentSession;
    }
    
    protected TAggregate HandleCreate(Func<TAggregate> create) {
        var aggregate = create();

        var events = aggregate.DequeueUncommittedEvents();
        var _ = DocumentSession.Events.StartStream<TAggregate>(aggregate.Id, events);

        return aggregate;
    }

    protected async Task<TAggregate> HandleUpdate(Guid id, Action<TAggregate> update) {
        var aggregate = await DocumentSession.Events.AggregateStreamAsync<TAggregate>(id);
        if (aggregate == null) throw new Exception($"No aggregate of type {typeof(TAggregate).Name} found with id {aggregate.Id.ToString()}");

        update(aggregate);
        var events = aggregate.DequeueUncommittedEvents();
        var nextVersion = aggregate.Version + events.Length;
        
        DocumentSession.Events.Append(id, nextVersion, events);
        
        return aggregate;
    }

    public abstract Task<TAggregate> Handle(object command);

    public abstract Task<TAggregate?> Load(Guid id);
}