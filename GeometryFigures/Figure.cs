using System;
using System.Text.RegularExpressions;

namespace GeometryFigures
{
    /// <summary>
    /// Represents a class for any regular polygon.
    /// </summary>
    /// <remarks>
    /// Contains basic methods for calculating 
    /// area and radius of the circumscribed 
    /// circle of the figure.
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

        /// <summary>
        /// Empty constructor in case derived classes 
        /// will call base constructor without parameters.
        /// </summary>
        protected Figure() 
        { 
        }

        /// <summary>
        /// Сonstructor of the regular polygon.
        /// </summary>
        /// <param name="points">
        /// Array with the vertices of the regular polygon. 
        /// Array length should be at least 2.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Figure(Point[] points)
        {
            if (points is null)
            {
                throw new ArgumentNullException("Provided array with points was not initialized.");
            }
            if (points.Length < 2)
            {
                throw new ArgumentException("Array with points can not contain less then 2 points");
            }
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
        /// S = (n * a * a) / (4 * tan(pi / n)
        /// </remarks>
        /// <returns>Area of the regular polygon.</returns>
        public virtual double Area()
            => _n * _sideLength * _sideLength / (4 * Math.Tan(Math.PI / _n));

        /// <summary>
        /// Get geometric type of the instance.
        /// </summary>
        /// <returns>Type of the instance.</returns>
        public new virtual string GetType() => "Figure";

        public override string ToString() => $"{GetType()} {_points[0]} {_sideLength}";

        /// <summary>
        /// Converts data from the string to the figure.
        /// </summary>
        /// <param name="line">A string containing figure data to convert.</param>
        /// <param name="figure">Figure to initialize based on the data from the string.</param>
        /// <returns>true if line was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string line, out Figure figure)
        {
            figure = new Figure();
            if (line is null)
            {
                return false;
            }
            const string LINEPATTERN = "^(?:(Square|EqTriangle)) -?\\d+((\\.|\\,)\\d+)? -?\\d+((\\.|\\,)\\d+)? \\d+((\\.|\\,)\\d+)?$";
            if (!Regex.IsMatch(line, LINEPATTERN))
            {
                return false;
            }
            string[] lineArgs = Regex.Replace(line, ",", ".").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string xArg = lineArgs[1];
            string yArg = lineArgs[2];
            string sideLengthArg = lineArgs[3];
            if (!double.TryParse(xArg, out double x))
            {
                return false;
            }
            if (!double.TryParse(yArg, out double y))
            {
                return false;
            }
            if (!double.TryParse(sideLengthArg, out double sideLenght))
            {
                return false;
            }
            if (sideLenght < double.Epsilon)
            {
                return false;
            }
            if (lineArgs[0].Equals("Square"))
            {
                figure = new Square(x, y, sideLenght);
            }
            else
            {
                figure = new EqTriangle(x, y, sideLenght);
            }
            return true;
        }
    }
}
