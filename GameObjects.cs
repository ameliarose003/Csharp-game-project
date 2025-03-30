public abstract class GameObject
{
    protected int _y;
    protected int _x;
    protected int _width;
    protected int _height;
    protected int _speedX;
    protected int _speedY;

    public GameObject(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public abstract void Draw();

    public virtual void ProcessActions()
    {

    }

    public virtual void CollideWith(GameObject first)
    {

    }
    // public bool IsCollision(GameObject first, GameObject second)
    // {
    //     if (first.GetLeftEdge() <= second.GetRightEdge() && first.GetRightEdge() >= second.GetLeftEdge()
    //     && first.GetBottomEdge() >= second.GetTopEdge() && first.GetTopEdge() <= second.GetBottomEdge())
    //     {
    //         return true;
    //     }
    //     else return false;
    // }

    public virtual int GetLeftEdge()
    {
        return _x;
    }
    public virtual int GetRightEdge()
    {
        return _x + _width;
    }
    public virtual int GetBottomEdge()
    {
        return _y + _height;
    }
    public virtual int GetTopEdge()
    {
        return _y;
    }

    // save .wav file in project and in game manager Raylib.InitAudioDevice() and close it at the end. Raylib.PlaySound(AssetManaager.GetSound("treasure"))
    // Add sound into CollideWith() 
    // Spawning and death of objects
    // COllide based on not the centers, but the actual object. 
    // Can you find the x coordinate of the left/rigth edge of an object?

    // x and y in a rectangle draw it from the corner Raylib.DrawRectangle()
}