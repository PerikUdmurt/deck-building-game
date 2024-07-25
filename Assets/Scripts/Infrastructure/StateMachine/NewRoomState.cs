using CardBuildingGame.Datas;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;
using System;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class NewRoomState : IState
    {
        private readonly RoundStateMachine _roundStateMachine;
        private readonly DiContainer _projectContainer;

        public NewRoomState(RoundStateMachine roundStateMachine, DiContainer projectContainer) 
        {
            _roundStateMachine = roundStateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter()
        {
            UpdateHUD();
            SpawnEnemies();

            _roundStateMachine.Enter<PlayerRoundState>();
        }

        public void Exit()
        {
            
        }
       
        private void UpdateHUD()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            LevelData levelData = _projectContainer.Resolve<LevelData>();

            levelData.CurrentRoom += 1;
            hud.SetRoomText(levelData.CurrentRoom, levelData.MaxRoom);
        }

        private void SpawnEnemies()
        {
            ICharacterSpawner characterSpawner = _projectContainer.Resolve<ICharacterSpawner>();
            Vector3 enemyPosition = _projectContainer.Resolve<Vector3>("EnemyPosition");
            Vector3 delta = _projectContainer.Resolve<Vector3>("DeltaEnemySpawnOffset");

            characterSpawner.SpawnCharacterFromStaticData("Enemy1", "EnemyDeck1", enemyPosition);
            characterSpawner.SpawnCharacterFromStaticData("Enemy2", "EnemyDeck2", enemyPosition + delta);
        }
    }
}
