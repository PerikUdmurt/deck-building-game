using CardBuildingGame.Gameplay.Cards;
using System.Collections.Generic;
using UnityEngine;


namespace CardBuildingGame.Datas
{
    public class LevelData
    {
        public int CurrentRoom;
        public List<ICardTarget> Characters;

        public LevelData(int currentRoom, List<ICardTarget> characters)
        {
            CurrentRoom = currentRoom;
            Characters = characters;
        }
    }
}