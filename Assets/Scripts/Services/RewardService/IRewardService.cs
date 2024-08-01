using YGameTemplate.Infrastructure.EventBus;

namespace YGameTemplate.Services.Rewards
{
    public interface IRewardService : IEventBus
    {
        void ShowRewardAd(RewardType reward);
        void Trigger(RewardType reward);
    }
}