using CardBuildingGame.Services.DI;
using UnityEngine;

namespace CardBuildingGame
{
    public class SceneInstaller: MonoBehaviour
    {
        [SerializeField] private int _maxRoom;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private Transform _enemyPosition;
        [SerializeField] private Transform _cardHolderPosition;
        [SerializeField] private Vector3 _deltaCardOffset;
        [SerializeField] private Vector3 _deltaEnemySpawnOffset;
        private DiContainer _sceneContainer;

        public DiContainer GetSceneInstaller(DiContainer projectContainer) 
        { 
            _sceneContainer = new DiContainer(projectContainer);
            RegisterInitConfigs();
            return _sceneContainer;
        }

        private void RegisterInitConfigs()
        {
            _sceneContainer.RegisterInstance(_maxRoom, tag: "MaxRoom");
            _sceneContainer.RegisterInstance(_heroPosition.position, tag: "HeroPosition");
            _sceneContainer.RegisterInstance(_enemyPosition.position, tag: "EnemyPosition");
            _sceneContainer.RegisterInstance(_cardHolderPosition.position, tag: "CardHolderPosition");
            _sceneContainer.RegisterInstance(_deltaCardOffset, tag: "DeltaCardOffset");
            _sceneContainer.RegisterInstance(_deltaEnemySpawnOffset, tag: "DeltaEnemySpawnOffset");
        }
    }
}