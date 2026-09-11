using Roguelike;

namespace CSharp_Roguelike
{
    class Program()
    {
        static int Main()
        {
            // Warn if window size is too small
            if (Map.Width > Console.WindowWidth || Map.Height > Console.WindowHeight)
            {
                Console.WriteLine(
                    "Your terminal window size is too small to show the loaded map. Please resize your window!");
                return 1;
            }

            // Initialize player
            const char playerGlyph = '&';

            (int X, int Y) startPosition = (31, 17);
            Entity player = new Entity(startPosition, playerGlyph);

            // Render player
            Map.SetGlyphAt(startPosition, playerGlyph);
            Render();

            // User input loop
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                bool playerMoveSuccess = player.Move(key);

                if (playerMoveSuccess)
                {
                    Render();
                }
            }
        }

        private static void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();

            foreach (string line in Map.Rows)
            {
                Console.WriteLine(line);
            }
        }
    }
}
