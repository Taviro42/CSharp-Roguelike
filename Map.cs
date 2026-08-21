namespace Roguelike;

public class Map
{
    public (int, int) xy;
    public string[] map;

    public void loadMapFile(string path)
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
}