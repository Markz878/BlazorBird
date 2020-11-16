namespace BlazorBird.Models
{
    public class PipeModel
    {
        public double Y { get; set; }
        public double X { get; set; } = 2500;
        public double Vx { get; set; } = -10;
        public double Rotation { get; set; }
        public int Height { get; set; }

        public PipeModel(bool inverted, int height)
        {
            Height = height;
            if (inverted)
            {
                Y = 0;
                Rotation = 180;
            }
            else
            {
                Y = 800-height;
            }
        }

        public void Move()
        {
            X += Vx;
        }
    }
}
