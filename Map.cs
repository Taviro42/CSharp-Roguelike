using System.Text;

namespace CSharp_Roguelike;

public static class Map
{
    private static readonly string _lobbyMapPath = @"/home/timon/_/work/coding/CSharp-Roguelike/map_lobby.txt";
    public static string[] Rows { get; private set; } = LoadFromFile(_lobbyMapPath);
    public static int Width => Rows[0].Length;
    public static int Height => Rows.Length;

    private const char FloorGlyph = '.';

    private static string[] LoadFromFile(string path)
    {
        if (Path.Exists(path))
        {
            return File.ReadAllLines(path);
        }
        else
        {
            throw new Exception("Path to map does not exist!");
        }
    }

    /// <summary>
    ///     returns the Glyph at a specific position on the map
    /// </summary>
    public static char GetGlyphAt((int X, int Y) coordinate)
    {
        return Rows[coordinate.Y][coordinate.X];
    }

    public static void SetGlyphAt((int X, int Y) coordinate, char glyph)
    {
        StringBuilder stringBuilder = new StringBuilder(Rows[coordinate.Y]);
        stringBuilder[coordinate.X] = glyph;
        Rows[coordinate.Y] = stringBuilder.ToString();
    }

    /// <summary>
    ///     Checks if target position is valid and rebuilds the map with the entity glyph at the new position.
    /// </summary>
    /// <param name="from">Current position of entity</param>
    /// <param name="to">Target position the entity shall be moved to</param>
    /// <returns>0 if ran successfully and 1 if the target position is not valid</returns>
    public static void MoveGlyph((int X, int Y) from, (int X, int Y) to)
    {
        // check if position is valid
        if (from.X > Width || from.Y > Height || to.X > Width || to.Y > Height)
        {
            throw new Exception("Entity position is out of range!");
        }

        char originGlyph = GetGlyphAt(from);
        char targetGlyph = GetGlyphAt(to);

        StringBuilder fromRow = new StringBuilder(Rows[from.Y]);
        fromRow[from.X] = targetGlyph;
        Rows[from.Y] = fromRow.ToString();

        StringBuilder toRow = new StringBuilder(Rows[to.Y]);
        toRow[to.X] = originGlyph;
        Rows[to.Y] = toRow.ToString();
    }
}