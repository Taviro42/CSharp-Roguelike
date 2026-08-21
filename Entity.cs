namespace Roguelike;

public class Entity
{
    public (int, int) xy;
    public char icon;

    public Entity((int, int) position_xy, char icon)
    {
        xy = position_xy;
        this.icon = icon;
    }
}