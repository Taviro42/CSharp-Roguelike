using System.Text;

namespace Roguelike;

public class Map
{
    private readonly string _lobbyMapPath = @"/home/timon/_/work/coding/CSharp-Roguelike/map_lobby.txt";

    private (int, int)? xy;
    private string[] _rows;

    private void LoadMapFile(string path)
    {
        if (Path.Exists(path))
        {
            _rows = File.ReadAllLines(path);
            xy = (_rows[0].Length, _rows.Length);
        }
        else
        {
            Console.WriteLine("Path to map does not exist!");
        }
    }

    public void MoveEntity((int, int) from, (int, int) to, char glyph)
    {
        const char floorGlyph = '.';

        if (!xy.HasValue)
        {
            LoadMapFile(_lobbyMapPath);
        }

        // check if position is valid
        if (from.Item1 > xy.Value.Item1 || from.Item2 > xy.Value.Item2 || to.Item1 > xy.Value.Item1 || to.Item2 > xy.Value.Item2)
        {
            Console.WriteLine("Entity position is out of range!");
        }
        else
        {
            // TODO: check for obstacles

            StringBuilder fromRow = new StringBuilder(_rows[to.Item2]);
            fromRow[to.Item1] = floorGlyph;
            _rows[to.Item2] = fromRow.ToString();

            StringBuilder toRow = new StringBuilder(_rows[from.Item2]);
            toRow[from.Item1] = glyph;
            _rows[from.Item2] = toRow.ToString();
        }
    }

    public string[] getMap()
    {
        if (_rows == null)
        {
            LoadMapFile(_lobbyMapPath);
        }

        return _rows;
    }

    public (int, int) getXY()
    {
        if (!xy.HasValue)
        {
            LoadMapFile(_lobbyMapPath);
        }

        return xy.Value;
    }
}