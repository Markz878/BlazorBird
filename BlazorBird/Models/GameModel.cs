using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using static System.Math;

namespace BlazorBird.Models
{
    public class GameModel
    {
        public bool IsRunning { get; set; }
        public int Score { get; set; }
        public BirdModel Bird { get; } = new BirdModel() { ImagePath = birdImage };
        public List<PipeModel> Pipes { get; } = new List<PipeModel>();

        public event Action PropertyChanged;
        private byte pipeInterval;
        private const string birdImage = "/images/bird.png";
        private const string dizzyBirdImage = "/images/bird-dizzy.png";

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
                if (Abs(Bird.X - Pipes[i].X) < 40)
                {
                    if (Pipes[i].Rotation == 0) // Pipe from the ground
                    {
                        if (Bird.Y + Bird.Size > Pipes[i].Y)
                        {
                            EndGame();
                        }
                    }
                    else // Pipe from the sky
                    {
                        if (Bird.Y < Pipes[i].Y + Pipes[i].Height)
                        {
                            EndGame();
                        }
                    }
                }
            }
        }

        private void CheckBorderCollision()
        {
            if (Bird.Y < 0 || Bird.Y > 750)
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
                Pipes.Add(new PipeModel(Random.Shared.Next(0, 2) == 0, Random.Shared.Next(400, 650)));
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
            Bird.Y = 500;
            Bird.Vy = 0;
        }
    }
}
