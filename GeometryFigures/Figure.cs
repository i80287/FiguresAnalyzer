using System;

namespace GeometryFigures
{
    /// <summary>
    /// Base figure class. 
    /// </summary>
    /// <remarks>
    /// Base figure class for the regular polygons. 
    /// Contains basic methods for calculating area 
    /// and radius of the circumscribed circle of
    /// the figure.
    /// </remarks>
    public class Figure
    {
        /// <summary>
        /// Array with vertices of the regular polygon.
        /// </summary>
        protected Point[] _points;
        /// <summary>
        /// Amout of vertices of the regular polygon.
        /// </summary>
        protected int _n;
        /// <summary>
        /// Length of the regular polygon side.
        /// </summary>
        protected double _sideLength = 0.0;

        protected Figure() : this(new Point[0]) { }

        /// <summary>
        /// Сonstructor of the regular polygon.
        /// </summary>
        /// <param name="points">Array with the vertices of the regular polygon.</param>
        public Figure(Point[] points)
        {
            _n = points.Length;
            _points = new Point[_n];
            Array.Copy(points, _points, _n);
        }

        /// <summary>
        /// Calculate radius of the circumscribed circle
        /// of the regular polygon with n vertices.
        /// </summary>
        /// <remarks>
        /// By default radius is calculated by the formula:
        /// R = a / (2 * sin(pi / n))
        /// </remarks>
        /// <returns>Radius of the circumscribed circle.</returns>
        public virtual double CircumscribedCircleRadius()
            => _sideLength / (2 * Math.Sin(Math.PI / _n));

        /// <summary>
        /// Calculate area of the regular 
        /// polygon with n vertices.
        /// </summary>
        /// <remarks>
        /// By default area is calculated by the formula:
        /// S = (n * a^2) / (4 * tan(pi / n)
        /// </remarks>
        /// <returns>Area of the regular polygon.</returns>
        public virtual double Area()
            => _n * _sideLength * _sideLength / (4 * Math.Tan(Math.PI / _n));

        public override string ToString()
        {
            string coords = string.Join<Point>(' ', _points);
            string type = GetType().ToString();
            double area = Area();
            return $"{type} {coords} {_sideLength} {area}";
        }

        public new virtual Type GetType() => typeof(Figure);
    }
}
