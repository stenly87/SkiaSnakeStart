using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WinFormsApp2;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        SKControl skControl = new SKControl();
        skControl.Width = 500;
        skControl.Height = 500;
        skControl.PaintSurface += SkControlOnPaintSurface;
        this.Controls.Add(skControl);

        game.OnMove += (sender, args) => { skControl.Invoke(() => { skControl.Invalidate(); }); };
    }

    private void SkControlOnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var paint = e.Surface.Canvas;
        paint.DrawColor(SKColors.Black);
        SKPaint pen = new SKPaint { Color = SKColors.Tomato };
        bool start = true;
        foreach (var cell in game.Snake.SnakeCells)
        {
            if (start)
            {
                paint.DrawCircle(cell.X+5, cell.Y+5, 5, pen);
                start = false;
            }
            else
            {
                pen.Color = SKColors.Green;
                paint.DrawRect(cell.X, cell.Y, 10, 10, pen);
            }
        }
        /*SKPaint pen = new SKPaint { Color = SKColors.Tomato };
        paint.DrawColor(SKColors.Black);
        paint.DrawRect(50, 50, 300, 200, pen);
        pen.Color = SKColors.GreenYellow;
        paint.DrawCircle(150, 200, 50, pen);
        paint.DrawCircle(280, 200, 50, pen);
        pen.Color = SKColors.Blue;
        paint.DrawOval(75, 100, 25, 50, pen);
        pen.IsStroke = true;
        pen.Color = SKColors.Green;
        pen.StrokeWidth = 5;
        paint.DrawPoints(SKPointMode.Polygon,
            [new  SKPoint(50, 50), new SKPoint(100, 100),
            new  SKPoint(100, 50), new  SKPoint(50, 50)],
            pen);*/
    }

    private Game game = new();

    private void button1_Click(object sender, EventArgs e)
    {
        game.Start();
        this.Focus();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        game.Stop();
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                game.NextDirection = Direction.Up;
                break;
            case Keys.S:
                game.NextDirection = Direction.Down;
                break;
            case Keys.A:
                game.NextDirection = Direction.Left;
                break;
            case Keys.D:
                game.NextDirection = Direction.Right;
                break;
        }
    }
}

public class SnakeCell
{
    public Direction Direction { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Snake
{
    public List<SnakeCell> SnakeCells { get; set; } = new();

    public Snake()
    {
        SnakeCells = new();
        SnakeCells.Add(new SnakeCell { X = 50, Y = 50 });
    }

    public void Move(Direction nextDirection)
    {
        for (int i = SnakeCells.Count - 1; i > 0; i--)
        {
            SnakeCells[i].Direction = SnakeCells[i - 1].Direction;
            SnakeCells[i].X = SnakeCells[i - 1].X;
            SnakeCells[i].Y = SnakeCells[i - 1].Y;
        }

        SnakeCells[0].Direction = nextDirection;

        switch (SnakeCells[0].Direction)
        {
            case Direction.Up:
                SnakeCells[0].Y -= 10;
                break;
            case Direction.Down:
                SnakeCells[0].Y += 10;
                break;
            case Direction.Left:
                SnakeCells[0].X -= 10;
                break;
            case Direction.Right:
                SnakeCells[0].X += 10;
                break;
        }
    }
}

internal class Game
{
    bool snakeIsAlive = true;
    public Snake Snake { get; set; } = new();

    public EventHandler OnMove;

    public void Start()
    {
        snakeIsAlive = true;
        Snake = new();
        Thread thread = new(ThreadStart);
        thread.Start();
    }

    public Direction NextDirection { get; set; } = Direction.Up;

    private void ThreadStart()
    {
        while (snakeIsAlive)
        {
            Thread.Sleep(100);
            Snake.Move(NextDirection);
            OnMove?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Stop()
    {
        snakeIsAlive = false;
    }
}
