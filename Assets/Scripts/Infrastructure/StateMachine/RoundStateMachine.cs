using CardBuildingGame.Services.DI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class RoundStateMachine : BaseStateMachine
    {
        public RoundStateMachine(GameStateMachine gameStateMachine, DiContainer projectDiContainer)
        {
            _states = new()
            {
                [typeof(EnemyRoundState)] = new EnemyRoundState(gameStateMachine, this, projectDiContainer),
                [typeof(PlayerRoundState)] = new PlayerRoundState(this, projectDiContainer),
                [typeof(NewRoomState)] = new NewRoomState(this, projectDiContainer)
            };
        }
    }
}