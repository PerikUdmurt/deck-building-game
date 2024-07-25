using CardBuildingGame.Gameplay.Cards;
using System.Collections.Generic;
using UnityEngine;


namespace CardBuildingGame.Datas
{
    public class LevelData
    {
        public int CurrentRoom;
        public int MaxRoom;
        public List<ICardTarget> Characters;

        public LevelData(int currentRoom, int maxRoom, List<ICardTarget> characters)
        {
            CurrentRoom = currentRoom;
            MaxRoom = maxRoom;
            Characters = characters;
        }
    }
}