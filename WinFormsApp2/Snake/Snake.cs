using System.Collections;

namespace WinFormsApp2;

public class Snake
{
    private readonly int _snakeSize;
    public Direction Direction { get; set; }

    public Cell Head
    {
        get =>
            new Cell { X = _snakeCells[0].X, Y = _snakeCells[0].Y };
    }

    public Cell[] Cells
    {
        get => _snakeCells.ToArray();
    }

    private List<Cell> _snakeCells;

    public Snake(int x, int y, int snakeSize)
    {
        _snakeSize = snakeSize;
        _snakeCells = new();
        _snakeCells.Add(new Cell { X = x, Y = y });
    }

    public void Move(Direction nextDirection)
    {
        for (int i = _snakeCells.Count - 1; i > 0; i--)
        {
            _snakeCells[i].X = _snakeCells[i - 1].X;
            _snakeCells[i].Y = _snakeCells[i - 1].Y;
        }

        Direction = nextDirection;

        switch (Direction)
        {
            case Direction.Up:
                _snakeCells[0].Y -= _snakeSize;
                break;
            case Direction.Down:
                _snakeCells[0].Y += _snakeSize;
                break;
            case Direction.Left:
                _snakeCells[0].X -= _snakeSize;
                break;
            case Direction.Right:
                _snakeCells[0].X += _snakeSize;
                break;
        }
    }

    public void Increase()
    {
        _snakeCells.Add(new Cell { X = _snakeCells[^1].X, Y = _snakeCells[^1].Y });
    }

    public SnakeJunk[] Slice(int i)
    {
        var junk = _snakeCells.Skip(i).ToArray();
        _snakeCells = new(_snakeCells.Take(i));
        return junk.Select(s => new SnakeJunk { X = s.X, Y = s.Y }).ToArray();
    }

    private int countJunk = 0;

    public void EatJunk()
    {
        countJunk++;
        if (countJunk == 3)
        {
            countJunk = 0;
            Increase();
        }
    }
}