using CardBuildingGame.Infrastructure;
using CardBuildingGame.Infrastructure.StateMachine;
using CardBuildingGame.Services.DI;
using UnityEngine;

namespace CardBuildingGame
{
    public class Bootstraper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;
        private DiContainer _projectDiContainer;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            _projectDiContainer = new DiContainer();
            RegisterCoroutineRunner();
            _gameStateMachine = new(_projectDiContainer);
            _gameStateMachine.Enter<BootstrapState>();
            
        }

        private void RegisterCoroutineRunner()
        {
            _projectDiContainer.RegisterInstance<ICoroutineRunner>(this);
        }
    }
}