/* 
 * GameObject class.
 * Author: Paprika Chen
 * Date: 2024/9/11
 * 
 * Represents a base class for all objects in the game, providing position and size properties.
 * Can be extended by other game objects like Player and Duck.
 */

using System.Drawing;

public class GameObject
{
    public Point Position { get; set; }  // Position of the game object
    public Size Size { get; set; }  // Size of the game object

    /* 
     * Constructor for GameObject.
     * Initializes the position and size of the object.
     */
    public GameObject(Point position, Size size)
    {
        Position = position;
        Size = size;
    }

    /* 
     * Update method.
     * Virtual method to be overridden to update the object's state.
     * 
     * Parameters:
     * - dt: The delta time
     */
    public virtual void Update(float dt)
    {
    }
}

