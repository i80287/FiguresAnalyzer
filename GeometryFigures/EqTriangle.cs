namespace GeometryFigures
{
    public class EqTriangle : Figure
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
        public EqTriangle(double x, double y, double length) : 
            this(new Point(x, y), length) { }

        /// <summary>
        /// Constructor of the equilateral 
        /// triangle from the lower left 
        /// vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the equilateral triangle side.</param>
        public EqTriangle(Point point, double length)
        {
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
        /// Calculate area of equilateral triangle.
        /// </summary>
        /// <returns>Area of the equilateral triangle.</returns>
        public override double Area()
            => _sideLength * _sideLength * MathConstants.SQRT3 / 4;

        /// <summary>
        /// Calculate all 3 vertices of the 
        /// equilateral triangle based on the lower 
        /// left vertex and length of the side.
        /// </summary>
        /// <param name="point">Lower left vertex.</param>
        /// <param name="length">Length of the side of the triangle.</param>
        /// <returns>Array with 3 vertices of the equilateral triangle</returns>
        public Point[] CalcAllEquilTriangVertices(Point point, double length)
        {
            Point[] points = new Point[3];
            points[0] = new Point(point);
            points[1] = new Point(point.X + length, point.Y);
            double height = MathConstants.SQRT3 * length / 2;
            points[2] = new Point(point.X + length / 2, height);
            return points;
        }
    }
}
