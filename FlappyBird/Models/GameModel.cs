﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using static System.Math;

namespace FlappyBird.Models
{
    public class GameModel : INotifyPropertyChanged
    {
        public bool IsRunning { get; set; }
        public int Score { get; set; }
        public BirdModel Bird { get; } = new BirdModel();
        public List<PipeModel> Pipes { get; } = new List<PipeModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        readonly Random random = new Random();

        byte pipeInterval = 0;

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
                OnPropertyChanged();
                await Task.Delay(10);
            }
        }

        private void CheckPipeCollision()
        {
            for (int i = 0; i < Pipes.Count; i++)
            {
                if (Abs(Bird.X - Pipes[i].X) < 50)
                {
                    if (Pipes[i].Rotation == 0)
                    {
                        if (Bird.Y > Pipes[i].Y)
                        {
                            EndGame();
                        }
                    }
                    else
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
            if (pipeInterval>100)
            {
                pipeInterval = 0;
                Pipes.Add(new PipeModel(random.Next(0, 2) == 0, random.Next(400, 650)));
            }

        }

        private void EndGame()
        {
            IsRunning = false;
        }

        public void Restart()
        {
            Score = 0;
            Pipes.Clear();
            IsRunning = true;
            Bird.Y = 500;
            Bird.Vy = 0;
        }

        protected void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, null);
        }


    }
}
