namespace Roguelike;

public class Entity
{
    public (int X, int Y) Position;
    public readonly char Glyph;

    public Entity((int X, int Y) position, char glyph)
    {
        Position = position;
        Glyph = glyph;
    }
}