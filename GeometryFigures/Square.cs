using System;

namespace GeometryFigures
{
    public sealed class Square : Figure
    {
        /// <summary>
        /// Constructor of the square from the 
        /// coordinates of the lower left vertex
        /// and length of the side.
        /// </summary>
        /// <param name="x">Abscissa of the lower left vertex.</param>
        /// <param name="y">Ordinate of the lower left vertex.</param>
        /// <param name="length">Length of the square side.</param>
        /// <exception cref="ArgumentException"></exception>
        public Square(double x, double y, double length) :
            this(new Point(x, y), length)
        { 
        }

        /// <summary>
        /// Constructor of the square from  the
        /// lower left vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the square side.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Square(Point point, double length)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} was not initialized.");
            }
            if (length < 0D)
            {
                throw new ArgumentException("Length of the side of the square can not be less then 0.");
            }
            _points = CalcAllSquareVertices(point, length);
            _n = _points.Length;
            _sideLength = length;
        }

        /// <summary>
        /// Calculate radius of the circumscribed 
        /// circle of the of square.
        /// </summary>
        /// <returns>Radius of the circumscribed circle.</returns>
        public override double CircumscribedCircleRadius()
            => _sideLength / MathConstants.SQRT2;

        /// <summary>
        /// Calculate area of the square.
        /// </summary>
        /// <returns>Area of the square.</returns>
        public override double Area()
            => _sideLength * _sideLength;

        /// <summary>
        /// Get geometric type of the instance.
        /// </summary>
        /// <returns>Type of the instance.</returns>
        public override string GetType() => "Square";

        /// <summary>
        /// Calculate all 3 vertices of the 
        /// square based on the lower left
        /// vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the side of the triangle.</param>
        /// <returns>Array with 4 vertices of the square</returns>
        private static Point[] CalcAllSquareVertices(Point point, double length)
        {
            Point[] points = new Point[4] 
            {
                new Point(point),
                new Point(point.X, point.Y + length),
                new Point(point.X + length, point.Y),
                new Point(point.X + length, point.Y + length),
            };
            return points;
        }
    }
}
