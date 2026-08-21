using System;

namespace Roguelike
{
    class Program()
    {
        public static Map map = new Map();

        static int Main()
        {
            string map_lobby_path = @"/home/timon/_/work/coding/CSharp-Roguelike/map_lobby.txt";
            map.loadMapFile(map_lobby_path);

            if (map.xy.Item1 > Console.WindowWidth || map.xy.Item2 > Console.WindowWidth)
            {
                Console.WriteLine("Your terminal window size is too small to show the loaded map. Please resize your window!");
                return 1;
            }

            foreach (var line in map.map)
            {
                Console.WriteLine(line);
            }

            return 0;
        }
    }
}
