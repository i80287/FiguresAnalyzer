namespace GeometryFigures
{
    /// <summary>
    /// Class for the point 
    /// on the plane.
    /// </summary>
    public sealed class Point
    {
        private readonly double _x;
        private readonly double _y;

        public double X => _x;
        public double Y => _x;

        public Point() : this(0.0, 0.0) { }

        public Point(double x, double y) => (_x, _y) = (x, y);       

        public override string ToString() => $"{_x} {_y}";
    }
}
