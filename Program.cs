using Roguelike;

namespace CSharp_Roguelike
{
    class Program()
    {
        private static Map _map = new Map();

        static int Main()
        {
            // Warn if window size is too small
            if (_map.Width > Console.WindowWidth || _map.Height > Console.WindowHeight)
            {
                Console.WriteLine("Your terminal window size is too small to show the loaded map. Please resize your window!");
                return 1;
            }

            (int X, int Y) startPosition = (31, 17);
            Entity player = new Entity(startPosition, '&');

            _map.MoveEntity(startPosition, startPosition, player.Glyph);
            Render();

            (int X, int Y) target = startPosition;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        target.X -= 1;
                        break;

                    case ConsoleKey.UpArrow:
                        target.Y -= 1;
                        break;

                    case ConsoleKey.DownArrow:
                        target.Y += 1;
                        break;

                    case ConsoleKey.RightArrow:
                        target.X += 1;
                        break;
                }

                _map.MoveEntity(player.Position, target, player.Glyph);
                player.Position = target;
                Console.SetCursorPosition(0, 0);
                Render();
            }
        }

        private static void Render()
        {
            foreach (string line in _map.Rows)
            {
                Console.WriteLine(line);
            }
        }
    }
}
