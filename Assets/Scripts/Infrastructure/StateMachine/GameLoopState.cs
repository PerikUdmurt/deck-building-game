using CardBuildingGame.Services.DI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly RoundStateMachine _roundStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine, DiContainer projectDiContainer)
        {
            _roundStateMachine = new RoundStateMachine(gameStateMachine, projectDiContainer);
        }

        public void Enter()
        {
            _roundStateMachine.Enter<NewRoomState>();
        }

        public void Exit()
        {
            
        }
    }
}