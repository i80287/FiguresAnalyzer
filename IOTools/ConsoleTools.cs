using System;
using System.Collections;

namespace IOTools
{
    public static class ConsoleTools 
    {
        /// <summary>
        /// Print objects to the console separating 
        /// by sep string and add end string to the end.
        /// </summary>
        /// <param name="objects">Array of objects to print to the console.</param>
        /// <param name="sep">Separator string between objects.</param>
        /// <param name="end">End of the string sequence.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Print(object[] objects, char sep = ' ', string end = "\n")
        {
            if (objects is null)
            {
                throw new ArgumentNullException($"{nameof(objects)} was not initialized");
            }
            Console.Write(string.Join(sep, objects));
            if (!string.IsNullOrEmpty(end))
            {
                Console.Write(end);
            }
        }
    }
}
