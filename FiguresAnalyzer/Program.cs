﻿using System;
using IOTools;
using GeometryFigures;

/// <remarks>
/// "Вариант 16"
/// This work made by
/// kormilitsyn vladimir
/// from group БПИ226.
/// </remarks>
namespace FiguresAnalyzer
{
    /// <summary>
    /// Represents a class with 
    /// main program loop.
    /// </summary>
    public class Program
    {
        private const string parseReport = "Successfully parsed {0} figures:";
        private const string askToContinueReport = "Press Escape to exit. Press any key to continue";
        private const string savedDataReport = "Successfully saved data with sorted figures to the file:\n{0}\n";
        private const string savedDataErrorReport = "An error occured while attempting to write to the file.";

        private ConsoleTable table;

        private static void Main(string[] args)
            => new Program().Run();

        public Program()
        {// Change locale to en-US 
         // to avoid errors with parsing
         // double numbers from the file.
            ChangeLocale();            
        }                                                    

        /// <summary>
        /// Method with the main program loop.
        /// </summary>
        public void Run()
        {
            do 
            {
                string[] data = FileTools.RequestDataFromFile();
                Figure[] figures = FigureTools.ParseFigures(data);
                FigureTools.SortFigures(figures, FigureTools.SortOptions.ByRadius);

                table = new ConsoleTable("Figure", "Abscissa", "Ordinate", "Side length");
                table.AddRows(figures);

                Console.WriteLine(parseReport, figures.Length);
                Console.WriteLine(table);
                WriteFiguresToFile(figures);
                Console.WriteLine(askToContinueReport);
            } 
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Method to write data from the 
        /// array of figures to the file.
        /// </summary>
        /// <param name="figures">Array of figures</param>
        private void WriteFiguresToFile(Figure[] figures)
        {
            try
            {
                string fileFullPath = FileTools.WritebjectsToFile(figures);
                Console.WriteLine(savedDataReport, fileFullPath);
            }
            catch
            {
                Console.WriteLine(savedDataErrorReport);
            }
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
