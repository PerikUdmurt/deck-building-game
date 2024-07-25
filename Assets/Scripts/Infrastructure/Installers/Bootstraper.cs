using CardBuildingGame.Infrastructure.StateMachine;
using CardBuildingGame.Services.DI;
using CardBuildingGame.StaticDatas;
using UnityEngine;

namespace CardBuildingGame
{

    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private DeckStaticData _initialDeckStaticData;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private Transform _enemyPosition;
        [SerializeField] private Transform _cardHolderPosition;
        [SerializeField] private Vector3 _deltaCardOffset;
        [SerializeField] private Vector3 _deltaEnemySpawnOffset;

        private GameStateMachine _gameStateMachine;
        private DiContainer _projectDiContainer;

        public void Awake()
        {
            _projectDiContainer = new DiContainer();
            RegisterInitConfigs();
            _gameStateMachine = new(_projectDiContainer);
            _gameStateMachine.Enter<BootstrapState>();
        }

        private void RegisterInitConfigs()
        {
            _projectDiContainer.RegisterInstance(_heroPosition.position, tag: "HeroPosition");
            _projectDiContainer.RegisterInstance(_enemyPosition.position, tag: "EnemyPosition");
            _projectDiContainer.RegisterInstance(_cardHolderPosition.position, tag: "CardHolderPosition");
            _projectDiContainer.RegisterInstance(_initialDeckStaticData, tag: "InitialDeckStaticData");
            _projectDiContainer.RegisterInstance(_deltaCardOffset, tag: "DeltaCardOffset");
            _projectDiContainer.RegisterInstance(_deltaEnemySpawnOffset, tag: "DeltaEnemySpawnOffset");
        }
    }
}