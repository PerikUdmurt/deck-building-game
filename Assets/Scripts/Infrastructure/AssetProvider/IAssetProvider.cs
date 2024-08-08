using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace YGameTemplate.Infrastructure.AssetProviders
{
    public interface IAssetProvider
    {
        void CleanUp();
        void Initialize();
        UniTask<T> Load<T>(AssetReference assetReference) where T : class;
        UniTask<T> Load<T>(string address) where T : class;
    }
}