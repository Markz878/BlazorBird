using static System.Math;

namespace FlappyBird.Models
{
    public class BirdModel
    {
        public double Y { get; set; } = 500;
        public double X { get; set; } = 300;
        public double Vy { get; set; } = 0;
        public double Rotation { get; set; } = 0;

        public void Move()
        {
            Vy += 0.1;
            Y += Vy;
            if (Rotation < 30)
            {
                Rotation += 0.5;
            }
        }

        public void Jump()
        {
            Vy -= 10;
            Rotation = -30;
        }
    }
}
