using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class PlayerRoundState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private DiContainer _container;

        public PlayerRoundState(GameStateMachine gameStateMachine , DiContainer container) 
        {
            _gameStateMachine = gameStateMachine;
            _container = container;
        }

        public void Enter()
        {
            HUDController hud = _container.Resolve<HUDController>();

            SpawnCards(5);
            UpdateHUD(hud);
        }


        public void Exit()
        {
            HUDController hud = _container.Resolve<HUDController>();
            ICardSpawner cardSpawner = _container.Resolve<ICardSpawner>();

            hud.EndTurnButtonPressed -= StartEnemyRound;
            hud.SetButtonInteractable(false);
            cardSpawner.DespawnAllCards();
        }

        public void StartEnemyRound() => _gameStateMachine.Enter<EnemyRoundState>();
        
        private void UpdateHUD(HUDController hud)
        {
            hud.EndTurnButtonPressed += StartEnemyRound;
            hud.SetButtonInteractable(true);
        }

        private void SpawnCards(int num)
        {
            ICardSpawner cardSpawner = _container.Resolve<ICardSpawner>();
            IDeck deck = _container.Resolve<IDeck>(tag: "PlayerDeck");

            for (int i = 0; i < num; i++)
                cardSpawner.SpawnCardFromDeck(deck, new UnityEngine.Vector3());
        }
    }
}
