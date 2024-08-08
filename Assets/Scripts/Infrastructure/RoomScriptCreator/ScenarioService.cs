using CardBuildingGame.Services;
using System.Collections.Generic;

namespace CardBuildingGame.Infrastructure.GameScenario
{
    public class ScenarioService
    {
        private readonly RoomScriptsCreator _roomScriptsCreator;
        public Dictionary<int, List<RoomStaticData>> _rooms;

        public ScenarioService(GameMode gameMode ,IStaticDataService staticDataService) 
        {
            _roomScriptsCreator = new RoomScriptsCreator(staticDataService);
            _rooms = CreateRoomScripts(gameMode);
        }

        public List<RoomStaticData> GetRoomScripts(int floor)
        {
            if (_rooms.ContainsKey(floor))
                return _rooms[floor];

            return null;
        }

        public void AddRoom(int floor, RoomStaticData room)
            => _rooms.AddRoom(floor, room);

        private Dictionary<int, List<RoomStaticData>> CreateRoomScripts(GameMode gameMode)
            => _roomScriptsCreator.CreateGameGraph(gameMode);
    }
}