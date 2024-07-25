using CardBuildingGame.Services.DI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameStateMachine: BaseStateMachine
    {
        public GameStateMachine(DiContainer projectDiContainer)
        {
            _states = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, projectDiContainer),
                [typeof(GameLoopState)] = new GameLoopState(this, projectDiContainer),
            };
        }
    }
}