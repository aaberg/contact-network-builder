using Contact.Network.Domain.TenantContext;

namespace Contact.Network.Domain; 

public abstract class Aggregate {

    public long Version { get; set; } = 0;
    
    public Guid Id { get; set; }

    private readonly Queue<object> _uncommittedEvents = new();

    public void Enqueue(object @event) => _uncommittedEvents.Enqueue(@event);

    public object[] DequeueUncommittedEvents() {
        var events = _uncommittedEvents.ToArray();
        _uncommittedEvents.Clear();
        return events;
    }

    protected abstract void EnsureValidState();

    protected void HandleEvent<TEvent>(TEvent @event, Action<TEvent> apply) where TEvent : notnull {
        Enqueue(@event);
        apply(@event);
        EnsureValidState();
    }
}