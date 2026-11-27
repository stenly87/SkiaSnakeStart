using System.Reflection;

namespace WinFormsApp2;

public class Game
{
    public int SnakeSize { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool GameRunning { get; private set; }
    public Snake Snake { get; private set; }
    public Apple Apple { get; private set; }

    private List<SnakeJunk> _junk = new();

    public SnakeJunk[] Junk
    {
        get => _junk.ToArray();
    }

    public EventHandler OnGameFrameChanged;

    public Game(int width, int height, int snakeSize)
    {
        SnakeSize = snakeSize;
        Width = width;
        Height = height;
    }

    private void GenerateApple()
    {
        Apple = new Apple(
            x: _random.Next(0, Width / SnakeSize) * SnakeSize,
            y: _random.Next(0, Height / SnakeSize) * SnakeSize);
    }

    private Random _random = new();

    public void Start()
    {
        if (GameRunning)
            return;
        GameRunning = true;
        GenerateApple();
        Snake = new(
            x: _random.Next(0, Width / SnakeSize) * SnakeSize,
            y: _random.Next(0, Height / SnakeSize) * SnakeSize,
            snakeSize: SnakeSize);
        Thread _thread = new(ThreadStart);
        _thread.Start();
    }

    public Direction NextDirection { get; set; } = Direction.Up;

    private void ThreadStart()
    {
        while (GameRunning)
        {
            Thread.Sleep(200);
            Snake.Move(NextDirection);
            CheckIntersect();
            JunkRotten();
            OnGameFrameChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void JunkRotten()
    {
        if (_random.Next(0, 100) < 80)
            return;
        for (int i = 0; i < _junk.Count; i++)
            _junk[i].Rotten();
    }

    private void CheckIntersect()
    {
        CheckIntersectWithSnake();

        CheckIntersectWithJunk();

        CheckIntersectWithApple();
    }

    private void CheckIntersectWithJunk()
    {
        var head = Snake.Head;
        for (int i = 0; i < _junk.Count; i++)
        {
            if (head.X == _junk[i].X && head.Y == _junk[i].Y)
            {
                _junk.RemoveAt(i);
                Snake.EatJunk();
                break;
            }
        }
    }

    private void CheckIntersectWithSnake()
    {
        var cells = Snake.Cells;
        var head = Snake.Head;
        for (int i = 1; i < cells.Length; i++)
        {
            if (head.X == cells[i].X && head.Y == cells[i].Y)
            {
                var junk = Snake.Slice(i);
                _junk.AddRange(junk);
                break;
            }
        }
    }

    private void CheckIntersectWithApple()
    {
        var head = Snake.Head;
        if (head.X == Apple.X &&
            head.Y == Apple.Y)
        {
            GenerateApple();
            Snake.Increase();
        }
    }

    public void Stop()
    {
        GameRunning = false;
    }
}
