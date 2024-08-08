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
                [typeof(MainMenuState)] = new MainMenuState(this, projectDiContainer),
                [typeof(GameModeRegistrateState)] = new GameModeRegistrateState(this, projectDiContainer),
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
    }
}