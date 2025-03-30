// using System.Drawing;
using Raylib_cs;

public class Treasure : GameObject
{
    Random random = new Random();
    public int _value;
    private int _size;
    private GameManager _gameManager;

    public Treasure(int x, int y, GameManager gameManager) : base(x, y)
    {
        _speedY = 5;
        _width = 15;
        _height = 15;
        _gameManager = gameManager;
        _value = CalculateValue();
    }

    public int RandomSize()
    {
        _size = random.Next(5, 30);
        return _size;
    }

    public int CalculateValue()
    {
        _value = RandomSize() / 3;
        return _value;
    }
    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, Color.Gold);
    }

    public void YAxisChange()
    {
        _y += CalculateValue();
        _speedX = Math.Abs(_speedX);
    }

    public override void ProcessActions()
    {
        YAxisChange();
    }

    public override void CollideWith(GameObject other)
    {
        Console.WriteLine($"Treasure's CollideWith() called. Other object type: {other.GetType().Name}");
        if (other is Player)
        {
            _gameManager.IncrementScore(_value);
            _gameManager.IncreaseSpawnInterval();

        }
    }
}