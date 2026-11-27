using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WinFormsApp2;

public class GameGraphic
{
    private readonly SKControl _skControl;
    private readonly Game _game;

    public GameGraphic(SKControl skControl, Game game)
    {
        _skControl = skControl;
        _game = game;
        _skControl.Width = game.Width;
        _skControl.Height = game.Height;

        _skControl.PaintSurface += DrawField;
        _skControl.PaintSurface += DrawSnake;
        _skControl.PaintSurface += DrawApple;
        _skControl.PaintSurface += DrawJunk;

        game.OnGameFrameChanged += (sender, args) =>
        {
            try
            {
                skControl.Invoke(() => { skControl.Invalidate(); });
            }
            catch
            {
            }
        };
    }

    private void DrawJunk(object? sender, SKPaintSurfaceEventArgs e)
    {
        if (!_game.GameRunning)
            return;
        var paint = e.Surface.Canvas;

        SKPaint pen = new SKPaint();
        foreach (var cell in _game.Junk)
        {
            switch (cell.Alive)
            {
                case 5: pen.Color = SKColors.DarkOliveGreen; break;
                case 4: pen.Color = new SKColor(85, 107, 67, 255); break;
                case 3: pen.Color = new SKColor(85, 107, 87, 255); break;
                case 2: pen.Color = new SKColor(85, 107, 107, 255); break;
                default: pen.Color = new SKColor(85, 107, 127, 255); break;
            }
            paint.DrawRect(cell.X, cell.Y, _game.SnakeSize, _game.SnakeSize, pen);
        }
    }

    private void DrawApple(object? sender, SKPaintSurfaceEventArgs e)
    {
        if (!_game.GameRunning)
            return;
        var paint = e.Surface.Canvas;
        SKPaint pen = new SKPaint { Color = SKColors.Red };
        paint.DrawCircle(_game.Apple.X + _game.SnakeSize / 2, _game.Apple.Y + _game.SnakeSize / 2, _game.SnakeSize / 2,
            pen);
    }

    private void DrawSnake(object? sender, SKPaintSurfaceEventArgs e)
    {
        if (!_game.GameRunning)
            return;
        var paint = e.Surface.Canvas;

        SKPaint pen = new SKPaint { Color = SKColors.Tomato };
        bool start = true;
        foreach (var cell in _game.Snake.Cells)
        {
            if (start)
            {
                paint.DrawCircle(cell.X + _game.SnakeSize / 2, cell.Y + _game.SnakeSize / 2, _game.SnakeSize / 2, pen);
                start = false;
            }
            else
            {
                pen.Color = SKColors.Green;
                paint.DrawRect(cell.X, cell.Y, _game.SnakeSize, _game.SnakeSize, pen);
            }
        }
    }

    private void DrawField(object? sender, SKPaintSurfaceEventArgs e)
    {
        if (!_game.GameRunning)
            return;

        var paint = e.Surface.Canvas;
        paint.DrawColor(SKColors.Black);
    }
}