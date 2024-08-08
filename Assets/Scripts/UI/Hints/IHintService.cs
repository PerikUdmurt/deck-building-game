using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace YGameTemplate.UI.Hints
{
    public interface IHintService
    {
        Task CreateHint(string name, string hintText, Color color);
        UniTask CreateObjectPool();
        void DeleteHint();
    }
}