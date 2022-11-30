﻿using System;
using System.Text;
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
        private const string ParseReport = "Successfully parsed {0} figures:";
        private const string AskToContinueReport = "Press Escape to exit. Press any key to continue";
        private const string SavedDataReport = "Successfully saved data with sorted figures to the file:\n{0}\n";
        private const string SavedDataErrorReport = "An error occured while attempting to write to the file.";
        private const string EmptyFiguresReport = "Correct data about the figures was not found in the file.";

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
                if (figures.Length == 0)
                {
                    Console.WriteLine(EmptyFiguresReport);
                    Console.WriteLine(AskToContinueReport);
                    continue;
                }
                FigureTools.SortFigures(figures, FigureTools.SortOptions.ByRadius);

                table = new ConsoleTable("Figure", "Abscissa", "Ordinate", "Side length");
                table.AddRows(figures);

                Console.WriteLine(ParseReport, figures.Length);
                Console.WriteLine(table);
                WriteFiguresToFile(figures);
                Console.WriteLine(AskToContinueReport);
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
                Console.WriteLine(SavedDataReport, fileFullPath);
            }
            catch
            {
                Console.WriteLine(SavedDataErrorReport);
            }
        }

        /// <summary>
        /// Method to change current locale to en-US
        /// and change console input-output encodings.
        /// </summary>
        private void ChangeLocale()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding("windows-1251");
            Console.OutputEncoding = Encoding.UTF8;            
            System.Threading.Thread.CurrentThread.CurrentCulture
                = new System.Globalization.CultureInfo("en-US", false);
            System.Threading.Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo("en-US", false);
        }
    }
}
