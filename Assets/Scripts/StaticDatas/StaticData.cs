using UnityEngine;

namespace CardBuildingGame.StaticDatas
{
    public abstract class StaticData: ScriptableObject
    {
        public abstract string StaticDataID { get; }
    }
}