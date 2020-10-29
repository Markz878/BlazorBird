namespace FlappyBird.Models
{
    public class BirdModel
    {
        public double Y { get; set; } = 500;
        public double X { get; set; } = 300;
        public double Vy { get; set; } = 0;
        public double Rotation => 4 * Vy;

        public void Move()
        {
            Vy += 0.15;
            Y += Vy;
        }

        public void Jump()
        {
            Vy -= 5;
        }
    }
}
