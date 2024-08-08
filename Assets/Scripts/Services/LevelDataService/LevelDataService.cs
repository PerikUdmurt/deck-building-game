using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YGameTemplate.Services.LevelData;

namespace YGameTemplate
{
    public class LevelDataService : ILevelDataService
    {
        public LevelDataService() 
        { 
            CurrentLevelData = new LevelData();
        }

        public LevelData CurrentLevelData { get; private set; }
    }
}