using CardBuildingGame.Datas;
using CardBuildingGame.Services.DI;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class EnemyRoundState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DiContainer _projectContainer;
        
        public EnemyRoundState(GameStateMachine stateMachine, DiContainer projectContainer) 
        {
            _stateMachine = stateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(true);

            if (CheckRoomFinished())
                CheckGameFinished();

            _stateMachine.Enter<PlayerRoundState>();
        }

        private void CheckGameFinished()
        {
            LevelData levelData = _projectContainer.Resolve<LevelData>();
            if (levelData.CurrentRoom == 3)
                _stateMachine.Enter<BootstrapState>();

            _stateMachine.Enter<NewRoomState>();
        }

        public void Exit()
        {
            HUDController hud = _projectContainer.Resolve<HUDController>();
            hud.SetButtonInteractable(true);
        }

        private bool CheckRoomFinished()
        {
            LevelData levelData = _projectContainer.Resolve<LevelData>();

            var enemies = from enemy in levelData.Characters
                          where enemy.TargetLayer.value == 7
                          select enemy;

            if (enemies.Count() == 0)
                return true;

            return false;
        }
    }
}
