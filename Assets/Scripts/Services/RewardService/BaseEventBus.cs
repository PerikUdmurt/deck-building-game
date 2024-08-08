using System;
using System.Collections.Generic;

namespace YGameTemplate.Infrastructure.EventBus
{
    public class BaseEventBus: IEventBus
    {
        protected private readonly Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receivers;
        protected private readonly Dictionary<int, WeakReference<IBaseEventReceiver>> _receiversHashToReference;

        public BaseEventBus()
        {
            _receivers = new();
            _receiversHashToReference = new();
        }

        public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
        {
            Type eventType = typeof(IBaseEventReceiver);
            if (!_receivers.ContainsKey(eventType))
                _receivers[eventType] = new List<WeakReference<IBaseEventReceiver>>();

            WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);

            _receivers[eventType].Add(reference);
            _receiversHashToReference[receiver.GetHashCode()] = reference;
        }

        public void Unregister<T>(IEventReceiver<T> reciever) where T : struct, IEvent
        {
            Type eventType = typeof(T);
            int receiverHash = reciever.GetHashCode();
            if (!_receivers.ContainsKey(eventType) || _receiversHashToReference.ContainsKey(receiverHash))
                return;

            WeakReference<IBaseEventReceiver> reference = _receiversHashToReference[receiverHash];

            _receivers[eventType].Remove(reference);
            _receiversHashToReference.Remove(receiverHash);
        }

        public void Trigger<T>(T @event) where T : struct, IEvent 
        {
            Type eventType = typeof(T);
            if (!_receivers.ContainsKey(eventType))
                return;

            foreach (WeakReference<IBaseEventReceiver> reference in _receivers[eventType])
            {
                if (reference.TryGetTarget(out var receiver))
                    ((IEventReceiver<T>)receiver).OnEvent(@event);
            }
        }
    }   
}