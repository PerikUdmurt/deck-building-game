namespace CardBuildingGame.Infrastructure
{
    public interface IState: IExitableState
    {
        void Enter();
    }
}