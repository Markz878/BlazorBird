using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Math;

namespace BlazorBird.Models
{
  public class GameModel
  {
    public bool IsRunning { get; set; }
    public int Score { get; set; }
    public BirdModel Bird { get; } = new BirdModel() { ImagePath = birdImage, X = 100 };
    public List<PipeModel> Pipes { get; } = [];
    public ScreenSize Screen { get; set; } = new() { Height = 750, Width = 1000 };
    public event Action? PropertyChanged;
    private byte pipeInterval;
    private const string birdImage = "/images/bird.webp";
    private const string dizzyBirdImage = "/images/bird-dizzy.webp";

    public void ScreenSizeChanged(ScreenSize newScreenSize)
    {
      double oldFixedHeight = Screen.Height * 0.95;
      double newFixedHeight = newScreenSize.Height * 0.95;
      double ratio = newFixedHeight / oldFixedHeight;
      Bird.Y = Bird.Y * ratio;
      Screen = newScreenSize;
      Bird.Size = newScreenSize.Height * 0.1;
      foreach (PipeModel pipe in Pipes)
      {
        pipe.Width = Screen.Width * 0.05;
        pipe.Height *= ratio;
        bool inverted = pipe.Rotation == 180;
        pipe.Y = inverted ? 0 : Screen.Height * 0.95 - pipe.Height;
      }
    }

    public async Task MainLoop()
    {
      while (IsRunning)
      {
        Bird.Move();
        for (int i = Pipes.Count - 1; i > 0; i--)
        {
          Pipes[i].Move();
          if (Pipes[i].X < -100)
          {
            Pipes.RemoveAt(i);
          }
        }
        AddPipe();
        CheckBorderCollision();
        CheckPipeCollision();
        Score++;
        PropertyChanged?.Invoke();
        await Task.Delay(20);
      }
    }

    private void CheckPipeCollision()
    {
      int n = Min(2, Pipes.Count);
      for (int i = 0; i < n; i++)
      {
        double diff = Abs(Bird.X - Pipes[i].X);
        if (diff < Bird.Size)
        {
          if (Pipes[i].Rotation == 0) // Pipe from the ground
          {
            if (Bird.Y - diff + Bird.Size > Pipes[i].Y)
            {
              EndGame();
            }
          }
          else // Pipe from the sky
          {
            if ((Bird.Y + diff) < Pipes[i].Y + Pipes[i].Height)
            {
              EndGame();
            }
          }
        }
      }
    }

    private void CheckBorderCollision()
    {
      if (Bird.Y < 0 || Bird.Y > (Screen?.Height * 0.955 - Bird.Size))
      {
        EndGame();
      }
    }

    private void AddPipe()
    {
      pipeInterval++;
      if (pipeInterval > 100)
      {
        pipeInterval = 0;
        bool inverted = Random.Shared.Next(0, 2) == 0;
        double height = Random.Shared.Next(40, 60) * Screen.Height / 100d;
        Pipes.Add(new PipeModel()
        {
          Rotation = inverted ? 180 : 0,
          Height = height,
          X = Screen.Width * 2,
          Y = inverted ? 0 : Screen.Height * 0.95 - height,
          Width = Screen.Width * 0.05,
          Vx = -10
        });
      }
    }

    private void EndGame()
    {
      IsRunning = false;
      Bird.ImagePath = dizzyBirdImage;
    }

    public void Restart()
    {
      Score = 0;
      Pipes.Clear();
      Bird.ImagePath = birdImage;
      IsRunning = true;
      Bird.Y = Screen.Height / 2;
      Bird.Vy = 0;
    }
  }
}
