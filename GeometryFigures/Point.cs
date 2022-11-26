using System;

namespace GeometryFigures
{
    /// <summary>
    /// Represents a class for the point on plane.
    /// </summary>
    public sealed class Point
    {
        private readonly double _x;
        private readonly double _y;

        public double X => _x;
        public double Y => _y;

        /// <summary>
        /// Default point constructor.
        /// </summary>
        public Point() : this(0.0, 0.0) { }

        /// <summary>
        /// Constructor for the point from the coordinates.
        /// </summary>
        /// <param name="x">Abscissa of the point.</param>
        /// <param name="y">Ordinate of the point.</param>
        public Point(double x, double y) => (_x, _y) = (x, y);
        
        /// <summary>
        /// Constructor for the point from another point.
        /// </summary>
        /// <param name="point">Point to clone from.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Point(Point point)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} was not initialized.");
            }
            (_x, _y) = (point.X, point.Y);
        }

        public override string ToString() => $"{_x} {_y}";
    }
}
