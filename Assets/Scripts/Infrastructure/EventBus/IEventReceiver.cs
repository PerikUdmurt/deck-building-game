namespace YGameTemplate.Infrastructure.EventBus
{
    public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
    {
        void OnEvent(IEvent @event);
    }
}