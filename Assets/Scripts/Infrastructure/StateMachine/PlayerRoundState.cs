using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Statuses;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;
using System;
using System.Linq;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class PlayerRoundState : IState
    {
        private readonly RoundStateMachine _roundStateMachine;
        private DiContainer _container;

        public PlayerRoundState(RoundStateMachine roundStateMachine , DiContainer container) 
        {
            _roundStateMachine = roundStateMachine;
            _container = container;
        }

        public void Enter()
        {
            HUDController hud = _container.Resolve<HUDController>();
            PrepareEnemyAttack();
            RestorePlayer();
            ExecuteAllPlayerStatus();
            SpawnCards(5);
            UpdateHUD(hud);
        }

        private void ExecuteAllPlayerStatus()
        {
            LevelData levelData = _container.Resolve<LevelData>();
            StatusPlayer statusPlayer = new();

            var players = from player in levelData.Characters
                          where player.TargetLayer == TargetLayer.Player
                          select player;

            foreach (var player in players)
            {
                statusPlayer.ExecuteAllStatuses(player);
            }
        }

        private void RestorePlayer()
        {
            LevelData levelData = _container.Resolve<LevelData>();

            var players = from player in levelData.Characters
                          where player.TargetLayer == TargetLayer.Player
                          select player;

            foreach (var player in players)
            {
                player.CardPlayer.Energy.RestoreEnergy();
            }
        }

        private void PrepareEnemyAttack()
        {
            LevelData levelData = _container.Resolve<LevelData>();
            
            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer == TargetLayer.Enemy
                          select enemy;
             
            foreach (var enemy in enemies) 
            {
                enemy.CardPlayer.PrepareCard();
            }
        }

        public void Exit()
        {
            HUDController hud = _container.Resolve<HUDController>();
            ICardSpawner cardSpawner = _container.Resolve<ICardSpawner>();

            hud.EndTurnButtonPressed -= StartEnemyRound;
            hud.SetButtonInteractable(false);
            cardSpawner.DespawnAllCards();
        }

        public void StartEnemyRound() => _roundStateMachine.Enter<EnemyRoundState>();
        
        private void UpdateHUD(HUDController hud)
        {
            hud.EndTurnButtonPressed += StartEnemyRound;
            hud.SetButtonInteractable(true);
        }

        private void SpawnCards(int num)
        {
            ICardSpawner cardSpawner = _container.Resolve<ICardSpawner>();
            Character character = _container.Resolve<Character>(tag: "Hero");
            for (int i = 0; i < num; i++)
                cardSpawner.SpawnCardFromDeck(character.CardPlayer.Deck, new UnityEngine.Vector3(0,-10,0));
        }
    }
}
