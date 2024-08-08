using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Statuses;
using CardBuildingGame.Services.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class EnemyRoundState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly RoundStateMachine _roundStateMachine;
        private readonly DiContainer _sceneContainer;
        
        public EnemyRoundState(GameStateMachine gameStateMachine, RoundStateMachine stateMachine, DiContainer sceneContainer) 
        {
            _gameStateMachine = gameStateMachine;
            _roundStateMachine = stateMachine;
            _sceneContainer = sceneContainer;
        }

        public void Enter()
        {
            HUDController hud = _sceneContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(false);

            if (CheckGameFinished())
                return;

            DoEnemyActions();

            _roundStateMachine.Enter<PlayerRoundState>();
        }



        public void Exit()
        {
            HUDController hud = _sceneContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(true);
        }

        private bool CheckGameFinished()
        {
            HUDController hud = _sceneContainer.Resolve<HUDController>();
            LevelInfo levelData = _sceneContainer.Resolve<LevelInfo>();

            if (CheckRoomFinished())
            {
                if (levelData.CurrentRoom == levelData.MaxRoom)
                {
                    hud.OnVictory();
                    return true;
                }

                _roundStateMachine.Enter<NewRoomState>();
            }
            return false;
        }

        private void DoEnemyActions()
        {
            LevelInfo levelData = _sceneContainer.Resolve<LevelInfo>();
            StatusPlayer statusPlayer = new();

            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer == TargetLayer.Enemy
                          select enemy;

            List<ICardTarget> enemiesList = enemies.ToList();

            for (int i = 0; i < enemiesList.Count(); i++)
            {
                statusPlayer.ExecuteAllStatuses(enemiesList[i]);
                enemiesList[i].CardPlayer.PlayPreparedCard(levelData.Characters);
            }
        }


        private bool CheckRoomFinished()
        {
            LevelInfo levelData = _sceneContainer.Resolve<LevelInfo>();

            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer == TargetLayer.Enemy
                          select enemy;

            if (enemies.Count() == 0)
                return true;

            return false;
        }
    }
}
