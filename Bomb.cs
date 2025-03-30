using System.Security.Cryptography;
using Raylib_cs;

public class Bomb : GameObject
{
    private Color _color;
    // private int _randomNumber;
    private GameManager _gameManager;
    // private int _lives;



    public Bomb(int x, int y, GameManager gameManager) : base(x, y)
    {
        _color = Color.Red;
        _gameManager = gameManager;
    }

    public override void Draw()
    {
        Raylib.DrawCircle(_x, _y, 5, _color);
    }

    public void YAxisChange()
    {
        _y += 5;
        _speedX = Math.Abs(_speedX);
    }

    public override void ProcessActions()
    {
        YAxisChange();
    }

    public override void CollideWith(GameObject other)
    {
        if (other is Player)
        {
            _gameManager.DecreaseLives();
            _gameManager.DecreaseSpawnInterval();
        }

    }

}