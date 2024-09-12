using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Game 
{
    public enum GameState 
    {
        CoverPage,
        IntroPage,
        TaskCutCake,
        TaskChaseDucky,
        ChaseSuccess,
        ChaseFail,
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

    // TV task
    private readonly Keys[] _correctSeq = {Keys.Left, Keys.Right, Keys.Up, Keys.Left, Keys.Up, Keys.Right, Keys.Left, Keys.Left};
    private List<Keys> _userInputSeq = new List<Keys>();

    public void Setup() 
    {
        _cakeClicks = 0;
        _cakeSliced = false;
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

    public void Update(float dt) 
    {
        if (_chaseStart && _currentState == GameState.TaskChaseDucky)
        {
            if (_chasePressD)
            {
                _player.Update(dt);
                _chasePressD = false;
            }
            _duck.Update(dt);

            if (_player.Position.X >= _duck.Position.X)
            {
                _currentState = GameState.ChaseSuccess;
            }
            else if ((_player.Position.X >= 1025 || _duck.Position.X >= 1025) && _currentState == GameState.TaskChaseDucky)
            {
                _currentState = GameState.ChaseFail;
            }
        }
    }

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
        else if (key.KeyCode== Keys.S || key.KeyCode == Keys.Down)
        {
        }
        else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
        {
        }
        else if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up)
        {
        }
    }

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
