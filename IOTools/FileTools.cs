using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace IOTools
{
    public static class FileTools
    {
        private const string fileNameRequestReport = "\nPlease type file name of the file with figures data.\n\n"
                                                   + "File will be searched in:\n{0}\n\n> ";
        private const string badNameFormatReport = "Invalid file name format.";
        private const string missingFileReport = "File {0} not found.";
        private const string fileReadErrorReport = "An error occured while reading the file. Please type file name again.";
        private const string emptyFileReport = "Empty file is provided. Please type file name again.";

        private static readonly Encoding defaultReadEncoding;
        private static readonly Encoding writeEncoding;
        private static readonly char[] invalidNameChars = Path.GetInvalidFileNameChars();
        private static readonly string currentWorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Static constructor to register encoding 
        /// provider for the windows-1251 encoding.
        /// </summary>
        static FileTools()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            writeEncoding = Encoding.GetEncoding("windows-1251");
            defaultReadEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Method to write string representation 
        /// of given array of objects to the file.
        /// </summary>
        /// <param name="objects">Array of objects to write to the save.</param>
        /// <returns>File name in which data was written.</returns>
        /// <exception cref="IOException"></exception>
        public static string WritebjectsToFile(object[] objects)
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

        /// <summary>
        /// Method to find not taken file name
        /// in the current working directory.
        /// </summary>
        /// <returns>File name.</returns>
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

        /// <summary>
        /// Method to get data from the file. 
        /// File name is selected by user.
        /// </summary>
        /// <returns>File data.</returns>
        public static string[] RequestDataFromFile()
        {
            string path = RequestPathToExistingFile();
            Encoding encoding = DetectEncoding(path);
            string[] fileData;
            try
            {
                fileData = File.ReadAllLines(path, encoding: encoding); 
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
            if (fileData.Length == 0)
            {
                Console.WriteLine(emptyFileReport);
                return RequestDataFromFile();
            }

            return fileData;
        }

        /// <summary>
        /// Method to request file name of the file
        /// existing in the current working directory.
        /// </summary>
        /// <returns>File name.</returns>
        private static string RequestPathToExistingFile()
        {
            Console.Write(fileNameRequestReport, currentWorkingDirectory);
            string userInput = Console.ReadLine();
            if (!userInput.EndsWith(".txt"))
            { userInput += ".txt"; }

            while (!ValidateFileName(userInput) || !File.Exists(userInput))
            {
                if (!ValidateFileName(userInput))
                { 
                    Console.WriteLine(badNameFormatReport); 
                }
                else
                { 
                    Console.WriteLine(missingFileReport, userInput); 
                }
                Console.Write(fileNameRequestReport, currentWorkingDirectory);
                userInput = Console.ReadLine();
                if (!userInput.EndsWith(".txt"))
                { userInput += ".txt"; }
            }

            try
            { return Path.GetFullPath(userInput); }
            catch
            { return userInput; }
        }

        /// <summary>
        /// Method to validate file name.
        /// </summary>
        /// <param name="fileName">File name to validate.</param>
        /// <returns>true if name is valid; otherwise, false.</returns>
        private static bool ValidateFileName(string fileName)
            => fileName.IndexOfAny(invalidNameChars) == -1;

        private static Encoding DetectEncoding(string path)
        {// Function to read first bytes of the file and peek an Encoding.
            byte[] bomBytes = ReadBomBytes(path);
            if (bomBytes.Length == 0)
            {
                return defaultReadEncoding;
            }
            return SelectEncoding(bomBytes);
        }

        /// <summary>
        /// Method to read BOM bytes from the file.
        /// </summary>
        /// <returns>BOM bytes array.</returns>
        /// <remarks>
        /// Another possible way of realisation:
        /// <code>
        /// using (FileStream fileStream = File.OpenRead(path))
        /// {
        ///     fileStream.Position = 0;
        ///     long bomBytesLength = fileStream.Length > 4 ? 4 : fileStream.Length;
        ///     byte[] bomBytes = new byte[bomBytesLength];
        ///     try
        ///     { fileStream.Read(bomBytes, 0, bomBytes.Length); }
        ///     catch
        ///     { return new byte[0]; }
        ///     return bomBytes;
        /// }
        /// </code>
        /// </remarks>
        private static byte[] ReadBomBytes(string path)
        {
            FileStream fileStream;
            try
            { 
                fileStream = File.OpenRead(path); 
            }
            catch
            { 
                return new byte[0]; 
            }
            fileStream.Position = 0;
            long bomBytesLength = fileStream.Length > 4 ? 4 : fileStream.Length;
            byte[] bomBytes = new byte[bomBytesLength];
            try
            { 
                fileStream.Read(bomBytes, 0, bomBytes.Length);
                fileStream.Close();
            }
            catch
            { 
                return new byte[0]; 
            }
            return bomBytes;
        }

        /// <summary>
        /// Function to select file's encoding 
        /// based on first bytes of the file.
        /// </summary>
        /// <param name="bomBytes">Array with BOM bytes.</param>
        /// <returns>Selected encoding.</returns>
        private static Encoding SelectEncoding(byte[] bomBytes)
        {
            // UTF-16 BE starts with FE FF.
            if (bomBytes[0] == 0xFE && bomBytes[1] == 0xFF)
            { return Encoding.BigEndianUnicode; }

            // UTF-8 starts with EF BB BF.
            if (bomBytes[0] == 0xEF &&
                bomBytes[1] == 0xBB &&
                bomBytes[2] == 0xBF)
            { return Encoding.UTF8; }

            // UTF-7 starts with 2B 2F 76.
            if (bomBytes[0] == 0x2B &&
                bomBytes[1] == 0x2F &&
                bomBytes[2] == 0x76)
            { return Encoding.UTF7; }

            // Both UTF-16 LE and UTF-32 LE 
            // start with FF FE.
            if (bomBytes[0] == 0xFF &&
                bomBytes[1] == 0xFE)
            {
                if (bomBytes[2] == 0 && bomBytes[3] == 0)
                { return Encoding.UTF32; }
                return Encoding.Unicode;
            }

            // File may not contain BOM.
            return defaultReadEncoding;
        }
    }
}
