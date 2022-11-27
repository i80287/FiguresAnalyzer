﻿using System;
using System.IO;
using System.Text;

namespace IOTools
{
    public static class FileTools
    {
        private const string fileNameRequestReport = "Please type file name of the file with figures data\n\n"
                                                   + "File will be searched in:\n{0}\n\n> ";
        private const string badNameFormatReport = "Invalid file name format";
        private const string missingFileReport = "File not found";
        private const string fileReadErrorReport = "An error occured while reading the file. Please type file name again";

        private static readonly Encoding readEncoding;
        private static readonly Encoding writeEncoding;
        private static readonly char[] invalidNameChars = Path.GetInvalidFileNameChars();
        private static readonly string currentWorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        static FileTools()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            writeEncoding = Encoding.GetEncoding("windows-1251");
            readEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Method to write string representation 
        /// of given array of objects to the file.
        /// </summary>
        /// <param name="objects">Array of objects to write to the save.</param>
        /// <returns>File name in which data was written.</returns>
        /// <exception cref="IOException"></exception>
        public static string WriteFiguresToFile(object[] objects)
        {
            string fileName = FindFreeFileName();
            string[] stringFigures = Array.ConvertAll(objects, obj => obj.ToString());
            try
            {
                File.WriteAllLines(fileName, stringFigures, encoding: writeEncoding);
                return Path.GetFullPath(fileName);
            }
            catch (Exception ex)
            {
                throw new IOException($"Was not able to write to the file {fileName}; Error message: {ex.Message}");
            }
        }

        private static string FindFreeFileName()
        {
            string fileName = "Data1.txt";
            int i = 1;
            while (File.Exists(fileName))
            {
                fileName = fileName.Replace(i.ToString(), (++i).ToString());
            }
            return fileName;
        }

        public static string[] RequestDataFromFile()
        {
            string path = RequestPathToExistingFile();
            
            string[] fileData;
            try
            { 
                fileData = File.ReadAllLines(path, encoding: readEncoding); 
            }
            catch 
            { 
                fileData = null; 
            }

            if (fileData is null)
            {
                Console.WriteLine(fileReadErrorReport);
                return RequestDataFromFile();
            }

            return fileData;
        }

        private static string RequestPathToExistingFile()
        {
            Console.Write(fileNameRequestReport, currentWorkingDirectory);
            string userInput = Console.ReadLine();

            while (!ValidateFileName(userInput) || !File.Exists(userInput))
            {
                if (!ValidateFileName(userInput))
                { 
                    Console.WriteLine(badNameFormatReport); 
                }
                else
                { 
                    Console.WriteLine(missingFileReport); 
                }
                Console.Write(fileNameRequestReport, currentWorkingDirectory);
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool ValidateFileName(string fileName)
            => fileName.IndexOfAny(invalidNameChars) == -1;
    }
}