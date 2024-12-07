namespace BlazorBird.Models
{
    public record PipeModel
    {
        public double Y { get; set; }
        public double X { get; set; }
        public double Vx { get; set; }
        public double Rotation { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public void Move()
        {
            X += Vx;
        }
    }
}
