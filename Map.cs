using System.Text;

namespace CSharp_Roguelike;

public class Map
{
    private readonly string _lobbyMapPath = @"/home/timon/_/work/coding/CSharp-Roguelike/map_lobby.txt";

    public string[] Rows { get; private set; }
    public int Width => Rows[0].Length;
    public int Height => Rows.Length;

    private const char FloorGlyph = '.';

    public Map()
    {
        LoadFromFile(_lobbyMapPath);
    }

    private void LoadFromFile(string path)
    {
        if (Path.Exists(path))
        {
            Rows = File.ReadAllLines(path);
        }
        else
        {
            Console.WriteLine("Path to map does not exist!");
        }
    }

    public void MoveEntity((int X, int Y) from, (int X, int Y) to, char glyph)
    {
        // check if position is valid
        if (from.X > Width || from.Y > Height || to.X > Width || to.Y > Height)
        {
            Console.WriteLine("Entity position is out of range!");
        }
        else
        {
            // TODO: check for obstacles

            StringBuilder fromRow = new StringBuilder(Rows[from.Y]);
            fromRow[from.X] = FloorGlyph;
            Rows[from.Y] = fromRow.ToString();

            StringBuilder toRow = new StringBuilder(Rows[to.Y]);
            toRow[to.X] = glyph;
            Rows[to.Y] = toRow.ToString();
        }
    }
}