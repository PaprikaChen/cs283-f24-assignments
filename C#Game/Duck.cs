using System.Drawing;

public class Duck : GameObject
{
    private Image _duckImage;
    private Point _initialPosition;

    public Duck(Point position, Size size) : base(position, size)
    {
        _duckImage = Image.FromFile("page\\duckIcon.png");
        _initialPosition = position;
    }

    public override void Update(float dt)
    {
        Position = new Point(Position.X + 3, Position.Y);
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(_duckImage, Position.X, Position.Y, Size.Width, Size.Height);
    }

    public void Reset()
    {
        Position = _initialPosition;
    }
}
