namespace YGameTemplate.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent;
        void Trigger<T>(T @event) where T : struct, IEvent;
        void Unregister<T>(IEventReceiver<T> reciever) where T : struct, IEvent;
    }
}