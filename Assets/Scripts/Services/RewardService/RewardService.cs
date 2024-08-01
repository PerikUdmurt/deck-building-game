using YG;
using YGameTemplate.Infrastructure.EventBus;

namespace YGameTemplate.Services.Rewards
{
    public class RewardService : IRewardService
    {
        private readonly BaseEventBus _EventBus;

        public RewardService()
        {
            _EventBus = new BaseEventBus();
            YandexGame.RewardVideoEvent += Rewarded;
        }

        public void ShowRewardAd(RewardType reward)
        {
            YandexGame.RewVideoShow((int)reward);
        }

        public void Trigger(RewardType reward)
        {
            switch (reward)
            {
                case RewardType.Reward1:
                    Trigger(new Reward1());
                    break;
                
                case RewardType.Reward2:
                    Trigger(new Reward2());
                    break;
            }
        }

        public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
            => ((IEventBus)_EventBus).Register(receiver);

        public void Unregister<T>(IEventReceiver<T> reciever) where T : struct, IEvent
            => ((IEventBus)_EventBus).Unregister(reciever);
       
        public void Trigger<T>(T @event) where T : struct, IEvent
            => ((IEventBus)_EventBus).Trigger(@event);
        
        private void Rewarded(int rewardID) => Trigger((RewardType)rewardID);
    }
}