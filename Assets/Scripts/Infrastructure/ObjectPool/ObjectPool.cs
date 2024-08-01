using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGameTemplate.Infrastructure.Factory;
using YGameTemplate.Infrastructure.AssetProviders;
using Cysharp.Threading.Tasks;
using CardBuildingGame.Infrastructure;

namespace YGameTemplate.Infrastructure.ObjectPool
{
    public class ObjectPool<T> where T: MonoBehaviour, IPooledObject
    {
        private Factory<T> _factory;
        private List<T> _objects = new List<T>();

        public ObjectPool(IAssetProvider assetProvider, string bundlePath)
        {
            _factory = new Factory<T>(assetProvider, bundlePath);
        }


        public async UniTask Fill(int prepareObjects)
        {
            for (int i = 0; i < prepareObjects; i++)
            {
                await Create();
            }
        }

        public void CleanUp()
        {
            foreach (var obj in _objects)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public async UniTask<T> Get()
        {
            var obj = _objects.FirstOrDefault(x => x.gameObject.activeSelf == false);

            if (obj == null)
            {
                obj = await Create();
            }
            obj.OnReceipt();
            return obj;
        }

        private async UniTask<T> Create()
        {
            T obj = await _factory.Create();
            _objects.Add(obj);
            obj.OnCreated();
            return obj;
        }

        public void Release(T obj)
        {
            obj.OnReleased();
        }
    }
}
