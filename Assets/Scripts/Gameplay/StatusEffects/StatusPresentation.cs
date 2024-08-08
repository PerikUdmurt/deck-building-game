using CardBuildingGame.Infrastructure;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.ObjectPool;

namespace CardBuildingGame.Gameplay.Statuses
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StatusPresentation : MonoBehaviour, IPooledObject
    {
        [SerializeField] private TextMeshProUGUI _statusText;

        private string _description = "";
        private SpriteRenderer _spriteRenderer;

        public Sprite Sprite 
        { 
            get => _spriteRenderer.sprite ?? GetComponent<SpriteRenderer>().sprite;
            private set => _spriteRenderer.sprite = value;
        }

        public void Init(Sprite sprite, int statusText, string description)
        {
            _statusText.text = statusText.ToString();
            _description = description;
            Sprite = sprite;
        }

        public void OnCreated()
        {
            gameObject.SetActive(false);
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
        }

        public void OnReleased()
        {
            gameObject.SetActive(false);
        }
    }

    public class StatusPresentationHolder: MonoBehaviour
    {
        private StatusHolder _statusHolder;
        private List<StatusPresentation> _presentations;
        private ObjectPool<StatusPresentation> _pool;
        private IAssetProvider _assetProvider;

        public void Init(StatusHolder statusHolder, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _pool = new(assetProvider, BundlePath.StatusPresentation);
            _presentations = new List<StatusPresentation>();
            _statusHolder = statusHolder;
            statusHolder.Changed += UpdatePresentation;
        }

        private async void UpdatePresentation()
        { 
            _presentations.Clear();
            foreach (var status in _statusHolder.Statuses) 
            {
                _presentations.Add(await CreateStatusPresentation(status));
            }
        }

        private async UniTask<StatusPresentation> CreateStatusPresentation(KeyValuePair<Status, int> pair)
        {
            StatusPresentation status = await _pool.Get();
            Sprite icon = await _assetProvider.Load<Sprite>(pair.Key.statusType.ToString() + "Icon");
            status.Init(icon, pair.Value, StatusDescription.Descriptions[pair.Key.statusType]);
            return status;
        }
    }

    public static class StatusDescription
    {
        public static Dictionary<StatusType, string> Descriptions = new()
        {
            { StatusType.Bleeding, "" },
            {StatusType.Poisoning, "" },
            {StatusType.DoubleDamage, "" },
            {StatusType.AttackBonus, "" },
            {StatusType.Armor, "" },
            {StatusType.Health, "" }
        };
    }
}