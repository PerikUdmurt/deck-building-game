using static YGameTemplate.Services.StatisticsService.Statistics;

namespace YGameTemplate.Services.StatisticsService
{
    public interface IStatisticsModifier
    {
        public void ModifyStatistics(string targetStat, ModifyType modifyType, int value);
    }
}
