using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.Factory;
using YGameTemplate.Infrastructure.ObjectPool;

namespace YGameTemplate.UI.Hints
{
    public class HintService: IHintService
    {
        private RectTransform _hintEntryPos;
        private List<HintUI> _hintList = new List<HintUI>();
        private ObjectPool<HintUI> _hintPool;

        private RectTransform _currentHintsEntryPoint;

        public HintService(RectTransform rectTransform,IAssetProvider assetProvider) 
        { 
            _hintPool = new ObjectPool<HintUI>(assetProvider, BundlePath.Hint);
            SetHintEntryPos(rectTransform);
        }

        public async UniTask CreateObjectPool()
        {
            await _hintPool.Fill(3);
        }

        public async Task CreateHint(string name, string hintText, Color color) 
        {
            HintUI currentHint = await _hintPool.Get();
            
            currentHint.transform.SetParent(_currentHintsEntryPoint.transform, false);
            _currentHintsEntryPoint = currentHint.rectTransform;

            currentHint.hintName.text = name;
            currentHint.description.text = hintText;

            _hintList.Add(currentHint);
        }

        public void DeleteHint()
        { 
            foreach (var hint in _hintList)
            {
                _hintPool.Release(hint);
            }
            _hintList.Clear();
            _currentHintsEntryPoint = _hintEntryPos;
        }

        private void SetHintEntryPos(RectTransform hintEntryPos)
        {
            _hintEntryPos = hintEntryPos;
            _currentHintsEntryPoint = _hintEntryPos;
        }
    }
}
