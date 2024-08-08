using CardBuildingGame.Gameplay.Cards;
using System.Collections.Generic;


namespace CardBuildingGame.Datas
{
    public class LevelInfo
    {
        public int CurrentRoom;
        public int MaxRoom;
        public List<ICardTarget> Characters;

        public LevelInfo(int currentRoom, int maxRoom, List<ICardTarget> characters)
        {
            CurrentRoom = currentRoom;
            MaxRoom = maxRoom;
            Characters = characters;
        }
    }
}