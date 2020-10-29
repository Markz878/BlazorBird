using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;

namespace FlappyBird.Models
{
    public class GameModel : INotifyPropertyChanged
    {
        public BirdModel Bird { get; } = new BirdModel();
        public bool IsRunning { get; set; }
        public string YPosition => Bird.Y.ToString(CultureInfo.InvariantCulture) + "px";
        public string XPosition => Bird.X.ToString(CultureInfo.InvariantCulture) + "px";
        public string Rotation => Bird.Rotation.ToString(CultureInfo.InvariantCulture) + "deg";
        public List<PipeModel> Pipes { get; } = new List<PipeModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task MainLoop()
        {
            Pipes.Add(new PipeModel(true));
            Pipes.Add(new PipeModel(false));
            while (IsRunning)
            {
                Bird.Move();
                if (Bird.Y > 750)
                {
                    IsRunning = false;
                }
                OnPropertyChanged();
                await Task.Delay(10);
            }
        }

        public void Restart()
        {
            IsRunning = true;
            Bird.Y = 500;
            Bird.Vy = 0;
            Bird.Rotation = 0;
        }

        protected void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, null);
        }


    }
}
