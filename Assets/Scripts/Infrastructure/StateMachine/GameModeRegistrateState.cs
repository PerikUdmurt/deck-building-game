using CardBuildingGame.Infrastructure.GameScenario;
using CardBuildingGame.Services.DI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameModeRegistrateState : IPayloadedState<GameMode>
    {
        public GameModeRegistrateState(GameStateMachine gameStateMachine, DiContainer projectContainer) 
        { 
        
        }

        public void Enter(GameMode payload)
        {


            switch (payload)
            {
                case GameMode.Test:
                    RegistrateTestMode();
                    break;
                case GameMode.Infinite:
                    RegistrateInfiniteMode();
                    break;
            }
        }

        public void Exit()
        {

        }

        private void RegistrateTestMode()
        {

        }

        private void RegistrateInfiniteMode()
        {

        }
    }
}