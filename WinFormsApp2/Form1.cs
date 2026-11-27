using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WinFormsApp2;

public partial class Form1 : Form
{
    private Game game;
    private GameGraphic gameGraphic;
    
    public Form1()
    {
        InitializeComponent();
        SKControl skControl = new SKControl();
        this.Controls.Add(skControl);
        
        Closing += (sender, args) =>
        {
            if (game.GameRunning)
                game.Stop();
        };
        game = new Game(width: 500, height: 500, snakeSize: 50);
        gameGraphic = new(skControl, game);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        game.Start();
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