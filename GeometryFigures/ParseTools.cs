using System;
using System.Collections.Generic;

namespace GeometryFigures
{
    /// <summary>
    /// Represents a class for parsing
    /// figures from the strings.
    /// </summary>
    public static class ParseTools
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
    }
}
