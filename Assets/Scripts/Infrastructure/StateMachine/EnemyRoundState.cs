using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Services.DI;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class EnemyRoundState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly RoundStateMachine _roundStateMachine;
        private readonly DiContainer _projectContainer;
        
        public EnemyRoundState(GameStateMachine gameStateMachine, RoundStateMachine stateMachine, DiContainer projectContainer) 
        {
            _gameStateMachine = gameStateMachine;
            _roundStateMachine = stateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(false);

            DoEnemyActions();

            if (CheckRoomFinished())
                CheckGameFinished();

            

            _roundStateMachine.Enter<PlayerRoundState>();
        }

        public void Exit()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(true);
        }

        private void CheckGameFinished()
        {
            LevelData levelData = _projectContainer.Resolve<LevelData>();
            if (levelData.CurrentRoom == 3)
                _gameStateMachine.Enter<GameLoopState>();

            _roundStateMachine.Enter<NewRoomState>();
        }

        private void DoEnemyActions()
        {
            LevelData levelData = _projectContainer.Resolve<LevelData>();

            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer == TargetLayer.Enemy
                          select enemy;

            foreach (var enemy in enemies)
            {
                Debug.Log(enemy.CardPlayer.PreparedCard);
                enemy.CardPlayer.PlayPreparedCard(levelData.Characters);
            }
        }


        private bool CheckRoomFinished()
        {
            LevelData levelData = _projectContainer.Resolve<LevelData>();

            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer == TargetLayer.Enemy
                          select enemy;

            if (enemies.Count() == 0)
                return true;

            return false;
        }
    }
}
