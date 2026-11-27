namespace WinFormsApp2;

public class SnakeJunk : Cell
{
    public int Alive { get; private set; } = 5;

    public void Rotten()
    {
        Alive--;
    }
}