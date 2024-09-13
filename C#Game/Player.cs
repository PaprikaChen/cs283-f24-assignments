/* 
 * Player.cs
 * Author: Paprika Chen
 * Date: 2024/9/11
 * 
 * Represents the Player object in the game.
 * The Player moves based on user input and chases the Duck.
 */

using System.Drawing;

public class Player : GameObject
{
    private Image _playerImage;
    private Point _initialPosition;

    /* 
     * Player constructor.
     * Initializes the player's position, size, and loads the player's image.
     * 
     * Parameters:
     * - position: The initial position of the player.
     * - size: The size of the player.
     */
    public Player(Point position, Size size) : base(position, size)
    {
        _playerImage = Image.FromFile("page\\playerIcon.png");
        _initialPosition = position;
    }

    /* 
     * Update method.
     * Updates the player's position by moving it to the right.
     * 
     * Parameters:
     * - dt: The delta time (not used)
     */
    public override void Update(float dt)
    {
        Position = new Point(Position.X + 7, Position.Y); 
    }

    /* 
     * Draw method.
     * Draws the player at its current position on the screen.
     * 
     * Parameters:
     * - g: The Graphics object used to draw the player.
     */
    public void Draw(Graphics g)
    {
        g.DrawImage(_playerImage, Position.X, Position.Y, Size.Width, Size.Height);
    }

    /* 
     * Reset method.
     * Resets the player's position to its initial starting point.
     */
    public void Reset()
    {
        Position = _initialPosition;
    }
}
