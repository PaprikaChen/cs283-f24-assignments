using System.Drawing;

public class GameObject
{
    public Point Position { get; set; }  
    public Size Size { get; set; } 

    public GameObject(Point position, Size size)
    {
        Position = position;
        Size = size;
    }

    
    public virtual void Update(float dt)
    {
    }
}
