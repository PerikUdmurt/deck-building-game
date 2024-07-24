using CardBuildingGame.Infrastructure.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.ObjectPool
{
    public class ObjectPool<T> where T: MonoBehaviour, IPooledObject
    {
        private Factory<T> _factory;
        private List<T> _objects = new List<T>();

        public ObjectPool(string assetPath)
        {
            _factory = new Factory<T>(assetPath);
        }

        public ObjectPool(string assetPath, int prepareObjects)
        {
            _factory = new Factory<T>(assetPath);
            Fill(prepareObjects);
        }

        public void Fill(int prepareObjects)
        {
            for (int i = 0; i < prepareObjects; i++)
            {
                Create();
            }
        }

        public void CleanUp()
        {
            foreach (var obj in _objects)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => x.gameObject.activeSelf == false);

            if (obj == null)
            {
                obj = Create();
            }
            obj.OnReceipt();
            return obj;
        }

        private T Create()
        {
            T obj = _factory.Create();
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
