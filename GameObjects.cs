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

}