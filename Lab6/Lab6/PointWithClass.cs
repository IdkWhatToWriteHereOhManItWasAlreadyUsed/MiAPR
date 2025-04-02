namespace Lab6
{

    public class PointWithClass(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public int ClassNum { get; set; } = -1;
        public double DistanceTo(PointWithClass other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

