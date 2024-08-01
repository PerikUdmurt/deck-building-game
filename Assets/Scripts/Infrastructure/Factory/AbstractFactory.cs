using Cysharp.Threading.Tasks;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;

namespace YGameTemplate.Infrastructure.Factory
{
    public class AbstractFactory<T> where T: MonoBehaviour 
    {
        private IAssetProvider _assetProvider;

        public AbstractFactory(IAssetProvider assetProvider)
            =>  _assetProvider = assetProvider;

        public async UniTask<T> Create(string bundlePath)
        {
            GameObject resource = await _assetProvider.Load<GameObject>(bundlePath);
            GameObject obj = GameObject.Instantiate(resource, new Vector3(0, 0, 0), Quaternion.identity);
            obj.TryGetComponent<T>(out var result);
            return result;
        }
    }
}
    
