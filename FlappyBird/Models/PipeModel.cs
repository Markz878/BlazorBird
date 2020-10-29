namespace FlappyBird.Models
{
    public class PipeModel
    {
        public double Y { get; set; }
        public double X { get; set; }
        public double Vx { get; set; }
        public bool Inverted { get; set; }

        public PipeModel(bool inverted)
        {
            Inverted = inverted;
            X = 1;
            if (inverted)
            {
                Y = 0;
            }
            else
            {
                Y = 1;
            }
        }

        public void Move()
        {
            X += Vx;
        }
    }
}
