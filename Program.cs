using System;

namespace Roguelike
{
    class Program()
    {
        public static Map map = new Map();

        static int Main()
        {
            (int, int) map_xy = map.getXY();

            // Warn if window size is to small
            if (map_xy.Item1 > Console.WindowWidth || map_xy.Item2 > Console.WindowWidth)
            {
                Console.WriteLine("Your terminal window size is too small to show the loaded map. Please resize your window!");
                return 1;
            }

            (int, int) default_player_xy = (31, 17);
            Entity player = new Entity(default_player_xy, '&');
            (int, int) current_player_xy = default_player_xy;

            map.MoveEntity(current_player_xy, current_player_xy, player.icon);
            printCurrentMap(map);

            (int, int) xy_new = default_player_xy;

            while (true)
            {
                ConsoleKeyInfo ki = Console.ReadKey();

                switch (ki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        xy_new.Item1 -= 1;
                        break;

                    case ConsoleKey.UpArrow:
                        xy_new.Item2 -= 1;
                        break;

                    case ConsoleKey.DownArrow:
                        xy_new.Item2 += 1;
                        break;

                    case ConsoleKey.RightArrow:
                        xy_new.Item1 += 1;
                        break;
                }

                map.MoveEntity(current_player_xy, xy_new, player.icon);
                Console.SetCursorPosition(0, 0);
                printCurrentMap(map);


            }

            return 0;
        }

        static void printCurrentMap(Map map)
        {
            string[] current_map = map.getMap();
            foreach (var line in current_map)
            {
                Console.WriteLine(line);
            }
        }
    }
}
