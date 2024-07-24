using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class Factory<T>
    {
        private string _assetPath;

        public Factory(string assetPath)
        {
            _assetPath = assetPath;
        }

        public T Create()
        {
            GameObject resource = Resources.Load<GameObject>(_assetPath);
            GameObject obj = GameObject.Instantiate(resource, new Vector3(0,0,0),Quaternion.identity);
            obj.TryGetComponent<T>(out  var result);
            return result;
        }
    }
}
    
