using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Services
{
    public class StaticDataService: IStaticDataService
    {
        private const string StaticDataPath = "StaticDatas";
        private Dictionary<string, StaticData> _staticDatas;

        public void LoadStaticDatas()
        {
            _staticDatas = Resources.LoadAll<StaticData>(StaticDataPath)
                .ToDictionary(x => x.StaticDataID, x => x);
        }

        public StaticData GetStaticData(string staticDataName, out StaticData itemStaticData)
        {
            StaticData data = _staticDatas.TryGetValue(staticDataName, out StaticData value) ? value : null;
            itemStaticData = data;
            return itemStaticData;
        }
    }
}

