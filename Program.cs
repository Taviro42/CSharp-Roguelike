using System;

namespace Roguelike
{
    class Program()
    {
        public static Map map = new Map();

        static int Main()
        {
            (int, int) map_xy = map.getXY();
            string[] current_map;

            // Warn if window size is to small
            if (map_xy.Item1 > Console.WindowWidth || map_xy.Item2 > Console.WindowWidth)
            {
                Console.WriteLine("Your terminal window size is too small to show the loaded map. Please resize your window!");
                return 1;
            }

            // Print out Map
            current_map = map.getMap();
            foreach (var line in current_map)
            {
                Console.WriteLine(line);
            }

            return 0;
        }
    }
}
