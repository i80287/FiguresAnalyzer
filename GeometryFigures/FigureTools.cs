using System;
using System.Collections.Generic;

namespace GeometryFigures
{
    /// <summary>
    /// Represents a class for parsing
    /// figures from the strings.
    /// </summary>
    public static class FigureTools
    {
        /// <summary>
        /// Method to parse figures 
        /// from the string array.
        /// </summary>
        /// <param name="data">String array with data about the figures.</param>
        /// <returns>List of the parsed figures.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Figure[] ParseFigures(string[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException($"{nameof(data)} was not initialized");
            }
            List<Figure> figures = new List<Figure>(data.Length);
            foreach (string line in data)
            {
                if (Figure.TryParse(line, out Figure figure))
                {
                    figures.Add(figure);
                }
            }
            return figures.ToArray();
        }

        /// <summary>
        /// Options for sorting figures.
        /// </summary>
        [Flags]
        public enum SortOptions
        {
            ByArea = 0b01,
            ByRadius = 0b10,
            ByRadiusAndArea = 0b11,
        }
        
        /// <summary>
        /// Method to sort figures array by given options.
        /// </summary>
        /// <param name="figures">Array of figures.</param>
        /// <param name="sortOptions">Flags with sorting options.</param>
        public static void SortFigures(Figure[] figures, SortOptions sortOptions = SortOptions.ByRadius)
        {
            if (sortOptions.HasFlag(SortOptions.ByRadiusAndArea))
            {
                Array.Sort<Figure>(figures, SortRadiusAndAreaDescending());
            }
            else if (sortOptions.HasFlag(SortOptions.ByRadius))
            {
                Array.Sort<Figure>(figures, SortRadiusDescending());
            }
            else
            {
                Array.Sort<Figure>(figures, SortAreaDescending());
            }
        }

        /// <summary>
        /// Method to return IComparer 
        /// to sort by radius.
        /// </summary>
        /// <returns>Radius IComparer</returns>
        public static IComparer<Figure> SortRadiusDescending() => new InverseCompareByRadius();

        /// <summary>
        /// Method to return IComparer  
        /// to sort by area.
        /// </summary>
        /// <returns>Area IComparer</returns>
        public static IComparer<Figure> SortAreaDescending() => new InverseCompareByArea();

        /// <summary>
        /// Method to return IComparer to 
        /// sort by radius of the 
        /// circumscribed circle and area.
        /// </summary>
        /// <returns>Radius and area IComparer</returns>
        public static IComparer<Figure> SortRadiusAndAreaDescending() => new InverseCompareByRadiusAndArea();

        /// <summary>
        /// Represents nested class with 
        /// inversed comparer by the radius  
        /// of the circumscribed circle.
        /// </summary>
        private class InverseCompareByRadius : IComparer<Figure>
        {
            public int Compare(Figure figure1, Figure figure2)
            {
                if (figure1 is null || figure2 is null)
                {
                    throw new ArgumentNullException();
                }
                double radius1 = figure1.CircumscribedCircleRadius();
                double radius2 = figure2.CircumscribedCircleRadius();
                if (radius1 < radius2)
                {
                    return 1;
                }
                if (radius1 == radius2)
                {
                    return 0;
                }
                return -1;
            }
        }

        /// <summary>
        /// Represents nested class with 
        /// inversed comparer by the area
        /// of the regular polygon.
        /// </summary>
        private class InverseCompareByArea : IComparer<Figure>
        {
            public int Compare(Figure figure1, Figure figure2)
            {
                if (figure1 is null || figure2 is null)
                {
                    throw new ArgumentNullException();
                }
                double area1 = figure1.Area();
                double area2 = figure2.Area();
                if (area1 < area2)
                {
                    return 1;
                }
                if (area1 == area2)
                {
                    return 0;
                }
                return -1;
            }
        }

        /// <summary>
        /// Represents nested class with  
        /// inversed comparer by the radius
        /// of the circumscribed circle and
        /// area of the regular polygon.
        /// </summary>
        private class InverseCompareByRadiusAndArea : IComparer<Figure>
        {
            public int Compare(Figure figure1, Figure figure2)
            {
                if (figure1 is null || figure2 is null)
                {
                    throw new ArgumentNullException();
                }
                double radius1 = figure1.CircumscribedCircleRadius();
                double radius2 = figure2.CircumscribedCircleRadius();
                double area1 = figure1.Area();
                double area2 = figure2.Area();
                if (radius1 < radius2)
                {
                    return 1;
                }
                if (radius1 > radius2)
                {
                    return -1;
                }
                if (area1 < area2)
                {
                    return 1;
                }
                if (area1 == area2)
                {
                    return 0;
                }
                return -1;
            }
        }
    }
}
