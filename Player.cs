// using System.Drawing;
using Raylib_cs;

public class Player : GameObject
{
    private Color _color;
    // private int _pointValue;
    // private int _lives;
    // private int _speedX;
    // private int _speedY;



    public Player(int x, int y) : base(x, y)
    {
        _color = Color.DarkBlue;
        _speedX = 5;
        _speedY = 5;
        _width = 50;
        _height = 10;
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, _color);
    }

    public void XAxisChange()
    {
        _x += _speedX;

        if (_x < 0)
        {
            _x = 0;
        }
        else if (_x < 0)
        {
            _x = 0;
            _speedX = 0;
        }
        else if (_x > GameManager.SCREEN_WIDTH - 50)
        {
            _x = GameManager.SCREEN_WIDTH - 50;
        }
    }

    // public void HandleInput()
    // {

    // }
    public void keyLeft()
    {
        _speedX = -Math.Abs(_speedX);
        XAxisChange();
    }
    public void keyRight()
    {
        _speedX = Math.Abs(_speedX);
        XAxisChange();
    }


    public override void ProcessActions()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            keyLeft();
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            keyRight();
        }
    }

    // CollideWith
    // public override void CollideWith(GameObject first)
    // {
    //     if (first = Treasure)
    //     {
    //         _pointValue += Treasure._value;
    //     }
    // }

}