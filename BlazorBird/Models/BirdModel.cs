namespace BlazorBird.Models
{
    public record BirdModel
    {
        public double Y { get; set; }
        public double X { get; set; }
        public double Size { get; set; }
        public double Vy { get; set; }
        public double Rotation => 4 * Vy;
        public required string ImagePath { get; set; }

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
