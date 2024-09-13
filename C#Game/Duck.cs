/* 
 * Duck.cs
 * Author: Paprika Chen
 * Date: 2024/9/11
 * 
 * Represents the Duck object in the game.
 * The Duck moves from left to right and is chased by the Player.
 * This class update the position of the Duck object.
 */

using System.Drawing;

public class Duck : GameObject
{
    private Image _duckImage;
    private Point _initialPosition;

    /* 
     * Duck constructor.
     * Initializes the duck's position, size, and loads the duck's image.
     */
    public Duck(Point position, Size size) : base(position, size)
    {
        _duckImage = Image.FromFile("page\\duckIcon.png");
        _initialPosition = position;
    }

    /* 
     * Update method.
     * Updates the duck's position by moving it to the right.
     * 
     * Parameters:
     * - dt: The delta time (not used)
     */
    public override void Update(float dt)
    {
        Position = new Point(Position.X + 3, Position.Y);
    }

    /* 
     * Draw method.
     * Draws the duck at its current position on the screen.
     * 
     * Parameters:
     * - g: The Graphics object used to draw the duck.
     */
    public void Draw(Graphics g)
    {
        g.DrawImage(_duckImage, Position.X, Position.Y, Size.Width, Size.Height);
    }

    /* 
     * Reset method.
     * Resets the duck's position to its initial starting point.
     */
    public void Reset()
    {
        Position = _initialPosition;
    }
}
