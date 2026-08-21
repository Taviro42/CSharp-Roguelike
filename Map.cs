namespace Roguelike;

public class Map
{
    private string map_lobby_path = @"/home/timon/_/work/coding/CSharp-Roguelike/map_lobby.txt";

    private (int, int)? xy;
    private string[] map;

    private void loadMapFile(string path)
    {
        if (Path.Exists(path))
        {
            map = File.ReadAllLines(path);
            xy = (map[0].Length, map.Length);
        }
        else
        {
            Console.WriteLine("Path to map does not exist!");
        }
    }

    public string[] getMap()
    {
        if (map == null)
        {
            loadMapFile(map_lobby_path);
        }

        return map;
    }

    public (int, int) getXY()
    {
        if (!xy.HasValue)
        {
            loadMapFile(map_lobby_path);
        }

        return xy.Value;
    }
}