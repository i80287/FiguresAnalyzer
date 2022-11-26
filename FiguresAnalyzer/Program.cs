using System;
using System.Collections.Generic;
using IOTools;
using GeometryFigures;
using System.Text;

namespace FiguresAnalyzer
{
    public class Program
    {
        private const string parseReport = "Successfully parsed {0} figures:";
        private const string askToContinueReport = "Press Escape to exit. Press any key to continue";
        private const string savedDataReport = "Successfully saved data to the file:\n{0}\n";
        private const string savedDataErrorReport = "An error occured while attempting to write to the file.";

        private static void Main(string[] args)
            => new Program().Run();

        public Program()
        {// Change locale to en-US 
         // to avoid errors with parsing
         // double numbers from the file.
            ChangeLocale();
        }

        /// <summary>
        /// Method with main program loop.
        /// </summary>
        public void Run()
        {
            do
            {
                string[] data = FileTools.RequestDataFromFile();
                Figure[] figures = ParseFigures(data);
                Console.WriteLine(parseReport, figures.Length);
                ConsoleTools.Print(figures, sep: '\n', end: "\n\n");
                try
                {
                    string fileFullPath = FileTools.WriteFiguresToFile(figures);
                    Console.WriteLine(savedDataReport, fileFullPath);
                }
                catch
                {
                    Console.WriteLine(savedDataErrorReport);
                }
                Console.WriteLine(askToContinueReport);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Method to parse figures 
        /// from the string array.
        /// </summary>
        /// <param name="data">String array with data about the figures.</param>
        /// <returns>List of the parsed figures.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private Figure[] ParseFigures(string[] data)
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
        /// Method to change current locale to en-US.
        /// </summary>
        private void ChangeLocale()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Threading.Thread.CurrentThread.CurrentCulture
                = new System.Globalization.CultureInfo("en-US", false);
            System.Threading.Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo("en-US", false);
        }
    }
}