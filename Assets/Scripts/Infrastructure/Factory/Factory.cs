using Cysharp.Threading.Tasks;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;

namespace YGameTemplate.Infrastructure.Factory
{
    public class Factory<T> where T : MonoBehaviour
    {
        private readonly string _bundlePath;
        private IAssetProvider _assetProvider;
        private AbstractFactory<T> _abstractFactory;

        public Factory(IAssetProvider assetProvider, string bundlePath)
        {
            _abstractFactory = new AbstractFactory<T>(assetProvider);
            _bundlePath = bundlePath;
        }

        public async UniTask<T> Create() => await _abstractFactory.Create(_bundlePath);
    }
}
    
