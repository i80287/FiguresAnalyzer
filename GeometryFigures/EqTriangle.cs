using System;

namespace GeometryFigures
{
    public class EqTriangle : Figure
    {

        /// <summary>
        /// Сonstructor of the regular polygon.
        /// </summary>
        /// <param name="points">Array with the vertices of the regular polygon.</param>
        /// <param name="length">Length of the side of the regular polygon.</param>
        public EqTriangle(Point[] points, double length) : base(points)
            => _sideLength = length;

        /// <summary>
        /// Calculate area of equilateral triangle.
        /// </summary>
        /// <returns>Area of the triangle.</returns>
        public override double Area()
            => _sideLength * _sideLength * Math.Sqrt(3.0) / 4;
        /// <remarks>
        /// Root of 3? How about fast Quake inverse square root?
        /// Orig: https://en.wikipedia.org/wiki/Fast_inverse_square_root
        /// <code>
        /// float Q_rsqrt( float number )
        /// {
        ///	    long i;
        ///     float x2, y;
        ///     const float threehalfs = 1.5F;
        ///
        ///     x2 = number * 0.5F;
        ///     y = number;
        ///	    i = * ( long* ) &y;                       // evil floating point bit level hacking
        ///	    i = 0x5f3759df - ( i >> 1 );              // what the fuck? 
        ///	    y = * ( float* ) &i;
        ///	    y = y * ( threehalfs - ( x2 * y * y ) );  // 1st iteration
        ///   //y = y * ( threehalfs - ( x2 * y * y ) );  // 2nd iteration, this can be removed
        ///
        ///	    return y;
        /// }
        /// </code>
        /// </remarks>


    }
}
