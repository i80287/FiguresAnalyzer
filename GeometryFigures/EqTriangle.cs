using System;

namespace GeometryFigures
{
    /// <summary>
    /// Represents a class for the equilateral triangle.
    /// </summary>
    public sealed class EqTriangle : Figure
    {
        /// <summary>
        /// Constructor of the equilateral 
        /// triangle from the coordinates 
        /// of the lower left vertex and 
        /// length of the side.
        /// </summary>
        /// <param name="x">Abscissa of the lower left vertex.</param>
        /// <param name="y">Ordinate of the lower left vertex.</param>
        /// <param name="length">Length of the equilateral triangle side.</param>
        /// <exception cref="ArgumentException"></exception>
        public EqTriangle(double x, double y, double length) : 
            this(new Point(x, y), length)
        { 
        }

        /// <summary>
        /// Constructor of the equilateral 
        /// triangle from the lower left 
        /// vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the equilateral triangle side.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public EqTriangle(Point point, double length)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} was not initialized.");
            }
            if (length < double.Epsilon)
            {
                throw new ArgumentException("Length of the side of the triangle can not be less then 0.");
            }
            _points = CalcAllEquilTriangVertices(point, length);
            _n = _points.Length;
            _sideLength = length;
        }
        
        /// <summary>
        /// Calculate radius of the circumscribed 
        /// circle of the of equilateral triangle.
        /// </summary>
        /// <returns>Radius of the circumscribed circle.</returns>
        public override double CircumscribedCircleRadius()
            => _sideLength / MathConstants.SQRT3;

        /// <summary>
        /// Calculate area of the equilateral triangle.
        /// </summary>
        /// <returns>Area of the equilateral triangle.</returns>
        public override double Area()
            => _sideLength * _sideLength * MathConstants.SQRT3 / 4;

        /// <summary>
        /// Get geometric type of the instance.
        /// </summary>
        /// <returns>Type of the instance.</returns>
        public override string GetType() => "EqTriangle";

        /// <summary>
        /// Calculate all 3 vertices of the 
        /// equilateral triangle based on the lower 
        /// left vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the side of the triangle.</param>
        /// <returns>Array with 3 vertices of the equilateral triangle</returns>
        private static Point[] CalcAllEquilTriangVertices(Point point, double length)
        {
            double height = MathConstants.SQRT3 * length / 2;
            Point[] points = new Point[3] 
            {
                new Point(point),
                new Point(point.X + length / 2, height),
                new Point(point.X + length, point.Y),
            };
            return points;
        }
    }
}
