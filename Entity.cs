using CSharp_Roguelike;

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

    /// <summary>
    ///     Takes User input and moves the Entity on the map
    /// </summary>
    /// <param name="keyInfo">Arrow keys supported</param>
    /// <returns>True if Succeeded</returns>
    public bool Move(ConsoleKeyInfo keyInfo)
    {
        (int X, int Y) targetPosition = Position;

        switch (keyInfo.Key) 
        {
            case ConsoleKey.LeftArrow:
                targetPosition.X -= 1;
                break;

            case ConsoleKey.UpArrow:
                targetPosition.Y -= 1;
                break;

            case ConsoleKey.DownArrow:
                targetPosition.Y += 1;
                break;

            case ConsoleKey.RightArrow:
                targetPosition.X += 1;
                break;

            default:
                return false;
        }

        char targetGlyph = Map.GetGlyphAt(targetPosition);
        if (targetGlyph != '.')
        {
            return false;
        }

        Map.MoveGlyph(Position, targetPosition);
        Position = targetPosition;
        return true;
    }
}