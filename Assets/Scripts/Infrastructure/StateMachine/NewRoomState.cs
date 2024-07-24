using CardBuildingGame.Datas;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;
using System;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class NewRoomState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _projectContainer;

        public NewRoomState(GameStateMachine gameStateMachine, DiContainer projectContainer) 
        {
            _gameStateMachine = gameStateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter()
        {
            UpdateHUD();
            SpawnEnemies();

            _gameStateMachine.Enter<PlayerRoundState>();
        }

        public void Exit()
        {
            
        }
       
        private void UpdateHUD()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            LevelData levelData = _projectContainer.Resolve<LevelData>();

            levelData.CurrentRoom += 1;
            hud.SetRoomText(levelData.CurrentRoom);
        }

        private void SpawnEnemies()
        {
            ICharacterSpawner characterSpawner = _projectContainer.Resolve<ICharacterSpawner>();
            Vector3 enemyPosition = _projectContainer.Resolve<Vector3>("EnemyPosition");
            Vector3 delta = _projectContainer.Resolve<Vector3>("DeltaEnemySpawnOffset");

            characterSpawner.SpawnCharacterFromStaticData("Enemy1", enemyPosition);
            characterSpawner.SpawnCharacterFromStaticData("Enemy2", enemyPosition + delta);
        }
    }
}
