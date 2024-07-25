using System.Collections;
using UnityEngine;

namespace CardBuildingGame.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}