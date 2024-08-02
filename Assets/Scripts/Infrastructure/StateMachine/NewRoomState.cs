using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using YGameTempate.Services.SaveLoad;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class NewRoomState : IState
    {
        private readonly RoundStateMachine _roundStateMachine;
        private readonly DiContainer _sceneContainer;

        public NewRoomState(RoundStateMachine roundStateMachine, DiContainer sceneContainer) 
        {
            _roundStateMachine = roundStateMachine;
            _sceneContainer = sceneContainer;
        }

        public async void Enter()
        {
            UpdateHUD();
            await SpawnEnemies();
            SaveProgress();

            _roundStateMachine.Enter<PlayerRoundState>();
        }

        public void Exit()
        {
            
        }
       
        private void UpdateHUD()
        {
            HUDController hud = _sceneContainer.Resolve<HUDController>();
            LevelData levelData = _sceneContainer.Resolve<LevelData>();

            levelData.CurrentRoom += 1;
            hud.SetRoomText(levelData.CurrentRoom, levelData.MaxRoom);
        }

        private async UniTask SpawnEnemies()
        {
            ICharacterSpawner characterSpawner = _sceneContainer.Resolve<ICharacterSpawner>();
            Vector3 enemyPosition = _sceneContainer.Resolve<Vector3>("EnemyPosition");
            Vector3 delta = _sceneContainer.Resolve<Vector3>("DeltaEnemySpawnOffset");

            await characterSpawner.SpawnCharacterFromStaticData(Character.CharacterType.Enemy1, 1, enemyPosition);
            await characterSpawner.SpawnCharacterFromStaticData(Character.CharacterType.Enemy2, 1, enemyPosition + delta);
        }

        private void SaveProgress()
        {
            IDataPersistentService progressService = _sceneContainer.Resolve<IDataPersistentService>();
            progressService.SaveGame();
        }
    }
}
