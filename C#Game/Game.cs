/* 
 * Game.cs
 * Author: Paprika Chen
 * Date: 2024/9/11
 * 
 * This is the main class for managing the game logic.
 * It controls the game states, updates the game objects (Player, Duck), and handles user input.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Game 
{
    // the collection of different states 
    public enum GameState 
    {
        CoverPage,
        IntroPage,
        TaskCutCake,
        TaskChaseDucky,
        ChaseSuccess, // the state when the player caught the duck
        ChaseFail, // the state when the player fail to catch the duck
        TaskTurnOnTV,
        SuccessPage
    }

    // the current status
    private GameState _currentState = GameState.CoverPage;

    // GameObjects
    private Player _player;
    private Duck _duck;

    // images
    private Image _coverPageImage;
    private Image _introPageImage;
    private Image _taskCakeImage;
    private Image _taskCakeImage2;
    private Image _taskDuckInfoImage;
    private Image _taskDuckBackgroundImage;
    private Image _taskDuckSuccess;
    private Image _taskDuckFail;
    private Image _taskTV;
    private Image _lastPage;

    //count
    private int _cakeClicks = 0;
    private bool _cakeSliced = false;
    private bool _chaseStart = false;
    private bool _chasePressD = false;

    // TV task vars
    private readonly Keys[] _correctSeq = { Keys.Left, Keys.Right, Keys.Up, Keys.Left, 
                                            Keys.Up, Keys.Right, Keys.Left, Keys.Left };
    private List<Keys> _userInputSeq = new List<Keys>(); // the list to store the user input sequence

    /* 
     * Setup method.
     * Initializes game variables and loads images for different game stages.
     * Resets cake click count and sliced status for the cake task.
     * Loads player and duck objects and sets their initial positions and sizes.
     * 
     */
    public void Setup() 
    {
        _cakeClicks = 0; // count how many times the cake is clicked during the cake task
        _cakeSliced = false; // record if the cake is sliced successfully or not

        _coverPageImage = Image.FromFile("page\\cover1.png");
        _introPageImage = Image.FromFile("page\\page2.png");
        _taskCakeImage = Image.FromFile("page\\caketask.png");
        _taskCakeImage2 = Image.FromFile("page\\caketask2.png");
        _taskDuckInfoImage = Image.FromFile("page\\duckinfo.png");
        _taskDuckBackgroundImage = Image.FromFile("page\\duckchaseBackground.png");
        _taskDuckSuccess = Image.FromFile("page\\ducksuccess.png");
        _taskDuckFail = Image.FromFile("page\\duckfail.png");
        _taskTV = Image.FromFile("page\\TVtask.png");
        _lastPage = Image.FromFile("page\\lastpage.png");

        _player = new Player(new Point(10, 200), new Size(100,100));
        _duck = new Duck(new Point(250, 200), new Size(100, 100));
    }

    /* 
     * Update method.
     * Updates the game state and the positions of the player and the duck. 
     * 
     * Parameters:
     * - dt: The delta time
     */
    public void Update(float dt) 
    {
        // during the duck chase part, update the positions of the player and the duck
        if (_chaseStart && _currentState == GameState.TaskChaseDucky)
        {
            if (_chasePressD)
            {
                _player.Update(dt);
                _chasePressD = false;
            }
            _duck.Update(dt);

            // if the player catches the duck
            if (_player.Position.X >= _duck.Position.X)
            {
                _currentState = GameState.ChaseSuccess;
            }
            else if ((_player.Position.X >= 1025 || _duck.Position.X >= 1025) // if the duck escapes 
                    && _currentState == GameState.TaskChaseDucky)
            {
                _currentState = GameState.ChaseFail;
            }
        }
    }

    /* 
     * Draw method.
     * Draw the appropriate game screen based on the current game state.
     * 
     * Parameters:
     * - g: The Graphics object used to draw images to the screen.
     */
    public void Draw(Graphics g) 
    {
        switch (_currentState)
        {
            case GameState.CoverPage:
                g.Clear(Color.White);
                g.DrawImage(_coverPageImage, 0, 0, 1025, 700);
                break;

            case GameState.IntroPage:
                g.Clear(Color.White);
                g.DrawImage(_introPageImage, 0, 0, 1025, 700);
                break;

            case GameState.TaskCutCake:
                g.Clear(Color.White);

                if (!_cakeSliced)
                {
                    g.DrawImage(_taskCakeImage, 0, 0, 1025, 700);
                }
                else g.DrawImage(_taskCakeImage2, 0, 0, 1025, 700);
                break;

            case GameState.TaskChaseDucky:
                g.Clear(Color.White);
                if (!_chaseStart)
                {
                    g.DrawImage(_taskDuckInfoImage, 0, 0, 1025, 700);
                }
                else
                {
                    g.DrawImage(_taskDuckBackgroundImage, 0, 0, 1025, 700);
                    _player.Draw(g);
                    _duck.Draw(g);
                }
                break;

            case GameState.ChaseSuccess:
                g.Clear(Color.White);
                g.DrawImage(_taskDuckSuccess, 0, 0, 1025, 700);
                break;

            case GameState.ChaseFail:
                g.Clear(Color.White);
                g.DrawImage(_taskDuckFail, 0, 0, 1025, 700);
                break;

            case GameState.TaskTurnOnTV:
                g.Clear(Color.White);
                g.DrawImage(_taskTV, 0, 0, 1025, 700);
                break;

            case GameState.SuccessPage:
                g.Clear(Color.White);
                g.DrawImage(_lastPage, 0, 0, 1025, 700);
                break;
        }

    }

    /* 
     * MouseClick method.
     * Handles mouse click events during different game tasks.
     * 
     * Parameters:
     * - mouse: The MouseEventArgs containing information about the mouse click.
     */
    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            if (_currentState == GameState.TaskCutCake && _cakeSliced == false)
            {
                _cakeClicks++;
                if (_cakeClicks >= 5)
                {
                    _cakeSliced = true;
                }
            }
        }
    }

    /* 
     * KeyDown method.
     * Handles keyboard input and updates the game state based on key presses.
     * 
     * Parameters:
     * - key: The KeyEventArgs containing information about the key press.
     */
    public void KeyDown(KeyEventArgs key)
    {
        if (_currentState == GameState.TaskTurnOnTV)
        {
            _userInputSeq.Add(key.KeyCode);
            CheckInputSeq();
            return;
        }
        if (key.KeyCode == Keys.Space)
        {
            if (_currentState == GameState.CoverPage)
            {
                _currentState = GameState.IntroPage;
            } else if (_currentState == GameState.IntroPage) 
            {
                _currentState = GameState.TaskCutCake;
            } else if (_currentState == GameState.TaskCutCake && _cakeSliced)
            {
                _currentState = GameState.TaskChaseDucky;
            } else if (_currentState == GameState.TaskChaseDucky)
            {
                _chaseStart = true;
            } else if (_currentState == GameState.ChaseFail)
            {
                _currentState = GameState.TaskChaseDucky;
                _chaseStart = false;
                // reset Player and the Duck
                _player.Reset();
                _duck.Reset();
            } else if (_currentState == GameState.ChaseSuccess)
            {
                _currentState = GameState.TaskTurnOnTV;
            }
        } else if (key.KeyCode == Keys.D)
        {
            _chasePressD = true;
        }
    }

    /* 
     * CheckInputSeq method.
     * Verifies if the user's input sequence matches the correct sequence for a task.
     */
    private void CheckInputSeq()
    {
        if (_userInputSeq.Count > _correctSeq.Length)
        {
            _userInputSeq.Clear();
            return;
        }

        for (int i = 0; i < _userInputSeq.Count; i++)
        {
            if (_userInputSeq[i] != _correctSeq[i])
            {
                _userInputSeq.Clear();
                return;
            }
        }

        if (_userInputSeq.Count == _correctSeq.Length)
        {
            _currentState = GameState.SuccessPage;
        }
    }
}
