using System.Drawing;

public class Player : GameObject
{
    private Image _playerImage;
    private Point _initialPosition;

    public Player(Point position, Size size) : base(position, size)
    {
        _playerImage = Image.FromFile("page\\playerIcon.png");
        _initialPosition = position;
    }

    public override void Update(float dt)
    {
        Position = new Point(Position.X + 7, Position.Y); 
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(_playerImage, Position.X, Position.Y, Size.Width, Size.Height);
    }

    public void Reset()
    {
        Position = _initialPosition;
    }
}
